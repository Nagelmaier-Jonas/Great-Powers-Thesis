using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("FIGHTER")]
public class Fighter : APlane{
    public override int Movement{ get; protected set; } = 4;
    public override int Cost{ get; protected set; } = 10;
    public override int Attack{ get; protected set; } = 3;
    public override int Defense{ get; protected set; } = 4;
}