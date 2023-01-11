﻿using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("DESTROYER")]
public class Destroyer : AShip{
    public override int Movement{ get; protected set; } = 2;
    public override int Cost{ get; protected set; } = 8;
    public override int Attack{ get; protected set; } = 2;
    public override int Defense{ get; protected set; } = 2;

    protected override bool CheckForMovementRestrictions(int distance, Neighbours target, EPhase phase){
        switch (phase){
            case EPhase.NonCombatMove:
                if (!CanMove) break;
                if (target.Neighbour.IsLandRegion()) break;
                if (target.Neighbour.ContainsEnemies(Nation)) break;
                //Ships cant pass through canals if they arent owned by a friendly Nation
                if (target.CanalOwners.Any(o =>
                        o.CanalOwner.Nation != Nation || o.CanalOwner.Nation.Allies.All(a => a.Ally != Nation))) break;
                return true;
            case EPhase.CombatMove:
                //If a Ship doesnt support an amphibious assault, it has to end its attack on a Field containing Enemies, unless it started in an enemy Field and is escaping elsewhere
                if (distance != 1 && target.Neighbour.IsWaterRegion() &&
                    !target.Neighbour.ContainsAnyEnemies(Nation) && !Region.IsHostile(Nation)) break;
            
                //A Ship cant move through enemy Fields to attack, unless its a Submarine
                if (distance != 1 && target.Neighbour.IsHostile(Nation)) break;
            
                //Ships cant pass through canals if they arent owned by a friendly Nation
                if (target.CanalOwners.Any(o =>
                        o.CanalOwner.Nation != Nation || o.CanalOwner.Nation.Allies.All(a => a.Ally != Nation)))
                    break;
                return true;
        }

        return false;
    }
    public override List<AUnit> GetSubUnits() => null;
}