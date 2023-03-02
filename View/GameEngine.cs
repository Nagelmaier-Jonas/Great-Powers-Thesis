using System.Text.Json;
using DataTransfer;
using Domain.Repositories;
using Domain.Repositories.Implementations;
using Domain.Services;
using EventBus.Clients;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units.Abstract;
using View.Components.Game.Drawer.ConductCombat;

namespace View;

public class GameEngine{
    public SessionInfoRepository _SessionInfoRepository{ get; set; }
    public UnitRepository _UnitRepository{ get; set; }
    public FactoryRepository _FactoryRepository{ get; set; }
    public NationRepository _NationRepository{ get; set; }
    public BattleRepository _BattleRepository{ get; set; }
    public IServiceScopeFactory _ServiceScopeFactory{ get; set; }
    public ViewRefreshService _ViewRefreshService{ get; set; }
    public RegionRepository _RegionRepository{ get; set; }
    public WaterRegionRepository _WaterRegionRepository{ get; set; }
    public FileService FileService{ get; set; }
    public EventPublisher _EventPublisher{ get; set; }

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
        _FactoryRepository = scope.ServiceProvider.GetRequiredService<FactoryRepository>();
        _EventPublisher = scope.ServiceProvider.GetRequiredService<EventPublisher>();
        _WaterRegionRepository = scope.ServiceProvider.GetRequiredService<WaterRegionRepository>();
    }

    public async Task PlanMovement(AUnit unit, ARegion target){
        Init(_ServiceScopeFactory.CreateScope());
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        if (unit.SetTarget(session.Phase, target)){
            Init(_ServiceScopeFactory.CreateScope());
            await _UnitRepository.UpdateAsync(unit);
        }
        _EventPublisher.Publish(JsonSerializer.Serialize(new StateHasChangedEvent()));  
    }

    public async Task RemovePlannedMovement(AUnit unit){
        Init(_ServiceScopeFactory.CreateScope());
        await _UnitRepository.RemoveTargetAsync(unit.Id);
        _EventPublisher.Publish(JsonSerializer.Serialize(new StateHasChangedEvent()));  
    }
    
    public async Task<List<ARegion>> GetPathForUnit(AUnit unit){
        Init(_ServiceScopeFactory.CreateScope());
        SessionInfo session = await _SessionInfoRepository.ReadAsync();
        return unit.GetPathToTarget(session.Phase);
    }

    public async Task MoveUnits(){
        Init(_ServiceScopeFactory.CreateScope());
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        List<Nation> nations = await _NationRepository.ReadAllGraphAsync();
        await _WaterRegionRepository.ReadAllGraphAsync();
        List<AUnit> units = nations.SelectMany(n => n.Units.Where(u => u.TargetId != null)).ToList();
        foreach (var unit in units){
            if(!unit.MoveToTarget(session.Phase)) return;
            Init(_ServiceScopeFactory.CreateScope());
            await _UnitRepository.MoveUpdateAsync(unit);
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

    public async Task<List<ARegion>> GetCountryWithFactory(){
        Init(_ServiceScopeFactory.CreateScope());
        var sessionInfo = await _SessionInfoRepository.ReadAsync();
        if (sessionInfo == null) return new List<ARegion>();
        Init(_ServiceScopeFactory.CreateScope());
        return await _RegionRepository.GetCountryRegionsWithFactory(sessionInfo.CurrentNationId);
    }
    
    public async Task<List<ARegion>> GetCountryWithoutFactory(){
        Init(_ServiceScopeFactory.CreateScope());
        var sessionInfo = await _SessionInfoRepository.ReadAsync();
        if (sessionInfo == null) return new List<ARegion>();
        Init(_ServiceScopeFactory.CreateScope());
        return await _RegionRepository.GetCountryRegionsWithoutFactory(sessionInfo.CurrentNationId);
    }

    public async Task<List<ARegion>> GetRegionsForShipPlacement(){
        Init(_ServiceScopeFactory.CreateScope());
        var regions = await _RegionRepository.ReadAllAsync();
        var validSeaRegions = new List<ARegion>();
        foreach (var r in regions.Where(r => r.IsWaterRegion())){
            Init(_ServiceScopeFactory.CreateScope());
            var canPlaceShip = await CanPlaceShip(r);
            if (canPlaceShip){
                validSeaRegions.Add(r);
            }
        }
        return validSeaRegions;
    }

    public async Task<List<AUnit>> GetPlaceableUnits(){
        Init(_ServiceScopeFactory.CreateScope());
        return await _UnitRepository.GetPlaceableUnits();
    }
    
    public async Task<List<AUnit>> GetPlaceableFactories(){
        Init(_ServiceScopeFactory.CreateScope());
        return await _FactoryRepository.GetPlaceableFactories();
    }

    public async Task PlaceUnit(AUnit unit, ARegion region){
        Init(_ServiceScopeFactory.CreateScope());
        if (unit.SetLocation(region)) await _UnitRepository.UpdateAsync(unit);
        Init(_ServiceScopeFactory.CreateScope());
        if (region.IsLandRegion()){
            region.IncreaseTroopsMobilized();
        }
        await _RegionRepository.UpdateAsync(region);
    }
    
    public async Task ResetMovement(){
        Init(_ServiceScopeFactory.CreateScope());
        List<AUnit> units = await _UnitRepository.ReadAsync(u => !u.CanMove);
        units.ForEach(u => u.CanMove = true);
        foreach (var unit in units){
            Init(_ServiceScopeFactory.CreateScope());
            await _UnitRepository.UpdateAsync(unit);
        }
    } 
    public async Task<bool> CanPlaceShip(ARegion region){
        Init(_ServiceScopeFactory.CreateScope());
        var sessionInfo = await _SessionInfoRepository.ReadAsync();
        if (sessionInfo == null) return false;
        Init(_ServiceScopeFactory.CreateScope());
        var neighbors = region.Neighbours;
        var neighborLandRegions = await _RegionRepository.GetNeighborsLandRegions(neighbors);
        return neighborLandRegions.Any(r => r.GetOwnerId() == sessionInfo.CurrentNationId && r.GetFactory() is not null);
    }

    private async Task<bool> CheckIfAllUnitsAreMobilized(){
        Init(_ServiceScopeFactory.CreateScope());
        return (await GetPlaceableUnits()).Count == 0;
    }

    private async Task ResetAllFactoryRegions(){
        Init(_ServiceScopeFactory.CreateScope());
        var regions = await _RegionRepository.ReadAllAsync();
        var landRegions = regions.Where(r => r is LandRegion && r.GetTroopsMobilized() is not 0).Cast<LandRegion>().ToList();
        foreach (var region in landRegions){
            Init(_ServiceScopeFactory.CreateScope());
            await _RegionRepository.ResetTroopsMobilized(region.Id);
        }
    }

    private async Task<Battle> StartBattle(ARegion region){
        Init(_ServiceScopeFactory.CreateScope());
        var regions = await GetBattleLocations();
        if (regions.All(i => i.Id != region.Id)) return null;
        Init(_ServiceScopeFactory.CreateScope());
        var sessionInfo = await _SessionInfoRepository.ReadAsync();
        Battle battle = new Battle{
            LocationId = region.Id,
            Phase = region.IsWaterRegion() ? EBattlePhase.SPECIAL_SUBMARINE : EBattlePhase.ATTACK,
            CurrentNationId = sessionInfo.CurrentNationId
        };
        Init(_ServiceScopeFactory.CreateScope());
        await _BattleRepository.CreateAsync(battle);
        Init(_ServiceScopeFactory.CreateScope());
        List<AUnit> units = region.GetStationedUnits();
        units.AddRange(region.IncomingUnits);

        foreach (var unit in units){
            if (unit.Target is null) unit.DefenderId = battle.Id;
            else unit.AggressorId = battle.Id;
            Init(_ServiceScopeFactory.CreateScope());
            await _UnitRepository.UpdateAsync(unit);
        }
        
        Init(_ServiceScopeFactory.CreateScope());
        var result = await _BattleRepository.GetBattleFromLocation(region);
        result.AttackingInfantryRolls = result.GetInfantryRolls(result.GetAttacker());
        result.DefendingInfantryRolls = result.GetInfantryRolls(result.GetDefendingNations().FirstOrDefault());
        Init(_ServiceScopeFactory.CreateScope());
        await _BattleRepository.UpdateAsync(result);
        return result;
    }

    public async Task<Battle> GetBattle(ARegion region){
        Init(_ServiceScopeFactory.CreateScope());
        Battle? battle = await _BattleRepository.GetBattleFromLocation(region);
        if (battle is null) return await StartBattle(region);
        return battle;
    }
 
    public async Task<List<ARegion>> GetBattleLocations(){
        Init(_ServiceScopeFactory.CreateScope());
        SessionInfo? session = await _SessionInfoRepository.ReadAsync();
        if (session.Phase != EPhase.ConductCombat && session.Phase != EPhase.CombatMove) return new List<ARegion>();
        Init(_ServiceScopeFactory.CreateScope());
        return await _RegionRepository.ReadAsync(r => r.IncomingUnits.Count > 0);
    }

    public async Task AttackerRetreats(Battle battle){
        foreach (var unit in battle.Attackers.Where(u => u.HitPoints > 0)){
            unit.RemoveTarget();
            await _UnitRepository.UpdateAsync(unit);
        }
        if(!battle.AttackerRetreats()) return;
        Init(_ServiceScopeFactory.CreateScope());
        await _BattleRepository.UpdateAsync(battle);
        _EventPublisher.Publish(JsonSerializer.Serialize(new StateHasChangedEvent()));  
    }
    public async Task AttackerContinues(Battle battle){
        if(!battle.AttackerContinues()) return;
        Init(_ServiceScopeFactory.CreateScope());
        await _BattleRepository.UpdateAsync(battle);
        _EventPublisher.Publish(JsonSerializer.Serialize(new StateHasChangedEvent()));  
    }

    public async Task<bool> EndPhase(User CurrentUser){
        Init(_ServiceScopeFactory.CreateScope());
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        switch (session.Phase){
            case EPhase.PurchaseUnits:
                session.Phase = EPhase.CombatMove;
                break;
            case EPhase.CombatMove:
                session.Phase = EPhase.ConductCombat;
                break;
            case EPhase.ConductCombat:
                session.Phase = EPhase.NonCombatMove;
                await UpdateCapitals(session);
                break;
            case EPhase.NonCombatMove:
                await MoveUnits();
                session.Phase = EPhase.MobilizeNewUnits;
                break;
            case EPhase.MobilizeNewUnits:
                if (!await CheckIfAllUnitsAreMobilized()) return false;
                await ResetAllFactoryRegions();
                session.Phase = EPhase.CollectIncome;
                break;
            case EPhase.CollectIncome:
                Init(_ServiceScopeFactory.CreateScope());
                await _NationRepository.CollectNationIncome(session.CurrentNationId);
                session.Phase = EPhase.PurchaseUnits;
                if (session.CurrentNationId == 5){
                    session.CurrentNationId = 1;
                    session.Round++;
                }
                else session.CurrentNationId++;
                await ResetMovement();
                break;
        }

        if (CurrentUser.IsOwner){
            FileService.WriteSessionInfoToFile(session.Path, session);
        }
        Init(_ServiceScopeFactory.CreateScope());
        await _SessionInfoRepository.UpdateAsync(session);
        _EventPublisher.Publish(JsonSerializer.Serialize(new StateHasChangedEvent()));  
        return true;
    }

    private async Task UpdateCapitals(SessionInfo sessionInfo){
        Init(_ServiceScopeFactory.CreateScope());
        var nations = await _NationRepository.ReadAllGraphAsync();
        sessionInfo.AxisCapitals = nations.Where(n => n.Id is 2 or 4).SelectMany(i => i.Regions.Where(r => r.GetCapital() is not null).ToList()).Count();
        sessionInfo.AlliesCapitals = nations.Where(n => n.Id is 1 or 3 or 5).SelectMany(i => i.Regions.Where(r => r.GetCapital() is not null).ToList()).Count();
    }
    public async void FinishBattle(Battle battle){
        if(battle.Attackers.Count > 0){
            if (battle.Location.IsLandRegion()){
                Init(_ServiceScopeFactory.CreateScope());
                Nation winner = await _NationRepository.ReadGraphAsync(battle.GetAttacker().Id);
                LandRegion region = (LandRegion) await _RegionRepository.ReadAsync(battle.LocationId);
                region.Nation = winner;
                region.NationId = winner.Id;
                await _RegionRepository.UpdateAsync(region);
            }
            
            Init(_ServiceScopeFactory.CreateScope());
            SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
            List<Nation> nations = await _NationRepository.ReadAllGraphAsync();
            await _WaterRegionRepository.ReadAllGraphAsync();
            List<AUnit> units = nations.SelectMany(n => n.Units.Where(u => u.TargetId != null)).ToList();
            foreach (var unit in units.Where(unit => battle.Attackers.Any(a => a.Id == unit.Id)).Where(unit => unit.MoveToTarget(session.Phase))){
                Init(_ServiceScopeFactory.CreateScope());
                await _UnitRepository.UpdateAsync(unit);
            }
        }
        
        foreach (var attacker in battle.Attackers){
            Init(_ServiceScopeFactory.CreateScope());
            await _UnitRepository.RemoveAggressorAsync(attacker.Id);
        }
        foreach (var defender in battle.Defenders){
            Init(_ServiceScopeFactory.CreateScope());
            await _UnitRepository.RemoveDefenderAsync(defender.Id);
        }
        foreach (var unit in battle.Casualties){
            Init(_ServiceScopeFactory.CreateScope());
            await _UnitRepository.DeleteUnit(unit.Id);
        }

        Init(_ServiceScopeFactory.CreateScope());
        await _BattleRepository.DeleteBattle(battle.Id);
        _EventPublisher.Publish(JsonSerializer.Serialize(new StateHasChangedEvent()));  
    }
}