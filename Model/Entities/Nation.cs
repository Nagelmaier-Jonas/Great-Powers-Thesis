using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities;

[Table("NATIONS")]
public class Nation{
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("NAME", TypeName = "VARCHAR(45)")]
    public string Name{ get; set; }
    
    [Column("TREASURY")]
    public int Treasury{ get; set; }

    [Column("PLAYER_ID")]
    public string UserId{ get; set; }

    public User User{ get; set; }

    public List<ARegion> Regions{ get; set; }
}