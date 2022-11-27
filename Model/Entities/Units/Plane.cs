using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("PLANES")]
public class Plane : AUnit{
    [Column("LOCATION_ID")]
    public int RegionId{ get; set; }
    
    public ARegion Region{ get; set; }

    [Column("AIRCRAFT_CARRIER_ID")]
    public int? AircraftCarrierId{ get; set; }

    public AircraftCarrier? AircraftCarrier{ get; set; }
}