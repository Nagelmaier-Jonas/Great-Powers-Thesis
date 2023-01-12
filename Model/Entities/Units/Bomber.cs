using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("BOMBER")]
public class Bomber : APlane{
    public override int Movement{ get; protected set; } = 6;
    public override int Cost{ get; protected set; } = 12;
    public override int Attack{ get; protected set; } = 4;
    public override int Defense{ get; protected set; } = 1;
    
    public override bool IsBomber() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsBomber();
    
    public override string ToString() => "Bomber";
}