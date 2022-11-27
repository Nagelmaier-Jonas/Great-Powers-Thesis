using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Regions;

[Table("REGION_HAS_NEIGHBOURS_JT")]
public class Neighbours{
    [Column("REGION_ID")]
    public int RegionId{ get; set; }
    
    [Column("NEIGHBOUR_ID")]
    public int NeighbourId{ get; set; }
    
    public ARegion Region{ get; set; }
    
    public ARegion Neighbour{ get; set; }
}