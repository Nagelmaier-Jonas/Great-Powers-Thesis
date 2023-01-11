using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("PLANES_BT")]
public abstract class APlane : AUnit{
    [Column("LOCATION_ID")] public int RegionId{ get; set; }

    public ARegion Region{ get; set; }

    [Column("AIRCRAFT_CARRIER_ID")] public int? AircraftCarrierId{ get; set; }

    public AircraftCarrier? AircraftCarrier{ get; set; }

    public override ARegion GetLocation() => Region;

    protected override bool SetLocation(ARegion target){
        if (CanLand(target)){
            Region = target;
            return true;
        }

        return false;
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
        if (distance == movement) return new List<ARegion>();
        List<ARegion> landingSports = new List<ARegion>();
        landingSports.AddRange(GetTargetsForMovement(distance, region, EPhase.NonCombatMove));
        if (landingSports.Count == 0) landingSports.AddRange(GetClosestLandingSpots(region,movement, distance + 1));
        return landingSports;
    }
    
    protected override bool CheckForMovementRestrictions(int distance, Neighbours target, EPhase phase){
        LandRegion neigh = new LandRegion();
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanLand(target.Neighbour) && distance == 1) break;
                return true;
            case EPhase.CombatMove:
                //A Plane can only attack a Field if it will still have enough Movement left to land in the Non Combat Movement Phase
                if(distance == 1 && GetClosestLandingSpots(target.Neighbour, CurrentMovement - GetDistanceToTarget(EPhase.NonCombatMove)).Count == 0) break;
                
                return true;
        }
        return false;
    }
}