using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units.Abstract;

[Table("LAND_UNITS_BT")]
public abstract class ALandUnit : AUnit{
    [Column("LOCATION_ID")] public int? RegionId{ get; set; }

    public LandRegion? Region{ get; set; }

    [Column("TRANSPORT_ID")] public int? TransportId{ get; set; }

    public Transport? Transport{ get; set; }
    
    public override bool MoveToTarget(EPhase phase){
        if (GetPathToTarget(phase).Count == 0) return false;
        if (Target.IsWaterRegion()){
            Transport transport = Target.GetOpenTransports(Nation,phase).FirstOrDefault();
            if (transport is null) return false;
            Transport = transport;
        }
        CurrentMovement -= GetDistanceToTarget(phase);
        SetLocation(Target);
        return true;
    }

    public override ARegion? GetLocation() => Region;

    public override bool SetLocation(ARegion region){
        if (region.IsLandRegion()){
            Region = (LandRegion)region;
            return true;
        }
        return false;
    }

    public override List<AUnit> GetSubUnits() => null;
    
    public override bool IsLandUnit() => true;
    
    public override bool IsCargo() => Transport is not null;
}