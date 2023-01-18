﻿using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace Model.Entities.Units;

[Table("TANK")]
public class Tank : ALandUnit{

    public override int Movement{ get; protected set; } = 2;
    public override int Cost{ get; protected set; } = 6;
    public override int Attack{ get; protected set; } = 3;
    public override int Defense{ get; protected set; } = 3;

    protected override bool CheckForMovementRestrictions(int distance, Neighbours target, EPhase phase,bool planeCheck){
        if (target.Neighbour.IsWaterRegion()){
            if(target.Neighbour.GetOpenTransports(Nation, phase).Count == 0) return false;
            return true;
        }
        LandRegion neigh = (LandRegion)target.Neighbour;
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanMove) break;
                if (target.Neighbour.ContainsEnemies(Nation)) break;
                if (neigh.Nation != Nation && neigh.Nation.Allies.All(a => a.Ally != Nation)) break;
                return true;
            case EPhase.CombatMove:
                if (Target is not null && GetLocation().Neighbours.All(n => n.Neighbour != Target)){
                    if (!Target.IsHostile(Nation) && !target.Neighbour.IsHostile(Nation) && target.Neighbour != Target) break;
                    if (distance == 2 && target.Neighbour.ContainsEnemies(Nation)) break;
                }
                else{
                    if(!target.Neighbour.IsHostile(Nation)) break;
                }

                return true;
        }

        return false;
    }
    
    public override bool IsTank() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsTank();
    
    public override string ToString() => "Tank";
    
    public override AUnit GetNewInstanceOfSameType() => LandUnitFactory.CreateTank(null, null);
}