using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Model.Entities.Regions;

namespace Model.Entities.Units.Abstract;

[Table("UNITS_BT")]
public abstract class AUnit{
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("NATION_ID")] public int NationId{ get; set; }

    public Nation Nation{ get; set; }

    [Column("TARGET_ID")] public int? TargetId{ get; set; }

    public ARegion? Target{ get; set; }

    [Column("CURRENT_MOVEMENT")] public int CurrentMovement{ get; set; }

    [Column("CAN_MOVE", TypeName = "TINYINT")]
    public bool CanMove{ get; set; } = true;

    [Column("AGGRESSOR_ID")] public int? AggressorId{ get; set; }

    public Battle? Aggressor{ get; set; }

    [Column("DEFENDER_ID")] public int? DefenderId{ get; set; }

    public Battle? Defender{ get; set; }

    [Column("HITPOINTS")] public virtual int HitPoints{ get; set; } = 1;

    [NotMapped] public virtual int Movement{ get; protected set; }
    [NotMapped] public virtual int Cost{ get; protected set; }
    [NotMapped] public virtual int Attack{ get; protected set; }
    [NotMapped] public virtual int Defense{ get; protected set; }

    public int GetCost() => Cost;

    public abstract ARegion? GetLocation();
    public abstract ARegion? GetPreviousLocation();
    public abstract bool SetLocation(ARegion region);
    public virtual List<AUnit> GetSubUnits() => null;

    public List<ARegion> GetPossibleRetreatTargets(List<ARegion> previousRegions) =>
        GetTargetsForMovement(1, GetLocation(), EPhase.NonCombatMove).Where(previousRegions.Contains)
            .ToList();


    private bool CanTarget(EPhase phase, ARegion target){
        ARegion temp = Target;
        Target = target;
        List<ARegion> path = GetPathToTarget(phase);
        Target = temp;
        return path.Contains(target);
    }


    public bool SetTarget(EPhase phase, ARegion target){
        if (!CanTarget(phase, target)) return false;
        Target = target;
        TargetId = target.Id;
        return true;
    }

    public void RemoveTarget() => Target = null;

    public bool HasTarget() => Target is not null;


    public virtual bool MoveToTarget(EPhase phase) => false;

    public List<ARegion> GetPossibleTargets(EPhase phase){
        List<ARegion> regions = GetTargetsForMovement(CurrentMovement, GetLocation(), phase);
        List<ARegion> targets = new List<ARegion>();

        foreach (var region in regions){
            ARegion temp = Target;
            Target = region;
            if (GetPathToTarget(phase).Contains(region)) targets.Add(region);
            Target = temp;
        }

        return targets;
    }

    protected abstract bool CheckForMovementRestrictions(int distance, Neighbours target, EPhase phase);

    protected List<ARegion> GetTargetsForMovement(int distance, ARegion region, EPhase phase, bool planeCheck = false,
        List<ARegion>? previous = null){
        if (distance <= 0) return new List<ARegion>();
        if (region == null) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        regions.Add(region);
        
        foreach (var neighbour in region.Neighbours){
            
            if (previous is not null){
                regions.UnionWith(previous);
                if(regions.Contains(neighbour.Neighbour)) continue;
            }

            if (!CheckForMovementRestrictions(distance, neighbour, phase) && !planeCheck) continue;
            
            regions.Add(neighbour.Neighbour);
            regions.UnionWith(GetTargetsForMovement(distance - 1, neighbour.Neighbour, phase, planeCheck,
                regions.ToList()));
        }

        regions.Remove(region);
        return regions.ToList();
    }

    protected int GetDistanceToTarget(EPhase phase, int distance = 1, ARegion? region = null, bool planeCheck = false){
        if (Target == null) return 0;
        if (distance > CurrentMovement) return 0;
        if (GetLocation() == null) return 0;

        ARegion location = region ?? GetLocation();

        if (GetTargetsForMovement(distance, location, phase, planeCheck).Contains(Target)) return distance;

        return GetDistanceToTarget(phase, distance + 1, location, planeCheck);
    }

    public List<ARegion> GetPathToTarget(EPhase phase, ARegion? region = null, List<ARegion>? previos = null){
        if (Target == null) return new List<ARegion>();
        if (GetLocation() == null) return new List<ARegion>();

        ARegion location = region ?? GetLocation();

        int distance = GetDistanceToTarget(phase, 1, location);

        if (distance == 0) return new List<ARegion>();

        List<ARegion> path = new List<ARegion>(){
            location
        };

        if (previos is not null) path.AddRange(previos);

        if (path.Any(r => GetDistanceToTarget(phase, 1, r) == distance - 1)) return new List<ARegion>();

        List<ARegion> neighbours = GetTargetsForMovement(distance, location, phase);

        if (distance == 1){
            if (neighbours.Contains(Target)){
                path.Add(Target);
                return path;
            }

            return new List<ARegion>();
        }


        foreach (var neigh in neighbours){
            if (GetDistanceToTarget(phase, 1, neigh) == distance - 1)
                path.AddRange(GetPathToTarget(phase, neigh, path));
        }

        return path;
    }

    public virtual bool IsLandUnit() => false;
    public virtual bool IsPlane() => false;
    public virtual bool IsShip() => false;
    public virtual bool IsInfantry() => false;
    public virtual bool IsTank() => false;
    public virtual bool IsArtillery() => false;
    public virtual bool IsAntiAir() => false;
    public virtual bool IsFighter() => false;
    public virtual bool IsBomber() => false;
    public virtual bool IsSubmarine() => false;
    public virtual bool IsDestroyer() => false;
    public virtual bool IsCruiser() => false;
    public virtual bool IsBattleship() => false;
    public virtual bool IsAircraftCarrier() => false;
    public virtual bool IsTransport() => false;
    public virtual bool IsFactory() => false;
    public virtual bool IsSameType(AUnit unit) => false;

    public int? GeIntFromDictionary(Dictionary<AUnit, int> dictionary) =>
        dictionary.FirstOrDefault(p => IsSameType(p.Key)).Value;

    public string? GetStringFromDictionary(Dictionary<AUnit, string> dictionary) =>
        dictionary.FirstOrDefault(p => IsSameType(p.Key)).Value;

    public Point? GetPointFromDictionary(Dictionary<AUnit, Point> dictionary) =>
        dictionary.FirstOrDefault(p => IsSameType(p.Key)).Value;

    public abstract AUnit GetNewInstanceOfSameType();

    public virtual bool IsCargo() => false;

    public virtual bool CanAttack(AUnit unit) => true;
}