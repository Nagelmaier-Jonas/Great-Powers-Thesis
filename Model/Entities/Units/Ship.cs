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
    public override bool SetLocation(ARegion region){
        if (region.Type == ERegionType.LAND){
            Region = (WaterRegion)region;
            return true;
        }
        return false;
    }
    
    public override bool CanReach(ARegion region) => Region.GetPathToTargetByWaterWithMax(region,CurrentMovement).Count != 0;
}