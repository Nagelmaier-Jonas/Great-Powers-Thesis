using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Units;

[Table("UNITS_BT")]
public abstract class AUnit{
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("MOVEMENT")]
    public int Movement{ get; set; }

    [Column("UNIT_TYPE", TypeName = "VARCHAR(45)")]
    public EUnitType Type{ get; set; }
}