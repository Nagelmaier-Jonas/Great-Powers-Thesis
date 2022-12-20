using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Model.Entities.Units;

namespace Model.Entities.Regions;

[Table("REGIONS_BT")]
public abstract class ARegion{
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("NAME", TypeName = "VARCHAR(45)")]
    public string? Name{ get; set; }

    public List<Neighbours>? Neighbours{ get; set; }

    [Column("REGION_TYPE", TypeName = "VARCHAR(45)")]
    public ERegionType? Type{ get; set; }

    public List<Plane> StationedPlanes{ get; set; } = new List<Plane>();
    public List<Plane>? IncomingPlanes{ get; set; }

    #region Neighbours

    private List<ARegion> GetNeighbours(int distance, bool excludeSource = true, bool filterType = false,
        ERegionType type = ERegionType.LAND){
        if (distance <= 0) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        foreach (var neighbour in Neighbours){
            if (neighbour.Neighbour.Type == type || !filterType){
                regions.Add(neighbour.Neighbour);
                regions.UnionWith(neighbour.Neighbour.GetNeighbours(distance - 1, false, filterType, type));
            }
        }

        if (excludeSource) regions.Remove(this);
        return regions.ToList();
    }

    public List<ARegion> GetAllNeighbours(int distance) =>
        GetNeighbours(distance);

    public List<ARegion> GetAllNeighboursWithSource(int distance) =>
        GetNeighbours(distance, false);

    public List<ARegion> GetNeighboursByType(int distance, ERegionType type) =>
        GetNeighbours(distance, true, true, type);

    public List<ARegion> GetNeighboursByTypeWithSource(int distance, ERegionType type) =>
        GetNeighbours(distance, false, true, type);

    #endregion

    #region FriendlyNeighbours

    protected List<ARegion> GetFriendlyNeighbours(int distance, Nation nation, bool excludeSource = true,
        bool filterType = false, ERegionType type = ERegionType.LAND){
        if (distance <= 0) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        foreach (var neighbour in Neighbours){
            if (neighbour.Neighbour.ContainsEnemies(nation)) continue;
            LandRegion? neigh = null;
            if (neighbour.Neighbour.Type == ERegionType.LAND) neigh = (LandRegion)neighbour.Neighbour;
            if (neighbour.Neighbour.Type == type || !filterType){
                if (neigh == null){
                    regions.Add(neighbour.Neighbour);
                    regions.UnionWith(
                        neighbour.Neighbour.GetFriendlyNeighbours(distance - 1, nation, false, filterType, type));
                    continue;
                }

                if (neigh.Nation == nation || neigh.Nation.Allies.Any(a => a.Ally == nation)){
                    regions.Add(neigh);
                    regions.UnionWith(
                        neighbour.Neighbour.GetFriendlyNeighbours(distance - 1, nation, false, filterType, type));
                }
            }
        }

        if (excludeSource && Type == ERegionType.LAND) regions.Remove((LandRegion)this);
        return regions.ToList();
    }

    public List<ARegion> GetAllFriendlyNeighbours(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation);

    public List<ARegion> GetAllFriendlyNeighboursWithSource(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, false);

    public List<ARegion> GetFriendlyNeighboursByLand(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, true, true);

    public List<ARegion> GetFriendlyNeighboursByLandWithSource(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, false, true);

    #endregion

    #region Distance

    protected int GetDistance(ARegion target, Nation? nation = null, bool filterLand = false, int distance = 1){
        if (distance == 300) return 0;
        if (nation != null && !filterLand){
            if (GetAllFriendlyNeighbours(distance, nation).Contains(target)) return distance;
        }

        if (filterLand && nation == null){
            if (GetNeighboursByType(distance, ERegionType.LAND).Contains(target)) return distance;
        }

        if (filterLand && nation != null){
            if (GetFriendlyNeighboursByLand(distance, nation).Contains(target)) return distance;
        }

        if (!filterLand && nation == null){
            if (GetAllNeighbours(distance).Contains(target)) return distance;
        }

        return GetDistance(target, nation, filterLand, distance + 1);
    }

    public int GetMinimalDistance(ARegion target) => GetDistance(target);
    public int GetMinimalDistanceByFriendlies(ARegion target, Nation nation) => GetDistance(target, nation);
    public int GetMinimalDistanceByLand(ARegion target) => GetDistance(target, null, true);
    public int GetMinimalDistanceByFriendlyLand(ARegion target, Nation nation) => GetDistance(target, nation, true);

    #endregion

    protected List<ARegion> GetPath(ARegion target, Nation? nation = null, bool filterLand = false){
        int distance = GetDistance(target, nation, filterLand);
        if (distance == 0) return new List<ARegion>();

        List<ARegion> path = new List<ARegion>();
        path.Add(this);

        List<ARegion> neighbours = new List<ARegion>();

        if (nation != null && !filterLand){
            neighbours = GetAllFriendlyNeighbours(1, nation);
        }

        if (filterLand && nation == null){
            neighbours = GetNeighboursByType(1, ERegionType.LAND);
        }

        if (filterLand && nation != null){
            neighbours = GetFriendlyNeighboursByLand(1, nation);
        }

        if (!filterLand && nation == null){
            neighbours = GetAllNeighbours(1);
        }

        foreach (var neigh in neighbours){
            if (path.Any(r => r.GetDistance(target, nation, filterLand) == distance - 1)) break;
            if (neigh.GetDistance(target, nation, filterLand) == distance - 1)
                path.AddRange(neigh.GetPath(target, nation, filterLand));
        }

        return path;
    }

    public List<ARegion> GetPathToTargetByFriendlies(ARegion target, Nation nation) => GetPath(target, nation);
    public List<ARegion> GetPathToTargetByLand(ARegion target) => GetPath(target, null, true);
    public List<ARegion> GetPathToTargetByFriendlyLand(ARegion target, Nation nation) => GetPath(target, nation, true);

    public virtual List<AUnit> GetStationedUnits() =>
        throw new NotImplementedException();

    public List<AUnit> GetStationedFriendlyUnits(Nation nation) => GetStationedUnits()
        .Where(u => u.Nation == nation || u.Nation.Allies.Any(a => a.Ally == nation)).ToList();

    public bool ContainsEnemies(Nation nation) => GetStationedUnits()
        .Any(u => u.Nation != nation && u.Nation.Allies.All(a => a.Ally != nation));

    public Dictionary<EUnitType, int> GetStationedUnitCounts(){
        Dictionary<EUnitType, int> counts = new Dictionary<EUnitType, int>();
        foreach (var unit in GetStationedUnits()){
            counts[unit.Type] += 1;
        }
        return counts;
    }
}