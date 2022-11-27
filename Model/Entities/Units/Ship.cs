using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("SHIPS_BT")]
public class Ship : AUnit{
    [Column("LOCATION_ID")]
    public int RegionId{ get; set; }
    
    public WaterRegion Region{ get; set; }
}