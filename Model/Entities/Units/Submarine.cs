using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace Model.Entities.Units;

[Table("SUBMARINE")]
public class Submarine : AShip{
    public override int Movement{ get; protected set; } = 2;
    public override int Cost{ get; protected set; } = 6;
    public override int Attack{ get; protected set; } = 2;
    public override int Defense{ get; protected set; } = 1;

    protected override bool CheckForMovementRestrictions(int distance, Neighbours target, EPhase phase){
        if (target.Neighbour.IsLandRegion()) return false;
        //Ships cant pass through canals if they arent owned by a friendly Nation
        if (target.CanalOwners.Any(o =>
                o.CanalOwner.Nation != Nation && o.CanalOwner.Nation.Allies.All(a => a.Ally != Nation))) return false;
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanMove) break;
                //A Submarine cant pass under a Destroyer
                if (distance != 1 && target.Neighbour.GetStationedUnits().Any(u =>
                        u.Nation != Nation && u.Nation.Allies.All(a => a.Ally != Nation) &&
                        u.IsDestroyer())) break;
                
                if(distance == 1 && target.Neighbour.IsHostile(Nation)) break;
                
                return true;
            case EPhase.CombatMove:
                //If a Ship doesnt support an amphibious assault, it has to end its attack on a Field containing Enemies, unless it started in an enemy Field and is escaping elsewhere
                if (distance == 1 &&
                    !target.Neighbour.ContainsAnyEnemies(Nation) && !Region.IsHostile(Nation)) break;
            
                //A Submarines cant pass under a Destroyer
                if (distance != 1 && target.Neighbour.GetStationedUnits().Any(u =>
                        u.Nation != Nation && u.Nation.Allies.All(a => a.Ally != Nation) &&
                        u.IsDestroyer())) break;
            
                return true;
        }

        return false;
    }
    public override List<AUnit> GetSubUnits() => null;
    
    public override bool IsSubmarine() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsSubmarine();
    
    public override string ToString() => "Submarine";
    
    public override AUnit GetNewInstanceOfSameType() => ShipFactory.CreateSubmarine(null, null);

    public override bool CanAttack(AUnit unit) => !unit.IsPlane();
}