using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace Model.Entities.Units;

[Table("FIGHTER")]
public class Fighter : APlane{
    public override int Movement{ get; protected set; } = 4;
    public override int Cost{ get; protected set; } = 10;
    public override int Attack{ get; protected set; } = 3;
    public override int Defense{ get; protected set; } = 4;
    
    public override bool IsFighter() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsFighter();
    
    public override string ToString() => "Fighter";
    
    public override AUnit GetNewInstanceOfSameType() => PlaneFactory.CreateFighter(null, null);
}