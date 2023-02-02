using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace Model.Entities.Units;

[Table("TRANSPORT")]
public class Transport : AShip{
    public List<ALandUnit> Units{ get; set; } = new List<ALandUnit>();

    public override int Movement{ get; protected set; } = 2;
    public override int Cost{ get; protected set; } = 7;
    public override int Attack{ get; protected set; } = 0;
    public override int Defense{ get; protected set; } = 0;
    
    public override List<AUnit> GetSubUnits(){
        return Units.Cast<AUnit>().ToList();
    }

    protected override bool CheckForMovementRestrictions(Node target, Node previous, EPhase phase){
        //Ships cant pass through canals if they arent owned by a friendly Nation
        if (target.Region.Neighbours.Any(n => n.Neighbour == previous.Region && n.CanalOwners.Any(c => c.CanalOwner.IsHostile(Nation)))) return false;
        switch (phase){
            case EPhase.NonCombatMove:
                if(!CanMove) break;
                if (target.Region.IsHostile(Nation)) break;
                if (target.Region.IsLandRegion() && Units.Count == 0) break;
                if (target.Region.IsLandRegion() && target.Region.IsHostile(Nation)) break;
                if (target.Region.IsLandRegion() && previous.Region.IsLandRegion()) break;
                return true;
            case EPhase.CombatMove:
                //Battleships and Cruisers can attack coastal Land Regions to support amphibious assaults, Transporters conduct the Amphibious assaults
                if (target.Region.IsLandRegion() && Units.Count == 0) break;
                if (target.Region.IsLandRegion() && !target.Region.IsHostile(Nation)) break;
                if (target.Region.IsLandRegion() && previous.Region.IsLandRegion()) break;
            
                //A Transport cant attack
                if (target.Region.ContainsAnyEnemies(Nation) && target.Region.IsWaterRegion()) break;
                
                return true;
        }

        return false;
    }
    
    public override bool IsTransport() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsTransport();
    
    public override string ToString() => "Transport";
    
    public override AUnit GetNewInstanceOfSameType() => ShipFactory.CreateTransport(null, null);

    public override bool CanAttack(AUnit unit) => false;
}