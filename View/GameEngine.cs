using System.Diagnostics;
using Domain.Repositories;
using Domain.Repositories.Implementations;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace View;

public class GameEngine{
    public SessionInfoRepository _SessionInfoRepository{ get; set; }
    public UnitRepository _UnitRepository{ get; set; }

    public NationRepository _NationRepository{ get; set; }

    public GameEngine(SessionInfoRepository _sessionInfoRepository, UnitRepository _unitRepository,
        NationRepository _nationRepository){
        _SessionInfoRepository = _sessionInfoRepository;
        _UnitRepository = _unitRepository;
        _NationRepository = _nationRepository;
    }

    public async Task PlanMovement(AUnit unit, ARegion target){
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        if (unit.SetTarget(session.Phase, target)) _UnitRepository.UpdateAsync(unit);
    }

    public async Task MoveUnits(){
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        List<AUnit> units = await _UnitRepository.ReadAsync(u => u.Target != null);
        foreach (var unit in units.Where(unit => unit.MoveToTarget(session.Phase))){
            await _UnitRepository.UpdateAsync(unit);
        }
    }

    public async Task<List<ARegion>> GetPossibleTarget(AUnit unit){
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        return unit.GetPossibleTargets(session.Phase);
    }

    public async Task CreateUnit(AUnit type, Nation nation){
        AUnit unit = type.GetNewInstanceOfSameType();
        unit.Nation = nation;
        await _UnitRepository.CreateAsync(unit);
    }

    public async Task PlaceUnit(AUnit unit, ARegion region){
        if (unit.SetLocation(region)) await _UnitRepository.UpdateAsync(unit);
    }

    private async Task<bool> CheckIfAllUnitsAreMobilized() =>
        (await _UnitRepository.ReadAsync(u => u.GetLocation() == null && !u.IsCargo())).Count == 0;


    public async Task<bool> EndPhase(){
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        switch (session.Phase){
            case EPhase.PurchaseUnits:
                session.Phase = EPhase.CombatMove;
                break;
            case EPhase.CombatMove:
                MoveUnits();
                session.Phase = EPhase.ConductCombat;
                break;
            case EPhase.ConductCombat:
                session.Phase = EPhase.NonCombatMove;
                break;
            case EPhase.NonCombatMove:
                MoveUnits();
                session.Phase = EPhase.MobilizeNewUnits;
                break;
            case EPhase.MobilizeNewUnits:
                if (!await CheckIfAllUnitsAreMobilized()) return false;
                session.Phase = EPhase.CollectIncome;
                break;
            case EPhase.CollectIncome:
                Nation nation = await _NationRepository.ReadAsync(session.CurrentNationId);
                nation.CollectIncome();
                _NationRepository.UpdateAsync(nation);
                session.Phase = EPhase.PurchaseUnits;
                if (session.CurrentNationId == 5) session.CurrentNationId = 1;
                if (session.CurrentNationId != 5) session.CurrentNationId++;
                break;
        }

        await _SessionInfoRepository.UpdateAsync(session);
        return true;
    }
}