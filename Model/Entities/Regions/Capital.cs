using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Regions;

[Table("CAPITALS")]
public class Capital{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CAPITAL_ID")]
    public int Id{ get; set; }

    [Column("NAME")]
    public string Name{ get; set; }
}