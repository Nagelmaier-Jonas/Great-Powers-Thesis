﻿using System.ComponentModel.DataAnnotations;
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
    public string? Name{ get; set; }

    public List<Neighbours>? Neighbours{ get; set; }

    [Column("REGION_TYPE", TypeName = "VARCHAR(45)")]
    public ERegionType? Type{ get; set; }

    [Column("IDENTIFIER")] public ERegion Identifier{ get; set; }

    [NotMapped] public Point Position{ get; set; }

    public List<Plane> StationedPlanes{ get; set; } = new List<Plane>();
    public List<Plane>? IncomingPlanes{ get; set; }

    #region Neighbours

    private List<ARegion> GetNeighbours(int distance, bool excludeSource = true, ERegionType? type = null){
        if (distance <= 0) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        foreach (var neighbour in Neighbours){
            if (type != null){
                if (neighbour.Neighbour.Type == type){
                    regions.Add(neighbour.Neighbour);
                    regions.UnionWith(neighbour.Neighbour.GetNeighbours(distance - 1, false, type));
                }
            }
            else{
                regions.Add(neighbour.Neighbour);
                regions.UnionWith(neighbour.Neighbour.GetNeighbours(distance - 1, false, type));
            }
        }

        if (excludeSource) regions.Remove(this);
        return regions.ToList();
    }

    public List<ARegion> GetAllNeighbours(int distance) =>
        GetNeighbours(distance);

    public List<ARegion> GetAllNeighboursWithSource(int distance) =>
        GetNeighbours(distance, false);

    public List<ARegion> GetNeighboursByLand(int distance) =>
        GetNeighbours(distance, true, ERegionType.LAND);
    
    public List<ARegion> GetNeighboursByWater(int distance) =>
        GetNeighbours(distance, true, ERegionType.WATER);

    public List<ARegion> GetNeighboursByTypeWithSource(int distance, ERegionType type) =>
        GetNeighbours(distance, false, type);

    #endregion

    #region FriendlyNeighbours

    protected List<ARegion> GetFriendlyNeighbours(int distance, Nation nation, bool excludeSource = true, ERegionType? type = null){
        if (distance <= 0) return new List<ARegion>();
        HashSet<ARegion> regions = new HashSet<ARegion>();
        foreach (var neighbour in Neighbours){
            if (neighbour.Neighbour.ContainsEnemies(nation)) continue;
            LandRegion? neigh = null;
            if (neighbour.Neighbour.Type == ERegionType.LAND) neigh = (LandRegion)neighbour.Neighbour;
            if (type != null){
                if (neighbour.Neighbour.Type != type) continue;
            }
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

        if (excludeSource && Type == ERegionType.LAND) regions.Remove((LandRegion)this);
        return regions.ToList();
    }

    public List<ARegion> GetAllFriendlyNeighbours(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation);

    public List<ARegion> GetAllFriendlyNeighboursWithSource(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, false);

    public List<ARegion> GetFriendlyNeighboursByLand(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, true, ERegionType.LAND);
    public List<ARegion> GetFriendlyNeighboursByWater(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, true, ERegionType.WATER);

    public List<ARegion> GetFriendlyNeighboursByLandWithSource(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, false, ERegionType.LAND);
    public List<ARegion> GetFriendlyNeighboursByWaterWithSource(int distance, Nation nation) =>
        GetFriendlyNeighbours(distance, nation, false, ERegionType.WATER);

    #endregion

    #region Distance

    protected int GetDistance(ARegion target, Nation? nation = null, ERegionType? type = null, int maxDistance = 10,
        int distance = 1){
        if (distance == maxDistance) return 0;
        if (nation != null && type == null){
            if (GetAllFriendlyNeighbours(distance, nation).Contains(target)) return distance;
        }

        if (type != null && nation == null){
            switch (type){
                case ERegionType.LAND:
                    if (GetNeighboursByLand(distance).Contains(target)) return distance;
                    break;
                case ERegionType.WATER:
                    if (GetNeighboursByWater(distance).Contains(target)) return distance;
                    break;
            }
        }

        if (type != null && nation != null){
            switch (type){
                case ERegionType.LAND:
                    if (GetFriendlyNeighboursByLand(distance, nation).Contains(target)) return distance;
                    break;
                case ERegionType.WATER:
                    if (GetFriendlyNeighboursByWater(distance, nation).Contains(target)) return distance;
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

    protected List<ARegion> GetPath(ARegion target, Nation? nation = null, ERegionType? type = null,
        int maxDistance = 10){
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
                    neighbours = GetNeighboursByLand(distance);
                    break;
                case ERegionType.WATER:
                    neighbours = GetNeighboursByWater(distance);
                    break;
            }
        }

        if (type != null && nation != null){
            switch (type){
                case ERegionType.LAND:
                    neighbours = GetFriendlyNeighboursByLand(1, nation);
                    break;
                case ERegionType.WATER:
                    neighbours = GetFriendlyNeighboursByWater(1, nation);
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

    public List<ARegion> GetPathToTargetByFriendlies(ARegion target, Nation nation) => GetPath(target, nation);

    public List<ARegion> GetPathToTargetByFriendliesWithMax(ARegion target, Nation nation, int maxDistance) =>
        GetPath(target, nation, null, maxDistance);

    public List<ARegion> GetPathToTargetByLand(ARegion target) => GetPath(target, null, ERegionType.LAND);
    public List<ARegion> GetPathToTargetByWater(ARegion target) => GetPath(target, null, ERegionType.WATER);

    public List<ARegion> GetPathToTargetByLandWithMax(ARegion target, int maxDistance) =>
        GetPath(target, null, ERegionType.LAND, maxDistance);
    public List<ARegion> GetPathToTargetByWaterWithMax(ARegion target, int maxDistance) =>
        GetPath(target, null, ERegionType.WATER, maxDistance);

    public List<ARegion> GetPathToTargetByFriendlyLand(ARegion target, Nation nation) => GetPath(target, nation, ERegionType.LAND);
    public List<ARegion> GetPathToTargetByFriendlyWater(ARegion target, Nation nation) => GetPath(target, nation, ERegionType.WATER);

    public List<ARegion> GetPathToTargetByFriendlyLandWithMax(ARegion target, Nation nation, int maxDistance) =>
        GetPath(target, nation, ERegionType.LAND, maxDistance);
    public List<ARegion> GetPathToTargetByFriendlyWaterWithMax(ARegion target, Nation nation, int maxDistance) =>
        GetPath(target, nation, ERegionType.WATER, maxDistance);

    #endregion

    public virtual List<AUnit> GetStationedUnits() =>
        throw new NotImplementedException();

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
        foreach (var unit in GetStationedUnits().Where(unit => oneUnitPerType.All(u => u.Type != unit.Type))){
            oneUnitPerType.Add(unit);
        }

        return oneUnitPerType;
    }

    public virtual Nation GetOwner() => throw new NotImplementedException();
}