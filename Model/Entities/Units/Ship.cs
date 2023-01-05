using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("SHIPS_BT")]
public class Ship : AUnit{
    [Column("LOCATION_ID")]
    public int RegionId{ get; set; }

    public WaterRegion Region{ get; set; }
    
    [Column("TARGET_ID")]
    public int? TargetId{ get; set; }
    
    public WaterRegion? Target{ get; set; }
    
    public Ship(int movement, int cost, int attack, int defense){
        Movement = movement;
        Cost = cost;
        Attack = attack;
        Defense = defense;
    }
    
    public override ARegion GetLocation() => Region;

    protected override bool SetLocation(ARegion target){
        if (target.IsWaterRegion()){
            Region = (WaterRegion)target;
            return true;
        }
        return false;
    }

    public override ARegion GetTarget() => Target;
    
    public override bool CanTarget(EPhase phase,ARegion target) => GetPossibleTargets(phase).Contains(target);
    
    public override bool SetTarget(EPhase phase,ARegion region){
        if (CanTarget(phase,region)){
            Target = (WaterRegion)region;
            return true;
        }
        return false;
    }


    
    public override List<ARegion> GetPossibleTargets(EPhase phase){
        switch (phase){
            case EPhase.NonCombatMove:
                return GetTargetsForNonCombatMove(CurrentMovement, Region);
            case EPhase.CombatMove:
                return GetTargetsForCombatMove(CurrentMovement, Region);
        }
        return new List<ARegion>();
    }

    protected override List<ARegion> GetTargetsForNonCombatMove(int distance, ARegion region){
        if (distance <= 0) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        ARegion location = region ?? Region;
        foreach (var neighbour in location.Neighbours){
            if (neighbour.Neighbour.IsLandRegion()) continue;
            if (neighbour.Neighbour.ContainsEnemies(Nation)) continue;
            regions.Add(neighbour.Neighbour);
            regions.UnionWith(GetTargetsForNonCombatMove(distance - 1, neighbour.Neighbour));
        }
        return regions.ToList();
    }

    protected override List<ARegion> GetTargetsForCombatMove(int distance, ARegion region){
        throw new NotImplementedException();
    }
}