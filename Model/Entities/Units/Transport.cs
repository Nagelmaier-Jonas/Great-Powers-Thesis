using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Units;

[Table("TRANSPORTER")]
public class Transport : Ship{
    public List<LandUnit>? Units{ get; set; }
}