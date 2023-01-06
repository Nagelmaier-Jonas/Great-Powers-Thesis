using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("PLANES")]
public class Plane : AUnit{
    [Column("LOCATION_ID")] public int RegionId{ get; set; }

    public ARegion Region{ get; set; }

    [Column("TARGET_ID")] public int? TargetId{ get; set; }

    public ARegion? Target{ get; set; }

    [Column("AIRCRAFT_CARRIER_ID")] public int? AircraftCarrierId{ get; set; }

    public AircraftCarrier? AircraftCarrier{ get; set; }

    public Plane(int movement, int cost, int attack, int defense){
        Movement = movement;
        Cost = cost;
        Attack = attack;
        Defense = defense;
    }

    public override ARegion GetLocation() => Region;

    protected override bool SetLocation(ARegion target){
        if (CanLand(target)){
            Region = target;
            return true;
        }

        return false;
    }

    public override ARegion GetTarget() => Target;

    public override bool CanTarget(EPhase phase, ARegion target) => GetPossibleTargets(phase).Contains(target);

    public override bool SetTarget(EPhase phase, ARegion region){
        if (CanTarget(phase, region)){
            Target = region;
            return true;
        }
        return false;
    }

    public override void ClearTarget() => Target = null;

    public bool CanLand(ARegion region){
        if (region.GetOwner() != null){
            if (region.GetOwner() == Nation || Nation.Allies.Any(a => a.Ally == region.GetOwner())) return true;
        }
        if (region.GetOpenAircraftCarriers(Nation).Count != 0) return true;
        return false;
    }

    public List<ARegion> GetClosestLandingSpots(ARegion region, int movement, int distance = 1){
        if (distance == movement) return new List<ARegion>();
        List<ARegion> landingSports = new List<ARegion>();
        landingSports.AddRange(GetTargetsForNonCombatMove(distance, region));
        if (landingSports.Count == 0) landingSports.AddRange(GetClosestLandingSpots(region,movement, distance + 1));
        return landingSports;
    }

    public override List<ARegion> GetPossibleTargets(EPhase phase){
        switch (phase){
            case EPhase.NonCombatMove:
                return GetTargetsForNonCombatMove(CurrentMovement);
            case EPhase.CombatMove:
                return GetTargetsForCombatMove(CurrentMovement);
        }

        return new List<ARegion>();
    }

    protected override List<ARegion> GetTargetsForNonCombatMove(int distance, ARegion? region = null){
        if (distance <= 0) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        ARegion location = region ?? Region;
        foreach (var neighbour in location.Neighbours){
            if (!CanLand(neighbour.Neighbour) && distance == 1) continue;
            regions.Add(neighbour.Neighbour);
            regions.UnionWith(GetTargetsForNonCombatMove(distance - 1, neighbour.Neighbour));
        }

        return regions.ToList();
    }

    protected override List<ARegion> GetTargetsForCombatMove(int distance, ARegion? region = null){
        if (distance <= 0) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        ARegion location = region ?? Region;
        foreach (var neighbour in location.Neighbours){
            //A Plane can only attack a Field if it will still have enough Movement left to land in the Non Combat Movement Phase
            if(distance == 1 && GetClosestLandingSpots(neighbour.Neighbour, CurrentMovement - GetDistanceToTarget(EPhase.NonCombatMove)).Count == 0) continue;
            regions.Add(neighbour.Neighbour);
            regions.UnionWith(GetTargetsForCombatMove(distance - 1, neighbour.Neighbour));
        }

        return regions.ToList();
    }
}