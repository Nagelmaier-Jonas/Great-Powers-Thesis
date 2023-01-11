using Domain.Repositories;
using Domain.Repositories.Implementations;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace View;

public static class GameEngine{
    public static SessionInfoRepository _SessionInfoRepository{ get; set; }
    
    public static void Init(SessionInfoRepository _sessionInfoRepository){
        _SessionInfoRepository = _sessionInfoRepository;
    }

    public static async Task PlanMovement(AUnit unit, ARegion target){
        SessionInfo session = (await _SessionInfoRepository.ReadAsync())!;
        if (unit.SetTarget(session.Phase,target)){
            
        }
    }
}