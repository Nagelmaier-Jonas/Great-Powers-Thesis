using Domain.Repositories;
using Domain.Repositories.Implementations;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace View;

public static class GameEngine{
    public static SessionInfoRepository _SessionInfoRepository{ get; set; }
    public static UnitRepository _UnitRepository{ get; set; }
    
    public static void Init(SessionInfoRepository _sessionInfoRepository, UnitRepository _unitRepository){
        _SessionInfoRepository = _sessionInfoRepository;
        _UnitRepository = _unitRepository;
    }

    public static async void PlanMovement(AUnit unit, ARegion target){
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        if (unit.SetTarget(session.Phase,target)) _UnitRepository.UpdateAsync(unit);
    }
    
    public static async Task<List<ARegion>> GetPossibleTarget(AUnit unit){
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;

        return unit.GetPossibleTargets(session.Phase);
    }
}