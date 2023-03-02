using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units.Abstract;

[Table("SHIPS_BT")]
public abstract class AShip : AUnit{
    [Column("LOCATION_ID")] 
    public int? RegionId{ get; set; }

    public WaterRegion? Region{ get; set; }

    public override ARegion? GetLocation() => Region;
 
    public override bool SetLocation(ARegion region){
        if (region.IsWaterRegion()){
            Region = (WaterRegion)region;
            RegionId = region.Id;
            return true;
        }

        return false;
    }

    public override bool MoveToTarget(EPhase phase){
        if (!CanMove) return false;
        
        List<ARegion> path = GetPath(phase);
        if (path.Count == 0) return false;

        if (phase == EPhase.CombatMove) CanMove = false;
        
        if (Target.IsLandRegion()){
            CurrentMovement -= path.Count - 1;
            Region = (WaterRegion)path[^2];
            RegionId = Region.Id;
            RemoveTarget();
            return true;
        }
        
        CurrentMovement -= path.Count;
        SetLocation(Target);
        RemoveTarget();
        return true;
    }

    public override bool IsShip() => true;
}