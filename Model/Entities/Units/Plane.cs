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
    
    public override bool CheckIfReachable(ARegion target) => Region.GetAllFriendlyNeighbours(CurrentMovement, Nation).Contains(target);
}