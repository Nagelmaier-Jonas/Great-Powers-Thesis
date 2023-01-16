using Domain.Repositories;
using Domain.Repositories.Implementations;
using Domain.Services;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units.Abstract;
using View.Services;

namespace View;

public class GameEngine{
    public SessionInfoRepository _SessionInfoRepository{ get; set; }
    public UnitRepository _UnitRepository{ get; set; }
    public NationRepository _NationRepository{ get; set; }
    public BattleRepository _BattleRepository{ get; set; }
    public IServiceScopeFactory _ServiceScopeFactory{ get; set; }
    public ViewRefreshService _ViewRefreshService{ get; set; }
    public RegionRepository _RegionRepository{ get; set; }

    public FileService FileService{ get; set; }
    public GameEngine(IServiceScopeFactory serviceScopeFactory){
        _ServiceScopeFactory = serviceScopeFactory;
        using var scope = _ServiceScopeFactory.CreateScope();
        Init(scope);
    }

    private void Init(IServiceScope scope){
        _SessionInfoRepository = scope.ServiceProvider.GetRequiredService<SessionInfoRepository>();
        _UnitRepository = scope.ServiceProvider.GetRequiredService<UnitRepository>();
        _NationRepository = scope.ServiceProvider.GetRequiredService<NationRepository>();
        _BattleRepository = scope.ServiceProvider.GetRequiredService<BattleRepository>();
        _ViewRefreshService = scope.ServiceProvider.GetRequiredService<ViewRefreshService>();
        FileService = scope.ServiceProvider.GetRequiredService<FileService>();
        _RegionRepository = scope.ServiceProvider.GetRequiredService<RegionRepository>();
    }

    public async Task PlanMovement(AUnit unit, ARegion target){
        Init(_ServiceScopeFactory.CreateScope());
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        if (unit.SetTarget(session.Phase, target)) await _UnitRepository.UpdateAsync(unit);
        _ViewRefreshService.Refresh();
    }

    public async Task RemovePlannedMovement(AUnit unit){
        Init(_ServiceScopeFactory.CreateScope());
        unit.RemoveTarget();
        await _UnitRepository.UpdateAsync(unit);
        _ViewRefreshService.Refresh();
    }
    
    public async Task<List<ARegion>> GetPathForUnit(AUnit unit){
        Init(_ServiceScopeFactory.CreateScope());
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        return unit.GetPathToTarget(session.Phase);
    }

    public async Task MoveUnits(){
        Init(_ServiceScopeFactory.CreateScope());
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        List<AUnit> units = await _UnitRepository.ReadAsync(u => u.Target != null);
        foreach (var unit in units.Where(unit => unit.MoveToTarget(session.Phase))){
            await _UnitRepository.UpdateAsync(unit);
        }
    }

    public async Task<List<ARegion>> GetPossibleTarget(AUnit unit){
        Init(_ServiceScopeFactory.CreateScope());
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        return unit.GetPossibleTargets(session.Phase);
    }

    public async Task CreateUnit(AUnit type, Nation nation){
        Init(_ServiceScopeFactory.CreateScope());
        AUnit unit = type.GetNewInstanceOfSameType();
        unit.NationId = nation.Id;
        await _UnitRepository.CreateAsync(unit);
    }

    public async Task<List<AUnit>> GetMovingUnits(){
        Init(_ServiceScopeFactory.CreateScope());
        return await _UnitRepository.ReadAsync(u => u.Target != null);
    } 

    public async Task PlaceUnit(AUnit unit, ARegion region){
        Init(_ServiceScopeFactory.CreateScope());
        if (unit.SetLocation(region)) await _UnitRepository.UpdateAsync(unit);
    }

    private async Task<bool> CheckIfAllUnitsAreMobilized(){
        Init(_ServiceScopeFactory.CreateScope());
        return (await _UnitRepository.ReadAsync(u => u.GetLocation() == null && !u.IsCargo())).Count == 0;
    }
    
    private async Task<Battle?> StartBattle(ARegion region){
        Init(_ServiceScopeFactory.CreateScope());
        if (!(await GetBatteLocations()).Contains(region)) return null;
        
        Battle battle = new Battle{
            Location = region,
            Phase = region.IsWaterRegion() ? EBattlePhase.SPECIAL_SUBMARINE : EBattlePhase.ATTACK,
            CurrentNation = (await _SessionInfoRepository.ReadAsync()).Nation
        };
        
        List<AUnit> units = region.GetStationedUnits();
        units.AddRange(region.IncomingUnits);
        
        foreach (var unit in units){
            if (unit.Target is null) unit.Defender = battle;
            else unit.Aggressor = battle;
            await _UnitRepository.UpdateAsync(unit);
        }
        
        await _BattleRepository.CreateAsync(battle);
        return (await _BattleRepository.ReadAsync(b => b.Location == region)).FirstOrDefault();
    }

    public async Task<Battle> GetBattle(ARegion region){
        Init(_ServiceScopeFactory.CreateScope());
        Battle? battle = await _BattleRepository.GetBattleFromLocation(region);
        if (battle is null) return await StartBattle(region);
        return battle;
    }

    public async Task<List<ARegion>> GetBatteLocations() => await _RegionRepository.ReadAsync(r => r.IncomingUnits.Count > 0);
    
    public async Task<List<ARegion>> GetPossibleRetreatTargets(AUnit unit){
        Init(_ServiceScopeFactory.CreateScope());
        Battle battle = (await _BattleRepository.ReadAsync(b => b.Location == unit.GetLocation())).FirstOrDefault();
        if (battle is null) return new List<ARegion>();
        if (!battle.Attackers.Contains(unit)) return new List<ARegion>();
        List<ARegion> previosRegions = (from u in battle.Attackers
            where u.GetPreviousLocation() is not null
            select u.GetPreviousLocation()).ToList();
        return unit.GetPossibleRetreatTargets(previosRegions);
    }

    public async Task<bool> EndPhase(){
        Init(_ServiceScopeFactory.CreateScope());
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        switch (session.Phase){
            case EPhase.PurchaseUnits:
                session.Phase = EPhase.CombatMove;
                break;
            case EPhase.CombatMove:
                await MoveUnits();
                session.Phase = EPhase.ConductCombat;
                break;
            case EPhase.ConductCombat:
                session.Phase = EPhase.NonCombatMove;
                break;
            case EPhase.NonCombatMove:
                await MoveUnits();
                session.Phase = EPhase.MobilizeNewUnits;
                break;
            case EPhase.MobilizeNewUnits:
                if (!await CheckIfAllUnitsAreMobilized()) return false;
                session.Phase = EPhase.CollectIncome;
                break;
            case EPhase.CollectIncome:
                Init(_ServiceScopeFactory.CreateScope());
                Nation nation = await _NationRepository.ReadAsync(session.CurrentNationId);
                nation.CollectIncome();
                Init(_ServiceScopeFactory.CreateScope());
                await _NationRepository.UpdateAsync(nation);
                session.Phase = EPhase.PurchaseUnits;
                if (session.CurrentNationId == 5) session.CurrentNationId = 1;
                if (session.CurrentNationId != 5) session.CurrentNationId++;
                break;
        }
        FileService.WriteSessionInfoToFile(session.Path,session);
        Init(_ServiceScopeFactory.CreateScope());
        await _SessionInfoRepository.UpdateAsync(session);
        _ViewRefreshService.Refresh();
        return true;
    }
}