using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("LAND_UNITS")]
public class LandUnit : AUnit{
    [Column("LOCATION_ID")] public int? RegionId{ get; set; }

    public LandRegion? Region{ get; set; }

    [Column("TARGET_ID")] public int? TargetId{ get; set; }

    public LandRegion? Target{ get; set; }

    [Column("TRANSPORT_ID")] public int? TransportId{ get; set; }

    public Transport? Transport{ get; set; }

    public LandUnit(int movement, int cost, int attack, int defense){
        Movement = movement;
        Cost = cost;
        Attack = attack;
        Defense = defense;
    }

    public override ARegion? GetLocation() => Region;

    protected override bool SetLocation(ARegion region){
        if (region.IsLandRegion()){
            Region = (LandRegion)region;
            return true;
        }
        return false;
    }

    public override ARegion? GetTarget() => Target;
    
    public override bool CanTarget(EPhase phase,ARegion target) => GetPossibleTargets(phase).Contains(target);

    public override bool SetTarget(EPhase phase,ARegion region){
        if (CanTarget(phase,region)){
            Target = (LandRegion)region;
            return true;
        }
        return false;
    }

    public override void ClearTarget() => Target = null;

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
            if (neighbour.Neighbour.IsWaterRegion()) continue;
            if (neighbour.Neighbour.ContainsEnemies(Nation)) continue;
            LandRegion neigh = (LandRegion)neighbour.Neighbour;
            if (neigh.Nation == Nation || neigh.Nation.Allies.Any(a => a.Ally == Nation)){
                regions.Add(neighbour.Neighbour);
                regions.UnionWith(GetTargetsForNonCombatMove(distance - 1, neighbour.Neighbour));
            }
        }
        return regions.ToList();
    }

    protected override List<ARegion> GetTargetsForCombatMove(int distance, ARegion region){
        if (distance <= 0) return new List<ARegion>();
        //Anti Air can only move in the Non Combat Movement Phase
        if (Type == EUnitType.ANTI_AIR) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        ARegion location = region ?? Region;
        foreach (var neighbour in location.Neighbours){
            
            if (neighbour.Neighbour.IsWaterRegion()) continue;
            
            LandRegion neigh = (LandRegion)neighbour.Neighbour;
            
            //Units other than Tanks must end their attack on an enemy Field
            if (distance == 1 && Type != EUnitType.TANK && !neigh.IsHostile(Nation)) continue;
            
            //Units other than Tanks cant move through Hostile Fields to attack
            if(distance > 1 && Type != EUnitType.TANK && neigh.IsHostile(Nation)) continue;
            
            //Tanks can blitz through unoccupied enemy Fields
            if(distance > 1 && Type == EUnitType.TANK && neigh.ContainsEnemies(Nation) || neigh.Factory != null) continue;
            
            regions.Add(neighbour.Neighbour);
            regions.UnionWith(GetTargetsForCombatMove(distance - 1, neighbour.Neighbour));
        }

        return regions.ToList();
    }
}