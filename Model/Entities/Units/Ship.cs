using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("SHIPS_BT")]
public class Ship : AUnit{
    [Column("LOCATION_ID")]
    public int RegionId{ get; set; }

    public WaterRegion Region{ get; set; }
    
    [Column("TARGET_ID")]
    public int? TargetId{ get; set; }
    
    public WaterRegion? Target{ get; set; }
    
    public Ship(int movement, int cost, int attack, int defense){
        Movement = movement;
        Cost = cost;
        Attack = attack;
        Defense = defense;
    }
    
    public override ARegion GetLocation() => Region;

    public override bool SetLocation(ARegion target){
        if (target.Type == Region.Type){
            Region = (WaterRegion)target;
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
            Target = (WaterRegion)region;
            return true;
        }
        return false;
    }

    public override List<ARegion> GetPathToCurrentTarget() =>
        Region.GetPathToFriendlyWaterTargetWithMax(Target,Nation, CurrentMovement);

    protected override bool CanTarget(ARegion target) =>
        Region.GetPathToFriendlyWaterTargetWithMax(target,Nation, CurrentMovement).Count != 0;
    
    public override bool MoveToTarget(){
        if (GetTarget() != null){
            CurrentMovement -= GetLocation().GetMinimalDistanceByFriendlyWater(Target, Nation);
            SetLocation(GetTarget());
            SetTarget(null);
            return true;
        }
        return false;
    }
}