using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Domain.Factories;

public static class ShipFactory{
    public static Ship Create(EUnitType type, WaterRegion region, Nation nation){
        Ship unit = type switch{
            EUnitType.DESTROYER => new(2,8,2,2),
            EUnitType.CRUISER => new(2,12,3,3),
            EUnitType.SUBMARINE => new(2,6,2,1),
            EUnitType.AIRCRAFT_CARRIER => new(2,14,1,2),
            EUnitType.TRANSPORT => new(2,7,0,0),
            EUnitType.BATTLESHIP => new(2,20,4,4),
        };
        unit.Region = region;
        unit.Nation = nation;
        return unit;
    }
}