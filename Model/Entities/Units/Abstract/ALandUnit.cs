using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Factories;

namespace Model.Entities.Units.Abstract;

[Table("LAND_UNITS_BT")]
public abstract class ALandUnit : AUnit{
    [Column("LOCATION_ID")] public int? RegionId{ get; set; }

    public LandRegion? Region{ get; set; }

    [Column("PREVIOS_LOCATION_ID")] public int? PreviousLocationId{ get; set; }

    public LandRegion? PreviousLocation{ get; set; }

    [Column("TRANSPORT_ID")] public int? TransportId{ get; set; }

    public Transport? Transport{ get; set; }
    public Transport? GetTransporter() => Transport;

    public override bool MoveToTarget(EPhase phase){
        List<ARegion> path = GetPathToTarget(phase);
        if (path.Count == 0) return false;
        if (Target.IsWaterRegion()){
            Transport transport = Target.GetOpenTransports(Nation, phase).FirstOrDefault();
            if (transport is null) return false;
            Transport = transport;
        }

        CurrentMovement -= path.Count;
        PreviousLocation = Region;
        SetLocation(Target);
        RemoveTarget();
        if (phase == EPhase.CombatMove){
            CanMove = false;
        }

        return true;
    }

    public override ARegion? GetLocation() => Region;

    public override ARegion? GetPreviousLocation() => PreviousLocation;

    public override bool SetLocation(ARegion region){
        if (region.IsLandRegion()){
            Region = (LandRegion)region;
            RegionId = region.Id;
            return true;
        }

        return false;
    }

    public override List<AUnit> GetSubUnits() => null;

    public override bool IsLandUnit() => true;

    public override bool IsCargo() => Transport is not null;
}