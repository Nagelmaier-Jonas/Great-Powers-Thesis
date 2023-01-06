using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("SHIPS_BT")]
public class Ship : AUnit{
    [Column("LOCATION_ID")] public int RegionId{ get; set; }

    public WaterRegion Region{ get; set; }

    [Column("TARGET_ID")] public int? TargetId{ get; set; }

    public ARegion? Target{ get; set; }

    public Ship(int movement, int cost, int attack, int defense){
        Movement = movement;
        Cost = cost;
        Attack = attack;
        Defense = defense;
    }

    public override ARegion GetLocation() => Region;

    protected override bool SetLocation(ARegion target){
        if (target.IsWaterRegion()){
            Region = (WaterRegion)target;
            return true;
        }

        return false;
    }

    public override ARegion GetTarget() => Target;

    public override bool CanTarget(EPhase phase, ARegion target) => GetPossibleTargets(phase).Contains(target);

    public override bool SetTarget(EPhase phase, ARegion region){
        if (CanTarget(phase, region)){
            Target = (WaterRegion)region;
            return true;
        }

        return false;
    }

    public override void ClearTarget() => Target = null;

    public override bool MoveToTarget(EPhase phase){
        if (Target == null) return false;
        if (phase == EPhase.CombatMove && Target.IsLandRegion()){
            CurrentMovement -= GetDistanceToTarget(phase) - 1;
            List<ARegion> path = GetPathToTarget(phase);
            Region = (WaterRegion)path[^2];
            return true;
        }

        CurrentMovement -= GetDistanceToTarget(phase);
        Region = (WaterRegion)Target;
        return true;
    }

    public override List<ARegion> GetPossibleTargets(EPhase phase){
        switch (phase){
            case EPhase.NonCombatMove:
                return GetTargetsForNonCombatMove(CurrentMovement, Region);
            case EPhase.CombatMove:
                return GetTargetsForCombatMove(CurrentMovement, Region);
        }

        return new List<ARegion>();
    }

    protected override List<ARegion> GetTargetsForNonCombatMove(int distance, ARegion region){
        if (distance <= 0) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        ARegion location = region ?? Region;
        foreach (var neighbour in location.Neighbours){
            if (neighbour.Neighbour.IsLandRegion()) continue;
            if (neighbour.Neighbour.ContainsEnemies(Nation)) continue;
            //Ships cant pass through canals if they arent owned by a friendly Nation
            if (neighbour.CanalOwners.Any(o =>
                    o.CanalOwner.Nation != Nation || o.CanalOwner.Nation.Allies.All(a => a.Ally != Nation))) continue;
            regions.Add(neighbour.Neighbour);
            regions.UnionWith(GetTargetsForNonCombatMove(distance - 1, neighbour.Neighbour));
        }

        return regions.ToList();
    }

    protected override List<ARegion> GetTargetsForCombatMove(int distance, ARegion region){
        if (distance <= 0) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        ARegion location = region ?? Region;
        foreach (var neighbour in location.Neighbours){
            
            //Battleships and Cruisers can attack coastal Land Regions to support amphibious assaults, Transporters conduct the Amphibious assaults
            if (distance != 1 && Type is EUnitType.CRUISER or EUnitType.BATTLESHIP or EUnitType.TRANSPORT &&
                neighbour.Neighbour.IsLandRegion()) continue;
            
            //If a Ship doesnt support an amphibious assault, it has to end its attack on a Field containing Enemies, unless it started in an enemy Field and is escaping elsewhere
            if (distance != 1 && neighbour.Neighbour.IsWaterRegion() &&
                !neighbour.Neighbour.ContainsAnyEnemies(Nation) && !Region.IsHostile(Nation)) continue;
            
            //A Ship cant move through enemy Fields to attack, unless its a Submarine
            if (distance != 1 && Type != EUnitType.SUBMARINE && neighbour.Neighbour.IsHostile(Nation)) continue;
            
            //A Submarine cant pass under a Destroyer
            if (distance != 1 && Type == EUnitType.SUBMARINE && neighbour.Neighbour.GetStationedUnits().Any(u =>
                    u.Nation != Nation && u.Nation.Allies.All(a => a.Ally != Nation) &&
                    u.Type == EUnitType.DESTROYER)) continue;
            
            //Ships cant pass through canals if they arent owned by a friendly Nation
            if (neighbour.CanalOwners.Any(o =>
                    o.CanalOwner.Nation != Nation || o.CanalOwner.Nation.Allies.All(a => a.Ally != Nation)))
                continue;
            
            regions.Add(neighbour.Neighbour);
            regions.UnionWith(GetTargetsForCombatMove(distance - 1, neighbour.Neighbour));
        }

        return regions.ToList();
    }
}