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
 
    public override bool SetLocation(ARegion region){
        if (region.IsWaterRegion()){
            Region = (WaterRegion)region;
            RegionId = region.Id;
            return true;
        }

        return false;
    }

    public override bool MoveToTarget(EPhase phase){
        List<ARegion> path = GetPath(phase);
        if (path.Count == 0) return false;
        
        if (Target.IsLandRegion()){
            CurrentMovement -= path.Count - 1;
            PreviousLocation = Region;
            Region = (WaterRegion)path[^2];
            RemoveTarget();
            return true;
        }
        
        CurrentMovement -= path.Count;
        PreviousLocation = Region;
        Region = (WaterRegion)Target;
        RemoveTarget();
        CanMove = !CanMove;
        if (phase == EPhase.CombatMove) CanMove = false;
        return true;
    }

    public override bool IsShip() => true;
}