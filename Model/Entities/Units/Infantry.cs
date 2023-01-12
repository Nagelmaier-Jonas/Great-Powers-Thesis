using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("INFANTRY")]
public class Infantry : ALandUnit{
    public override int Movement{ get; protected set; } = 1;
    public override int Cost{ get; protected set; } = 3;
    public override int Attack{ get; protected set; } = 1;
    public override int Defense{ get; protected set; } = 2;
    protected override bool CheckForMovementRestrictions(int distance, Neighbours target, EPhase phase){
        if (target.Neighbour.IsWaterRegion()){
            if(target.Neighbour.GetOpenTransports(Nation, phase).Count == 0) return false;
            return true;
        }
        LandRegion neigh = (LandRegion)target.Neighbour;
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanMove) break;
                if (target.Neighbour.ContainsEnemies(Nation)) break;
                if (neigh.Nation != Nation && neigh.Nation.Allies.All(a => a.Ally != Nation)) break;
                return true;
            case EPhase.CombatMove:
                //Units other than Tanks must end their attack on an enemy Field
                if (distance == 1 && !neigh.IsHostile(Nation)) break;
            
                //Units other than Tanks cant move through Hostile Fields to attack
                if(distance > 1 && neigh.IsHostile(Nation)) break;
                
                return true;
        }
        return false;
    }
    
    public override bool IsInfantry() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsInfantry();
    
    public override string ToString() => "Infantry";
}