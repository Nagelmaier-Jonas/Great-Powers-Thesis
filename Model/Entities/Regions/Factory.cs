﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Units;

namespace Model.Entities.Regions;

[Table("FACTORIES")]
public class Factory : AUnit{

    [Column("DAMAGE")]
    public int Damage{ get; set; }

    [Column("REGION_ID")]
    public int RegionId{ get; set; }

    public LandRegion Region{ get; set; }

    public override int Movement{ get; protected set; } = 0;
    public override int Cost{ get; protected set; } = 15;
    public override int Attack{ get; protected set; } = 0;
    public override int Defense{ get; protected set; } = 0;
    public int GetCost() => Cost;
    public override ARegion? GetLocation() => Region;

    protected override bool SetLocation(ARegion region) => false;

    public override List<AUnit> GetSubUnits() => null;

    protected override bool CheckForMovementRestrictions(int distance, Neighbours target, EPhase phase) => false;
}