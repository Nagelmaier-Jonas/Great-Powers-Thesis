using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Units;

namespace Model.Entities.Regions;

[Table("LAND_REGION")]
public class LandRegion : ARegion{
    [Column("INCOME")]
    public int? Income{ get; set; }

    [Column("CAPITAL_ID")]
    public int? CapitalId{ get; set; }
    
    public Capital? Capital{ get; set; }
    
    [Column("OWNER_ID")]
    public int? NationId{ get; set; }

    public Nation? Nation{ get; set; }

    public List<Factory>? Factories{ get; set; }

    public List<LandUnit> StationedUnits{ get; set; } = new List<LandUnit>();
    public List<LandUnit>? IncomingUnits{ get; set; }

    public override List<AUnit> GetStationedUnits(){
        List<AUnit> units = new List<AUnit>();
        units.AddRange(StationedUnits);
        units.AddRange(StationedPlanes);
        return units;
    }

    public override Nation GetOwner() => Nation;

    public List<ARegion> GetAllFriendlyNeighbours(int distance) =>
        GetFriendlyNeighbours(distance, Nation);

    public List<ARegion> GetAllFriendlyNeighboursWithSource(int distance) =>
        GetFriendlyNeighbours(distance, Nation, false);

    public List<ARegion> GetFriendlyNeighboursByLand(int distance) =>
        GetFriendlyNeighbours(distance, Nation, true, ERegionType.LAND);

    public List<ARegion> GetFriendlyNeighboursByLandWithSource(int distance) =>
        GetFriendlyNeighbours(distance, Nation, false, ERegionType.LAND);

    public int GetMinimalDistanceByFriendlies(ARegion target) => GetMinimalDistanceByFriendlies(target, Nation);
    public int GetMinimalDistanceByFriendlyLand(ARegion target) => GetMinimalDistanceByFriendlyLand(target, Nation);
    public int GetMinimalDistanceByFriendlyWater(ARegion target) => GetMinimalDistanceByFriendlyWater(target, Nation);
    public int GetMinimalDistanceByFriendliesWithMax(ARegion target, int maxDistance) => GetMinimalDistanceByFriendliesWithMax(target, Nation,maxDistance);
    public int GetMinimalDistanceByFriendlyLandWithMax(ARegion target, int maxDistance) => GetMinimalDistanceByFriendlyLandWithMax(target, Nation,maxDistance);
    public int GetMinimalDistanceByFriendlyWaterWithMax(ARegion target, int maxDistance) => GetMinimalDistanceByFriendlyWaterWithMax(target, Nation,maxDistance);
    
    public List<ARegion> GetPathToTargetByFriendlies(ARegion target) => GetPath(target, Nation);
    public List<ARegion> GetPathToTargetByFriendliesWithMax(ARegion target, int maxDistance) => GetPath(target, Nation,null, maxDistance);
    public List<ARegion> GetPathToTargetByFriendlyLand(ARegion target) => GetPath(target, Nation, ERegionType.LAND);
    public List<ARegion> GetPathToTargetByFriendlyLandWithMax(ARegion target, int maxDistance) => GetPath(target, Nation, ERegionType.LAND,maxDistance);
}