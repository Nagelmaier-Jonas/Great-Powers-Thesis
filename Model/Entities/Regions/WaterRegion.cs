using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Model.Entities.Regions;

[Table("WATER_REGIONS")]
public class WaterRegion : ARegion{
    public List<AShip> StationedShips{ get; set; } = new List<AShip>();

    public override List<AUnit> GetStationedUnits(){
        List<AUnit> units = new List<AUnit>();
        units.AddRange(StationedShips);
        units.AddRange(StationedPlanes);
        return units;
    }

    public override bool IsWaterRegion() => true;
}