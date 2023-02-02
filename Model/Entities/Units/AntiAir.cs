using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Factories;
using Model.Entities.Units.Abstract;

namespace Model.Entities.Units;

[Table("ANTI_AIR")]
public class AntiAir : ALandUnit{
    public override int Movement{ get; protected set; } = 1;
    public override int Cost{ get; protected set; } = 5;
    public override int Attack{ get; protected set; } = 0;
    public override int Defense{ get; protected set; } = 0;

    protected override bool CheckForMovementRestrictions(Node target, Node previous, EPhase phase){
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanMove) break;
                if (target.Region.IsWaterRegion()){
                    return target.Region.GetOpenTransports(Nation, phase).Count != 0;
                }
                LandRegion neigh = (LandRegion)target.Region;
                if (target.Region.ContainsEnemies(Nation)) break;
                if (neigh.Nation != Nation && neigh.Nation.Allies.All(a => a.Ally != Nation)) break;
                return true;
            case EPhase.CombatMove:
                //Anti Air can only move in the Non Combat Movement Phase
                return false;
        }
        return false;
    }
    
    public override bool IsAntiAir() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsAntiAir();
    
    public override string ToString() => "Anti Air";
    
    public override AUnit GetNewInstanceOfSameType() => LandUnitFactory.CreateAntiAir(null, null);
}