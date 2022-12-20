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
    
    public List<LandRegion> GetAllFriendlyNeighbours(int distance) =>
        GetFriendlyNeighbours(distance, Nation);

    public List<LandRegion> GetAllFriendlyNeighboursWithSource(int distance) =>
        GetFriendlyNeighbours(distance, Nation, false);

    public List<LandRegion> GetFriendlyNeighboursByLand(int distance) =>
        GetFriendlyNeighbours(distance, Nation, true, true);

    public List<LandRegion> GetFriendlyNeighboursByLandWithSource(int distance) =>
        GetFriendlyNeighbours(distance, Nation, false, true);
}