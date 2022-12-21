using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("PLANES")]
public class Plane : AUnit{

    [Column("LOCATION_ID")]
    public int RegionId{ get; set; }
    
    public ARegion Region{ get; set; }
    
    [Column("TARGET_ID")]
    public int? TargetId{ get; set; }
    
    public ARegion? Target{ get; set; }

    [Column("AIRCRAFT_CARRIER_ID")]
    public int? AircraftCarrierId{ get; set; }

    public AircraftCarrier? AircraftCarrier{ get; set; }
    
    public Plane(int movement, int cost, int attack, int defense){
        Movement = movement;
        Cost = cost;
        Attack = attack;
        Defense = defense;
    }
    
    public override ARegion GetLocation() => Region;

    public override bool SetLocation(ARegion target){
        if (target.Type == Region.Type){
            Region = target;
            return true;
        }
        return false;
    }

    public override ARegion GetTarget() => Target;

    public override bool SetTarget(ARegion region){
        if (region == null){
            Target = null;
            return true;
        }
        if (CanTarget(region)){
            Target = region;
            return true;
        }
        return false;
    }

    public override List<ARegion> GetPathToCurrentTarget() =>
        Region.GetPathToTargetWithMax(Target, CurrentMovement);

    protected override bool CanTarget(ARegion target) =>
        Region.GetPathToTargetWithMax(target, CurrentMovement).Count != 0;
    
    public override bool MoveToTarget(){
        if (GetTarget() != null){
            CurrentMovement -= GetLocation().GetMinimalDistance(Target);
            SetLocation(GetTarget());
            SetTarget(null);
            return true;
        }
        return false;
    }
}