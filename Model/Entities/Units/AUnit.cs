using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("UNITS_BT")]
public abstract class AUnit{

    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("UNIT_TYPE", TypeName = "VARCHAR(45)")]
    public EUnitType Type{ get; set; }

    [Column("NATION_ID")]
    public int NationId{ get; set; }
    
    public Nation Nation{ get; set; }
    
    [Column("CURRENT_MOVEMENT")] 
    public int CurrentMovement{ get; set; }
    
    public int Movement{ get; private protected set; }
    
    public int Cost{ get; private protected set; }
    public int Attack{ get; private protected set; }
    public int Defense{ get; private protected set; }

    public abstract ARegion? GetLocation();
    protected abstract bool SetLocation(ARegion region);
    public abstract ARegion? GetTarget();
    public abstract bool SetTarget(EPhase phase,ARegion region);
    public abstract bool CanTarget(EPhase phase,ARegion target);
    public virtual List<AUnit> GetSubUnits() => new ();
    
    public bool MoveToTarget(EPhase phase){
        if (GetTarget() == null) return false;
        CurrentMovement -= GetDistanceToTarget(phase);
        SetLocation(GetTarget());
        SetTarget(phase,null);
        return true;
    }

    public abstract List<ARegion> GetPossibleTargets(EPhase phase);
    protected abstract List<ARegion> GetTargetsForNonCombatMove(int distance, ARegion region);
    protected abstract List<ARegion> GetTargetsForCombatMove(int distance, ARegion region);
    
    public int GetDistanceToTarget(EPhase phase,int distance = 1, ARegion? region = null){
        if (GetTarget() == null) return 0;
        if (distance > CurrentMovement) return 0;
        if (GetLocation() == null) return 0;

        ARegion location = region ?? GetLocation();
        
        switch (phase){
            case EPhase.NonCombatMove:
                if (GetTargetsForNonCombatMove(distance,location).Contains(GetTarget())) return distance;
                break;
            case EPhase.CombatMove:
                if (GetTargetsForCombatMove(distance,location).Contains(GetTarget())) return distance;
                break;
        }
        return GetDistanceToTarget(phase,distance + 1, location);
    }

    public List<ARegion> GetPathToTarget(EPhase phase, ARegion? region = null){
        if (GetTarget() == null) return new List<ARegion>();
        if (GetLocation() == null) return new List<ARegion>();
        
        ARegion location = region ?? GetLocation();
        
        int distance = GetDistanceToTarget(phase, 1, location);
        
        if (distance == 0) return new List<ARegion>();

        List<ARegion> path = new List<ARegion>();
        path.Add(location);
        if (distance == 1) path.Add(GetTarget());
        
        List<ARegion> neighbours = new List<ARegion>();

        switch (phase){
            case EPhase.NonCombatMove:
                neighbours = GetTargetsForNonCombatMove(CurrentMovement, location);
                break;
            case EPhase.CombatMove:
                neighbours = GetTargetsForCombatMove(CurrentMovement, location);
                break;
        }
        
        foreach (var neigh in neighbours){
            if (path.Any(r => GetDistanceToTarget(phase, 1, r) == distance - 1)) break;
            if (GetDistanceToTarget(phase,1,neigh) == distance - 1)
                path.AddRange(GetPathToTarget(phase,neigh));
        }

        return path;
    }
}