using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace Model.Entities.Units;

[Table("BATTLESHIP")]
public class Battleship : AShip{
    public override int Movement{ get; protected set; } = 2;
    public override int Cost{ get; protected set; } = 20;
    public override int Attack{ get; protected set; } = 4;
    public override int Defense{ get; protected set; } = 4;
    
    public override int HitPoints{ get; set; } = 2;

    protected override bool CheckForMovementRestrictions(Node target, Node previous, EPhase phase){
        //Ships cant pass through canals if they arent owned by a friendly Nation
        if (target.Region.Neighbours.Any(n => n.Neighbour == previous.Region && n.CanalOwners.Any(c => c.CanalOwner.IsHostile(Nation)))) return false;
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanMove) break;
                if (target.Region.IsHostile(Nation)) break;
                if (target.Region.IsLandRegion()) break;
                
                return true;
            case EPhase.CombatMove:
                //Battleships and Cruisers can attack coastal Land Regions to support amphibious assaults, Transporters conduct the Amphibious assaults
                if (target.Region.IsLandRegion() && !target.Region.IsHostile(Nation)) break;
                if (target.Region.IsLandRegion() && previous.Region.IsLandRegion()) break;
                if (target.Region.IsLandRegion() && target.Region.IncomingUnits.All(u => !u.IsTransport() || u.Nation != Nation)) break;

                //If a Ship doesnt support an amphibious assault, it has to end its attack on a Field containing Enemies, unless it started in an enemy Field and is escaping elsewhere
                if (!target.Region.ContainsAnyEnemies(Nation) && !previous.Region.ContainsAnyEnemies(Nation) && previous.Distance != 0) break;
            
                //A Ship cant move through enemy Fields to attack, unless its a Submarine
                if (target.Region.IsHostile(Nation) && previous.Region.IsHostile(Nation)) break;

                return true;
        }

        return false;
    }

    public override List<AUnit> GetSubUnits() => null;
    
    public override bool IsBattleship() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsBattleship();
    
    public override string ToString() => "Battleship";
    
    public override AUnit GetNewInstanceOfSameType() => ShipFactory.CreateBattleship(null, null);
}