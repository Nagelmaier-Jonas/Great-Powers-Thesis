using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace Model.Entities.Units;

[Table("TANK")]
public class Tank : ALandUnit{
    public override int Movement{ get; protected set; } = 2;
    public override int Cost{ get; protected set; } = 6;
    public override int Attack{ get; protected set; } = 3;
    public override int Defense{ get; protected set; } = 3;

    protected override bool CheckForMovementRestrictions(Node target, Node previous, EPhase phase){
        if (target.Region.IsWaterRegion()){
            return target.Region.GetOpenTransports(Nation, phase).Count != 0;
        }

        LandRegion neigh = (LandRegion)target.Region;
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanMove) break;
                if (target.Region.IsHostile(Nation)) break;
                if (previous.Region.IsWaterRegion() && previous.Region.GetOpenTransports(Nation,phase).All(t => t.Target != target.Region)) break;
                    return true;
            case EPhase.CombatMove:
                if (target.Region.ContainsEnemies(Nation) && previous.Region.ContainsEnemies(Nation)) break;
                if (!target.Region.IsHostile(Nation) && !previous.Region.IsHostile(Nation) && previous.Distance != 0) break;
                return true;
            case EPhase.ConductCombat:
                return true;
        }
        return false;
    }

    public override bool IsTank() => true;

    public override bool IsSameType(AUnit unit) => unit.IsTank();

    public override string ToString() => "Tank";

    public override AUnit GetNewInstanceOfSameType() => LandUnitFactory.CreateTank(null, null);
}