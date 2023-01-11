using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Regions;

[Table("FACTORIES")]
public class Factory : IBuyable{
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("DAMAGE")]
    public int Damage{ get; set; }

    [Column("REGION_ID")]
    public int RegionId{ get; set; }

    public LandRegion Region{ get; set; }

    [Column("COST")] public int Cost{ get; set; } = 15;
    public int GetCost() => Cost;
}