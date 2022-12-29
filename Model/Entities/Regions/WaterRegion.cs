using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Units;

namespace Model.Entities.Regions;

[Table("WATER_REGIONS")]
public class WaterRegion : ARegion{
    public List<Ship> StationedShips{ get; set; } = new List<Ship>();
    public List<Ship> IncomingShips{ get; set; } = new List<Ship>();

    public override List<AUnit> GetStationedUnits(){
        List<AUnit> units = new List<AUnit>();
        units.AddRange(StationedShips);
        units.AddRange(StationedPlanes);
        return units;
    }

    public override Nation GetOwner() => null;
}