using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
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
    public string Name{ get; set; }

    public List<Neighbours> Neighbours{ get; set; }

    [Column("REGION_TYPE", TypeName = "VARCHAR(45)")]
    public ERegionType Type{ get; set; }

    [Column("IDENTIFIER", TypeName = "VARCHAR(45)")] public ERegion Identifier{ get; set; }
    
    [Column("POSITION_X")]
    public int? PositionX{ get; set; }
    
    [Column("POSITION_Y")]
    public int? PositionY{ get; set; }

    public List<Plane> StationedPlanes{ get; set; } = new List<Plane>();
    public List<Plane> IncomingPlanes{ get; set; } = new List<Plane>();

    #region Neighbours

    private List<ARegion> GetNeighbours(int distance, bool excludeSource = true, ERegionType? type = null){
        if (distance <= 0) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        foreach (var neighbour in Neighbours){
            if (type != null){
                if (neighbour.Neighbour.Type != type) continue;
            }
            regions.Add(neighbour.Neighbour);
            regions.UnionWith(neighbour.Neighbour.GetNeighbours(distance - 1, false, type));
        }
        if (excludeSource) regions.Remove(this);
        return regions.ToList();
    }

    public List<ARegion> GetAllNeighbours(int distance) =>
        GetNeighbours(distance);

    public List<ARegion> GetAllNeighboursWithSource(int distance) =>
        GetNeighbours(distance, false);

    public List<ARegion> GetLandNeighbours(int distance) =>
        GetNeighbours(distance, true, ERegionType.LAND);
    
    public List<ARegion> GetWaterNeighbours(int distance) =>
        GetNeighbours(distance, true, ERegionType.WATER);

    public List<ARegion> GetLandNeighboursWithSource(int distance) =>
        GetNeighbours(distance, false, ERegionType.LAND);
    public List<ARegion> GetWaterNeighboursWithSource(int distance) =>
        GetNeighbours(distance, false, ERegionType.WATER);

    #endregion

    #region FriendlyNeighbours

    protected List<ARegion> GetFriendlyNeighbours(int distance, Nation nation, bool excludeSource = true, ERegionType? type = null){
        HashSet<ARegion> regions = new HashSet<ARegion>();
        if (!excludeSource) regions.Add(this);
        if (distance == 0) return regions.ToList();
        foreach (var neighbour in Neighbours){
            if (neighbour.Neighbour.ContainsEnemies(nation)) continue;
            if (type != null){
                if (neighbour.Neighbour.Type != type) continue;
            }
            LandRegion? neigh = null;
            if (neighbour.Neighbour.Type == ERegionType.LAND) neigh = (LandRegion)neighbour.Neighbour;
            if (neigh == null){
                regions.Add(neighbour.Neighbour);
                regions.UnionWith(
                    neighbour.Neighbour.GetFriendlyNeighbours(distance - 1, nation, false, type));
                continue;
            }

            if (neigh.Nation == nation || neigh.Nation.Allies.Any(a => a.Ally == nation)){
                regions.Add(neigh);
                regions.UnionWith(
                    neighbour.Neighbour.GetFriendlyNeighbours(distance - 1, nation, false, type));
            }
        }

        if (excludeSource) regions.Remove(this);
        return regions.ToList();
    }

    public List<ARegion> GetAllFriendlyNeighbours(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation);

    public List<ARegion> GetAllFriendlyNeighboursWithSource(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, false);

    public List<ARegion> GetFriendlyLandNeighbours(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, true, ERegionType.LAND);
    public List<ARegion> GetFriendlyWaterNeighbours(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, true, ERegionType.WATER);

    public List<ARegion> GetFriendlyLandNeighboursWithSource(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, false, ERegionType.LAND);
    public List<ARegion> GetFriendlyWaterNeighboursWithSource(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, false, ERegionType.WATER);

    #endregion

    #region Distance

    protected int GetDistance(ARegion? target, Nation? nation = null, ERegionType? type = null, int maxDistance = 10,
        int distance = 1){
        if (target == null) return 0;
        if (distance > maxDistance) return 0;
        if (nation != null && type == null){
            if (GetAllFriendlyNeighbours(distance, nation).Contains(target)) return distance;
        }

        if (type != null && nation == null){
            switch (type){
                case ERegionType.LAND:
                    if (GetLandNeighbours(distance).Contains(target)) return distance;
                    break;
                case ERegionType.WATER:
                    if (GetWaterNeighbours(distance).Contains(target)) return distance;
                    break;
            }
        }

        if (type != null && nation != null){
            switch (type){
                case ERegionType.LAND:
                    if (GetFriendlyLandNeighbours(distance, nation).Contains(target)) return distance;
                    break;
                case ERegionType.WATER:
                    if (GetFriendlyWaterNeighbours(distance, nation).Contains(target)) return distance;
                    break;
            }
        }

        if (type == null && nation == null){
            if (GetAllNeighbours(distance).Contains(target)) return distance;
        }

        return GetDistance(target, nation, type, maxDistance, distance + 1);
    }

    public int GetMinimalDistance(ARegion target) => GetDistance(target);

    public int GetMinimalDistanceWithMax(ARegion target, int maxDistance) =>
        GetDistance(target, null, null, maxDistance);

    public int GetMinimalDistanceByFriendlies(ARegion target, Nation nation) => GetDistance(target, nation);

    public int GetMinimalDistanceByFriendliesWithMax(ARegion target, Nation nation, int maxDistance) =>
        GetDistance(target, nation, null, maxDistance);

    public int GetMinimalDistanceByLand(ARegion target) => GetDistance(target, null, ERegionType.LAND);
    public int GetMinimalDistanceByWater(ARegion target) => GetDistance(target, null, ERegionType.WATER);

    public int GetMinimalDistanceByLandWithMax(ARegion target, int maxDistance) =>
        GetDistance(target, null, ERegionType.LAND, maxDistance);
    public int GetMinimalDistanceByWaterWithMax(ARegion target, int maxDistance) =>
        GetDistance(target, null, ERegionType.WATER, maxDistance);

    public int GetMinimalDistanceByFriendlyLand(ARegion target, Nation nation) => GetDistance(target, nation, ERegionType.LAND);
    public int GetMinimalDistanceByFriendlyWater(ARegion target, Nation nation) => GetDistance(target, nation, ERegionType.WATER);

    public int GetMinimalDistanceByFriendlyLandWithMax(ARegion target, Nation nation, int maxDistance) =>
        GetDistance(target, nation, ERegionType.LAND, maxDistance);
    public int GetMinimalDistanceByFriendlyWaterWithMax(ARegion target, Nation nation, int maxDistance) =>
        GetDistance(target, nation, ERegionType.WATER, maxDistance);

    #endregion

    #region Path

    protected List<ARegion> GetPath(ARegion? target, Nation? nation = null, ERegionType? type = null,
        int maxDistance = 10){
        if (target == null) return new List<ARegion>();
        int distance = GetDistance(target, nation, type, maxDistance);
        if (distance == 0) return new List<ARegion>();

        List<ARegion> path = new List<ARegion>();
        path.Add(this);
        if (distance == 1) path.Add(target);

        List<ARegion> neighbours = new List<ARegion>();

        if (nation != null && type == null){
            neighbours = GetAllFriendlyNeighbours(1, nation);
        }

        if (type != null && nation == null){
            switch (type){
                case ERegionType.LAND:
                    neighbours = GetLandNeighbours(distance);
                    break;
                case ERegionType.WATER:
                    neighbours = GetWaterNeighbours(distance);
                    break;
            }
        }

        if (type != null && nation != null){
            switch (type){
                case ERegionType.LAND:
                    neighbours = GetFriendlyLandNeighbours(1, nation);
                    break;
                case ERegionType.WATER:
                    neighbours = GetFriendlyWaterNeighbours(1, nation);
                    break;
            }
            
        }

        if (type == null && nation == null){
            neighbours = GetAllNeighbours(1);
        }

        foreach (var neigh in neighbours){
            if (path.Any(r => r.GetDistance(target, nation, type) == distance - 1)) break;
            if (neigh.GetDistance(target, nation, type) == distance - 1)
                path.AddRange(neigh.GetPath(target, nation, type));
        }

        return path;
    }

    public List<ARegion> GetPathToTarget(ARegion target) => GetPath(target);

    public List<ARegion> GetPathToTargetWithMax(ARegion target, int maxDistance) =>
        GetPath(target, null, null, maxDistance);

    public List<ARegion> GetPathToFriendlyTarget(ARegion target, Nation nation) => GetPath(target, nation);

    public List<ARegion> GetPathToFriendlyTargetWithMax(ARegion target, Nation nation, int maxDistance) =>
        GetPath(target, nation, null, maxDistance);

    public List<ARegion> GetPathToLandTarget(ARegion target) => GetPath(target, null, ERegionType.LAND);
    public List<ARegion> GetPathToWaterTarget(ARegion target) => GetPath(target, null, ERegionType.WATER);

    public List<ARegion> GetPathToLandTargetWithMax(ARegion target, int maxDistance) =>
        GetPath(target, null, ERegionType.LAND, maxDistance);
    public List<ARegion> GetPathToWaterTargetWithMax(ARegion target, int maxDistance) =>
        GetPath(target, null, ERegionType.WATER, maxDistance);

    public List<ARegion> GetPathToFriendlyLandTarget(ARegion target, Nation nation) => GetPath(target, nation, ERegionType.LAND);
    public List<ARegion> GetPathToFriendlyWaterTarget(ARegion target, Nation nation) => GetPath(target, nation, ERegionType.WATER);

    public List<ARegion> GetPathToFriendlyLandTargetWithMax(ARegion target, Nation nation, int maxDistance) =>
        GetPath(target, nation, ERegionType.LAND, maxDistance);
    public List<ARegion> GetPathToFriendlyWaterTargetWithMax(ARegion target, Nation nation, int maxDistance) =>
        GetPath(target, nation, ERegionType.WATER, maxDistance);

    #endregion


    public List<AUnit> GetStationedFriendlyUnits(Nation nation) => GetStationedUnits()
        .Where(u => u.Nation == nation || u.Nation.Allies.Any(a => a.Ally == nation)).ToList();

    public bool ContainsEnemies(Nation nation) => GetStationedUnits()
        .Any(u => u.Nation != nation && u.Nation.Allies.All(a => a.Ally != nation));

    public Dictionary<EUnitType, int> GetStationedUnitCounts(){
        Dictionary<EUnitType, int> counts = new Dictionary<EUnitType, int>();
        foreach (var unit in GetStationedUnits()) counts[unit.Type] = counts.GetValueOrDefault(unit.Type, 0) + 1;
        return counts;
    }

    public List<AUnit> GetOneStationedUnitPerType(){
        List<AUnit> oneUnitPerType = new List<AUnit>();
        //make an exception for units with type EUnitType.TRANSPORT and EUnitType.AIRCRAFT_CARRIER
        foreach (var unit in GetStationedUnits()){
            if (oneUnitPerType.Any(u => u.Type == unit.Type && u.Type != EUnitType.TRANSPORT &&
                                        u.Type != EUnitType.AIRCRAFT_CARRIER)) continue;
            oneUnitPerType.Add(unit);
        }
        return oneUnitPerType;
    }
    public virtual List<AUnit> GetStationedUnits() => null;

    public virtual Nation GetOwner() => null;
    public virtual int GetIncome() => 0;
}