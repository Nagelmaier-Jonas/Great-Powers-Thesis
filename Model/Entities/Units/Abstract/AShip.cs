using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units.Abstract;

[Table("SHIPS_BT")]
public abstract class AShip : AUnit{
    [Column("LOCATION_ID")] public int? RegionId{ get; set; }

    public WaterRegion? Region{ get; set; }
    
    [Column("PREVIOS_LOCATION_ID")] public int? PreviousLocationId{ get; set; }

    public WaterRegion? PreviousLocation{ get; set; }

    public override ARegion? GetLocation() => Region;
    public override ARegion? GetPreviousLocation() => PreviousLocation;
 
    public override bool SetLocation(ARegion target){
        if (target.IsWaterRegion()){
            Region = (WaterRegion)target;
            return true;
        }

        return false;
    }

    public override bool MoveToTarget(EPhase phase){
        if (GetPathToTarget(phase).Count == 0) return false;
        if (phase == EPhase.CombatMove && Target.IsLandRegion()){
            CurrentMovement -= GetDistanceToTarget(phase) - 1;
            List<ARegion> path = GetPathToTarget(phase);
            PreviousLocation = Region;
            Region = (WaterRegion)path[^2];
            CanMove = false;
            return true;
        }
        CurrentMovement -= GetDistanceToTarget(phase);
        PreviousLocation = Region;
        Region = (WaterRegion)Target;
        if (phase == EPhase.CombatMove) CanMove = false;
        return true;
    }

    public override bool IsShip() => true;
}