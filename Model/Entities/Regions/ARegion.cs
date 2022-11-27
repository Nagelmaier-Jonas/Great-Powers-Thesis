using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Regions;

[Table("REGIONS_BT")]
public abstract class ARegion{
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("NAME", TypeName = "VARCHAR(45)")]
    public string Name{ get; set; }
    
    [Column("HAS_LANDING_STRIP", TypeName = "TINYINT")]
    public bool HasLandingStrip{ get; set; }

    [Column("OWNER_ID")]
    public int NationId{ get; set; }

    public Nation Nation{ get; set; }

    public List<Neighbours> Neighbours{ get; set; }
}