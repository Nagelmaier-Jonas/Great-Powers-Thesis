using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Regions;

[Table("CANAL_OWNERS_JT")]
public class CanalOwners{
    [Column("REGION_ID")]
    public int NeighboursRegionId{ get; set; }
    
    [Column("NEIGHBOUR_ID")]
    public int NeighboursNeighbourId{ get; set; }
    
    [Column("CANAL_OWNER_ID")] 
    public int CanalOwnerId{ get; set; }

    public LandRegion CanalOwner{ get; set; }
    
    public Neighbours Neighbours{ get; set; }
}