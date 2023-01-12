using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Factories;

namespace Model.Entities.Units;

[Table("CRUISER")]
public class Cruiser : AShip{
    public override int Movement{ get; protected set; } = 2;
    public override int Cost{ get; protected set; } = 12;
    public override int Attack{ get; protected set; } = 3;
    public override int Defense{ get; protected set; } = 3;

    protected override bool CheckForMovementRestrictions(int distance, Neighbours target, EPhase phase){
        //Ships cant pass through canals if they arent owned by a friendly Nation
        if (target.CanalOwners.Any(o =>
                o.CanalOwner.Nation != Nation && o.CanalOwner.Nation.Allies.All(a => a.Ally != Nation))) return false;
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanMove) break;
                if (target.Neighbour.IsLandRegion()) break;
                if (target.Neighbour.ContainsEnemies(Nation)) break;
                
                return true;
            case EPhase.CombatMove:
                //Battleships and Cruisers can attack coastal Land Regions to support amphibious assaults, Transporters conduct the Amphibious assaults
                if (distance != 1 && target.Neighbour.IsLandRegion()) break;

                //Battleships and Cruisers can attack coastal Land Regions to support amphibious assaults, but only if a Transporter is conducting one
                if (distance == 1 && target.Neighbour.IsLandRegion() && !target.Neighbour.IncomingUnits.Any(u =>
                        u.IsTransport() && u.Nation == Nation ||
                        u.Nation.Allies.Any(a => a.Ally == Nation))) break;
            
                //If a Ship doesnt support an amphibious assault, it has to end its attack on a Field containing Enemies, unless it started in an enemy Field and is escaping elsewhere
                if (distance == 1 &&
                    !target.Neighbour.ContainsAnyEnemies(Nation) && !Region.IsHostile(Nation)) break;
            
                //A Ship cant move through enemy Fields to attack, unless its a Submarine
                if (distance != 1 && target.Neighbour.IsHostile(Nation)) break;
            
                return true;
        }

        return false;
    }
    
    public override List<AUnit> GetSubUnits() => null;
    
    public override bool IsCruiser() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsCruiser();
    
    public override string ToString() => "Cruiser";
    
    public override AUnit GetNewInstanceOfSameType() => ShipFactory.CreateCruiser(null, null);
}