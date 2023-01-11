using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("SHIPS_BT")]
public abstract class AShip : AUnit{
    [Column("LOCATION_ID")] public int RegionId{ get; set; }

    public WaterRegion Region{ get; set; }

    public override ARegion GetLocation() => Region;
 
    protected override bool SetLocation(ARegion target){
        if (target.IsWaterRegion()){
            Region = (WaterRegion)target;
            return true;
        }

        return false;
    }

    public override bool MoveToTarget(EPhase phase){
        if (Target == null) return false;
        if (phase == EPhase.CombatMove && Target.IsLandRegion()){
            CurrentMovement -= GetDistanceToTarget(phase) - 1;
            List<ARegion> path = GetPathToTarget(phase);
            Region = (WaterRegion)path[^2];
            return true;
        }

        CurrentMovement -= GetDistanceToTarget(phase);
        Region = (WaterRegion)Target;
        return true;
    }
}