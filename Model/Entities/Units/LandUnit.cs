using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("LAND_UNITS")]
public class LandUnit : AUnit{

    [Column("LOCATION_ID")]
    public int RegionId{ get; set; }
    
    public LandRegion Region{ get; set; }
    
    [Column("TARGET_ID")]
    public int? TargetId{ get; set; }
    
    public LandRegion? Target{ get; set; }
    
    [Column("TRANSPORT_ID")]
    public int? TransportId{ get; set; }

    public Transport? Transport{ get; set; }
    
    public LandUnit(int movement, int cost, int attack, int defense){
        Movement = movement;
        Cost = cost;
        Attack = attack;
        Defense = defense;
    }

    public override bool CheckIfReachable(ARegion target) => Region.GetFriendlyNeighboursByLand(CurrentMovement).Contains(target);
    
}