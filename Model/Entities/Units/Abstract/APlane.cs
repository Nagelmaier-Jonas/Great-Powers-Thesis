using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design.Serialization;
using Model.Entities.Regions;

namespace Model.Entities.Units.Abstract;

[Table("PLANES_BT")]
public abstract class APlane : AUnit{
    [Column("LOCATION_ID")] public int? RegionId{ get; set; }

    public ARegion? Region{ get; set; }

    [Column("AIRCRAFT_CARRIER_ID")] public int? AircraftCarrierId{ get; set; }

    public AircraftCarrier? AircraftCarrier{ get; set; }

    public AircraftCarrier? GetAircraftCarrier() => AircraftCarrier;
    public override ARegion? GetLocation() => Region;

    public override bool SetLocation(ARegion region){
        if (region.IsLandRegion()){
            Region = (LandRegion)region;
            RegionId = region.Id;
            return true;
        }

        return false;
    }

    public override bool MoveToTarget(EPhase phase){
        if (!CanMove) return false;
        
        List<ARegion> path = GetPath(phase);
        if (path.Count == 0) return false;

        if (phase == EPhase.CombatMove) CanMove = false;
        
        if (Target.IsWaterRegion()){
            AircraftCarrier carrier = Target.GetOpenAircraftCarriers(Nation).FirstOrDefault();
            if (carrier is null) return false;
            AircraftCarrier = carrier;
            AircraftCarrierId = carrier.Id;
        }

        CurrentMovement -= path.Count;
        SetLocation(Target);
        RemoveTarget();
        return true;
    }

    public bool CanLand(ARegion region){
        if (region.GetOwner() == null) return region.GetOpenAircraftCarriers(Nation).Count != 0;
        return region.GetOwner() == Nation || Nation.Allies.Any(a => a.Ally == region.GetOwner());
    }

    public bool HasReachableLandingSpots(ARegion region, int movementLeft){
        if (movementLeft == 0) return false;

        List<ARegion> regions = region.Neighbours.Select(n => n.Neighbour).ToList();
        return regions.Where(CanLand).ToList().Count > 0 ||
               regions.Select(r => HasReachableLandingSpots(r, movementLeft - 1)).FirstOrDefault();
    }

    protected override bool CheckForMovementRestrictions(Node target, Node previous, EPhase phase){
        switch (phase){
            case EPhase.NonCombatMove:
                return true;
            case EPhase.CombatMove:
                
                //A Plane can only attack a Field if it will still have enough Movement left to land in the Non Combat Movement Phase
                if (!HasReachableLandingSpots(target.Region, CurrentMovement - target.Distance)) break;

                //A Plane cant capture Territory, only fight Units
                if (target.Distance == 4 && !target.Region.ContainsAnyEnemies(Nation)) break;

                return true;
        }
        return false;
    }

    public override bool IsPlane() => true;
    public override bool IsCargo() => AircraftCarrier is not null;
    public override bool CanAttack(AUnit unit) => !unit.IsSubmarine();
}