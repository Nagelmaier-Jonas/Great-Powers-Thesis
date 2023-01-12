using System.Diagnostics;
using Domain.Repositories;
using Domain.Repositories.Implementations;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;
using Model.Factories;

namespace View;

public static class GameEngine{
    public static SessionInfoRepository _SessionInfoRepository{ get; set; }
    public static UnitRepository _UnitRepository{ get; set; }
    
    public static NationRepository _NationRepository{ get; set; }
    
    public static void Init(SessionInfoRepository _sessionInfoRepository, UnitRepository _unitRepository,NationRepository _nationRepository){
        _SessionInfoRepository = _sessionInfoRepository;
        _UnitRepository = _unitRepository;
        _NationRepository = _nationRepository;
    }

    public static async void PlanMovement(AUnit unit, ARegion target){
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        if (unit.SetTarget(session.Phase,target)) _UnitRepository.UpdateAsync(unit);
    }

    public static async void MoveUnits(){
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        List<AUnit> units = await _UnitRepository.ReadAsync(u => u.Target != null);
        foreach (var unit in units.Where(unit => unit.MoveToTarget(session.Phase))){
            await _UnitRepository.UpdateAsync(unit);
        }
    }
    
    public static async Task<List<ARegion>> GetPossibleTarget(AUnit unit){
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        return unit.GetPossibleTargets(session.Phase);
    }

    public static async void CreateUnit(AUnit type , Nation nation){
        AUnit unit = type.GetNewInstanceOfSameType();
        unit.Nation = nation;
        await _UnitRepository.CreateAsync(unit);
    }

    public static async void PlaceUnit(AUnit unit, ARegion region){
        if (unit.SetLocation(region)) await _UnitRepository.UpdateAsync(unit);
    }
    

    public static async void EndPhase(){
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        /*switch (session.Phase){
            case EPhase.PurchaseUnits:
                session.Phase = EPhase.CombatMove;
                
        }*/
    }
}