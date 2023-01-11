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
    
    [Column("TARGET_ID")] 
    public int? TargetId{ get; set; }

    public ARegion? Target{ get; protected set; }
    
    [Column("CURRENT_MOVEMENT")] 
    public int CurrentMovement{ get; set; }

    [Column("CAN_MOVE", TypeName = "TINYINT")]
    public bool CanMove{ get; set; } = true;
    
    [NotMapped]
    public virtual int Movement{ get; protected set; }
    [NotMapped]
    public virtual int Cost{ get; protected set; }
    [NotMapped]
    public virtual int Attack{ get; protected set; }
    [NotMapped]
    public virtual int Defense{ get; protected set; }
    
    public int GetCost() => Cost;

    public abstract ARegion? GetLocation();
    protected abstract bool SetLocation(ARegion region);
    public abstract List<AUnit> GetSubUnits();

    public bool CanTarget(EPhase phase, ARegion target){
        ARegion temp = Target;
        Target = target;
        if (GetPathToTarget(phase).Count > 0 && GetPathToTarget(phase).Contains(target)){
            Target = temp;
            return true;
        }
        Target = temp;
        return false;
    }

    public bool SetTarget(EPhase phase, ARegion target){
        if (!CanTarget(phase, target)) return false;
        Target = target;
        return true;
    }
    
    public virtual bool MoveToTarget(EPhase phase){
        if (Target == null) return false;
        if (GetLocation() == null) return false;
        CurrentMovement -= GetDistanceToTarget(phase);
        SetLocation(Target);
        return true;
    }


    public List<ARegion> GetPossibleTargets(EPhase phase){
        List<ARegion> regions = GetTargetsForMovement(CurrentMovement, GetLocation(), phase);
        List<ARegion> targets = new List<ARegion>();

        foreach (var region in regions){
            ARegion temp = Target;
            Target = region;
            if (GetPathToTarget(phase).Count > 0 && GetPathToTarget(phase).Contains(region)) targets.Add(region);
            Target = temp;
        }

        return targets;
    } 
    
    protected List<ARegion> GetTargetsForMovement(int distance, ARegion region, EPhase phase){
        if (distance <= 0) return new List<ARegion>();
        if (region == null) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        foreach (var neighbour in region.Neighbours){
            if(!CheckForMovementRestrictions(distance,neighbour,phase)) continue;
            regions.Add(neighbour.Neighbour);
            regions.UnionWith(GetTargetsForMovement(distance - 1, neighbour.Neighbour, phase));
        }
        regions.Remove(GetLocation());
        return regions.ToList();
    }

    protected abstract bool CheckForMovementRestrictions(int distance, Neighbours target, EPhase phase);
    
    public int GetDistanceToTarget(EPhase phase,int distance = 1, ARegion? region = null){
        if (Target == null) return 0;
        if (distance > CurrentMovement) return 0;
        if (GetLocation() == null) return 0;

        ARegion location = region ?? GetLocation();
        
        if (GetTargetsForMovement(distance,location, phase).Contains(Target)) return distance;
                
        return GetDistanceToTarget(phase,distance + 1, location);
    }

    public List<ARegion> GetPathToTarget(EPhase phase, ARegion? region = null){
        if (Target == null) return new List<ARegion>();
        if (GetLocation() == null) return new List<ARegion>();
        
        ARegion location = region ?? GetLocation();
        
        int distance = GetDistanceToTarget(phase, 1, location);
        
        if (distance == 0) return new List<ARegion>();

        List<ARegion> path = new List<ARegion>();
        path.Add(location);
        
        List<ARegion> neighbours = new List<ARegion>();

        neighbours = GetTargetsForMovement(distance, location, phase);

        if (distance == 1){
            if (neighbours.Contains(Target)){
                path.Add(Target);
                return path;
            }
            return new List<ARegion>();
        }
        
        foreach (var neigh in neighbours){
            if (path.Any(r => GetDistanceToTarget(phase, 1, r) == distance - 1)) break;
            if (GetDistanceToTarget(phase,1,neigh) == distance - 1)
                path.AddRange(GetPathToTarget(phase,neigh));
        }

        return path;
    }
}