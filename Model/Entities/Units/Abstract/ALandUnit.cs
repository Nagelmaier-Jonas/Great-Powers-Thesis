﻿using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Factories;

namespace Model.Entities.Units.Abstract;

[Table("LAND_UNITS_BT")]
public abstract class ALandUnit : AUnit{
    [Column("LOCATION_ID")] 
    public int? RegionId{ get; set; }

    public LandRegion? Region{ get; set; }

    [Column("TRANSPORT_ID")] 
    public int? TransportId{ get; set; }

    public Transport? Transport{ get; set; }
    public Transport? GetTransporter() => Transport;

    public override bool MoveToTarget(EPhase phase){
        if (!CanMove) return false;
        
        List<ARegion> path = GetPath(phase, Target);
        if (path.Count == 0) return false;

        if (phase == EPhase.ConductCombat) CanMove = false;
        
        if (Target.IsWaterRegion()){
            Transport transport = Target.GetOpenTransports(Nation, phase).FirstOrDefault();
            if (transport is null) return false;
            Transport = transport;
            TransportId = transport.Id;
        }
        else{
            SetLocation(Target);
        }

        CurrentMovement -= path.Count;
        RemoveTarget();
        
        return true;
    }

    public override ARegion? GetLocation() => Region;

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