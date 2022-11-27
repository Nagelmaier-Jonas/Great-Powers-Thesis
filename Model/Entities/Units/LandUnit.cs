using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("LAND_UNITS")]
public class LandUnit : AUnit{
    [Column("LOCATION_ID")]
    public int RegionId{ get; set; }
    
    public LandRegion Region{ get; set; }
    
    [Column("TRANSPORT_ID")]
    public int? TransportId{ get; set; }

    public Transport? Transport{ get; set; }
}