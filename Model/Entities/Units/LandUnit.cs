using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("LAND_UNITS")]
public class LandUnit : AUnit{
    [Column("LOCATION_ID")] public int? RegionId{ get; set; }

    public LandRegion? Region{ get; set; }

    [Column("TARGET_ID")] public int? TargetId{ get; set; }

    public LandRegion? Target{ get; set; }

    [Column("TRANSPORT_ID")] public int? TransportId{ get; set; }

    public Transport? Transport{ get; set; }

    public LandUnit(int movement, int cost, int attack, int defense){
        Movement = movement;
        Cost = cost;
        Attack = attack;
        Defense = defense;
    }

    public override ARegion GetLocation() => Region;

    public override bool SetLocation(ARegion target){
        if (target.Type == Region.Type){
            Region = (LandRegion)target;
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
            Target = (LandRegion)region;
            return true;
        }
        return false;
    }

    public override List<ARegion> GetPathToCurrentTarget() =>
        Region.GetPathToFriendlyLandTargetWithMax(Target, CurrentMovement);

    protected override bool CanTarget(ARegion target) => 
        Region.GetPathToFriendlyLandTargetWithMax(target, CurrentMovement).Count != 0;
    
    public override bool MoveToTarget(){
        if (GetTarget() != null){
            CurrentMovement -= GetLocation().GetMinimalDistanceByFriendlyLand(Target, Nation);
            SetLocation(GetTarget());
            SetTarget(null);
            return true;
        }
        return false;
    } 
}