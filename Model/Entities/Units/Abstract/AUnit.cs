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
    
    [Column("CASUALTY_ID")] public int? CasualtyId{ get; set; }

    public Battle? Casualty{ get; set; }

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

    public List<ARegion> GetPossibleRetreatTargetsList(List<ARegion> previousRegions) =>
        throw new NotImplementedException();


    protected bool CanTarget(EPhase phase, ARegion target){
        List<ARegion> path = GetPath(phase, target);
        return path.Last().Id == target.Id;
    }

    public virtual bool SetTarget(EPhase phase, ARegion target){
        if (!CanTarget(phase, target)) return false;
        Target = target;
        TargetId = target.Id;
        return true;
    }

    public void RemoveTarget() => Target = null;

    public bool HasTarget() => Target is not null;


    public virtual bool MoveToTarget(EPhase phase) => false;

    public List<ARegion> GetPossibleTargets(EPhase phase){
        List<ARegion> regions = GetPath(phase);
        if (phase == EPhase.CombatMove) return regions.Where(r => r.IsHostile(Nation) || r.ContainsAnyEnemies(Nation)).ToList();
        if (!IsPlane() || phase != EPhase.NonCombatMove) return regions;
        APlane plane = (APlane)this;
        return regions.Where(r => plane.CanLand(r)).ToList();
    }

    public List<ARegion> GetPathToTarget(EPhase phase) => Target is null ? new List<ARegion>() : GetPath(phase, Target);


    protected abstract bool CheckForMovementRestrictions(Node target, Node previous, EPhase phase);

    public List<ARegion> GetAllegibleNeighbours(Node region, EPhase phase) => region.Region.Neighbours
        .Select(n => n.Neighbour)
        .Where(r => CheckForMovementRestrictions(new Node(r, region.Distance + 1), region, phase))
        .ToList();

    public List<ARegion> GetPath(EPhase phase, ARegion? target = null, ARegion? start = null){
        if (GetLocation() is null && start is null) return new List<ARegion>();

        ARegion source = start ?? GetLocation();

        List<Node> travelled = new List<Node>();
        travelled.Add(new Node(source, 0));

        List<Node> next = GetAllegibleNeighbours(new Node(source, 0), phase).Select(neigh => new Node(neigh, 1))
            .ToList();

        while (next.Count > 0){
            Node current = next.First();
            next.Remove(current);

            travelled.Add(current);

            travelled.ForEach(t => {
                if (!t.Equals(current)) return;
                if (t.Distance > current.Distance) t.Distance = current.Distance;
            });

            if (current.Distance == CurrentMovement) continue;

            List<Node> neighbours = GetAllegibleNeighbours(current, phase)
                .Select(neigh => new Node(neigh, current.Distance + 1))
                .Where(nextNode => travelled.All(t => !t.Equals(nextNode) || t.Distance > nextNode.Distance)).ToList();

            next.InsertRange(0, neighbours);
        }

        if (travelled.Count == 0) return new List<ARegion>();

        travelled.OrderBy(t => t.Distance);

        //Path to specified Target
        if (target is not null){
            List<List<Node>> permutations = new List<List<Node>>();
            permutations.Add(new List<Node>{ travelled.First() });

            List<List<Node>> temp = new List<List<Node>>();

            foreach (var n in travelled.Where(n => permutations.All(p => !p.Last().Equals(n)))){
                temp = permutations.Where(p =>
                    p.Last().Region.Neighbours.Any(neigh => neigh.Neighbour == n.Region) &&
                    n.Distance > p.Last().Distance).ToList();
                foreach (var copy in temp.Select(t => new List<Node>(t))){
                    copy.Add(n);
                    permutations.Add(copy);
                }
            }

            temp = permutations.Where(p => p.Last().Region.Id == target.Id).ToList();

            List<Node> temp2 = temp.FirstOrDefault(t1 => temp.All(t2 => t2.Count >= t1.Count));

            return temp2 is null ? new List<ARegion>() : temp2.Select(n => n.Region).ToList();
        }

        //All regions if target not defined
        travelled.RemoveAll(p => p.Region == source);
        return travelled.Select(p => p.Region).ToList();
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