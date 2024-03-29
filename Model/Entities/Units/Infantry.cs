﻿using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace Model.Entities.Units;

[Table("INFANTRY")]
public class Infantry : ALandUnit{
    public override int Movement{ get; protected set; } = 1;
    public override int Cost{ get; protected set; } = 3;
    public override int Attack{ get; protected set; } = 1;
    public override int Defense{ get; protected set; } = 2;
    protected override bool CheckForMovementRestrictions(Node target, Node previous, EPhase phase){
        if (target.Region.IsWaterRegion()){
            return target.Region.GetOpenTransports(Nation, phase).Count != 0;
        }
        LandRegion neigh = (LandRegion)target.Region;
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanMove) break;
                if (target.Region.IsHostile(Nation)) break;
                if (neigh.Nation != Nation && neigh.Nation.Allies.All(a => a.Ally != Nation)) break;
                return true;
            case EPhase.CombatMove:
                //Units other than Tanks must end their attack on an enemy Field
                if (!target.Region.IsHostile(Nation)) break;
                return true;
            case EPhase.ConductCombat:
                return true;
        }
        return false;
    }
    
    public override bool IsInfantry() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsInfantry();
    
    public override string ToString() => "Infantry";
    
    public override AUnit GetNewInstanceOfSameType() => LandUnitFactory.CreateInfantry(null, null);
}