using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Factories;
using Model.Entities.Units.Abstract;

namespace Model.Entities.Units;

[Table("ARTILLERY")]
public class Artillery : ALandUnit{
    public override int Movement{ get; protected set; } = 1;
    public override int Cost{ get; protected set; } = 4;
    public override int Attack{ get; protected set; } = 2;
    public override int Defense{ get; protected set; } = 2;

    protected override bool CheckForMovementRestrictions(Node target, Node previous, EPhase phase){
        if (target.Region.IsWaterRegion()){
            return target.Region.GetOpenTransports(Nation, phase).Count != 0;
        }
        LandRegion neigh = (LandRegion)target.Region;
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanMove) break;
                if (target.Region.ContainsEnemies(Nation)) break;
                if (neigh.Nation != Nation && neigh.Nation.Allies.All(a => a.Ally != Nation)) break;
                return true;
            case EPhase.CombatMove:

                //Units other than Tanks must end their attack on an enemy Field
                if (Target is not null && target.Region == Target && !neigh.IsHostile(Nation)) break;
            
                //Units other than Tanks cant move through Hostile Fields to attack
                if(Target is not null && target.Region != Target && neigh.IsHostile(Nation)) break;
                
                return true;
        }
        return false;
    }
    
    public override bool IsArtillery() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsArtillery();
    
    public override string ToString() => "Artillery";
    
    public override AUnit GetNewInstanceOfSameType() => LandUnitFactory.CreateArtillery(null, null);
}