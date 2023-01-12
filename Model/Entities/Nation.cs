using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Model.Entities;

[Table("NATIONS")]
public class Nation{
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("NAME", TypeName = "VARCHAR(45)")]
    public string Name{ get; set; } = String.Empty;
    
    [Column("TREASURY")]
    public int Treasury{ get; set; }

    [Column("PLAYER_ID")]
    public string? UserId{ get; set; }

    public User? User{ get; set; }

    [Column("COLOR", TypeName = "VARCHAR(7)")]
    public string Color{ get; set; } = String.Empty;

    [Column("TYPE", TypeName = "VARCHAR(45)")]
    public ENation Type{ get; set; }

    public List<LandRegion> Regions{ get; set; } = new List<LandRegion>();
    public List<AUnit> Units{ get; set; } = new List<AUnit>();
    public List<Allies> Allies{ get; set; } = new List<Allies>();

    public void CollectIncome() => Treasury += Regions.Sum(r => r.Income);
}