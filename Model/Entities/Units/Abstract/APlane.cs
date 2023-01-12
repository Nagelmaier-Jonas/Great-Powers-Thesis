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

    public override ARegion? GetLocation() => Region;

    public override bool SetLocation(ARegion target){
        Region = target;
        return true;
    }

    public override List<AUnit> GetSubUnits() => null;

    public bool CanLand(ARegion region){
        if (region.GetOwner() != null){
            if (region.GetOwner() == Nation || Nation.Allies.Any(a => a.Ally == region.GetOwner())) return true;
        }
        if (region.GetOpenAircraftCarriers(Nation).Count != 0) return true;
        return false;
    }

    public List<ARegion> GetClosestLandingSpots(ARegion region, int movement, int distance = 1){
        if (distance > movement) return new List<ARegion>();
        List<ARegion> landingSpots = new List<ARegion>();
        landingSpots.AddRange(GetTargetsForMovement(distance, region, EPhase.NonCombatMove));
        if (landingSpots.Count == 0) landingSpots.AddRange(GetClosestLandingSpots(region,movement, distance + 1));
        return landingSpots;
    }
    
    protected override bool CheckForMovementRestrictions(int distance, Neighbours target, EPhase phase){
        LandRegion neigh = new LandRegion();
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanLand(target.Neighbour) && distance == 1) break;
                return true;
            case EPhase.CombatMove:
                //A Plane can only attack a Field if it will still have enough Movement left to land in the Non Combat Movement Phase
                if(distance == 1 && GetClosestLandingSpots(target.Neighbour, Movement - GetDistanceToTarget(EPhase.NonCombatMove,1,GetLocation(),true)).Count == 0) break;
                
                //A Plane cant capture Territory, only fight Units
                if(distance == 1 && !target.Neighbour.ContainsAnyEnemies(Nation)) break;
                
                return true;
        }
        return false;
    }
    
    public override bool IsPlane() => true;
}