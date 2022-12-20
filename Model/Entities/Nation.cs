using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Model.Entities;

[Table("NATIONS")]
public class Nation{
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("NAME", TypeName = "VARCHAR(45)")]
    public string? Name{ get; set; }
    
    [Column("TREASURY")]
    public int? Treasury{ get; set; }

    [Column("PLAYER_ID")]
    public string? UserId{ get; set; }

    public User? User{ get; set; }

    public List<LandRegion>? Regions{ get; set; }
    public List<AUnit>? Units{ get; set; }
    public List<Allies>? Allies{ get; set; }
}