using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Regions;

[Table("LAND_REGION")]
public class LandRegion : ARegion{
    [Column("INCOME")]
    public int Income{ get; set; }

    [Column("IS_CAPITAL", TypeName = "TINYINT")]
    public bool IsCapital{ get; set; }

    public List<Factory>? Factories{ get; set; }
}