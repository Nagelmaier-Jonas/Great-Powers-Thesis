using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Model.Factories;

public static class ShipFactory{
    public static Ship Create(EUnitType type, WaterRegion region, Nation nation, bool seeding = false){
        Ship unit = type switch{
            EUnitType.DESTROYER => new(2, 8, 2, 2),
            EUnitType.CRUISER => new(2, 12, 3, 3),
            EUnitType.SUBMARINE => new(2, 6, 2, 1),
            EUnitType.AIRCRAFT_CARRIER => new AircraftCarrier(2, 14, 1, 2),
            EUnitType.TRANSPORT => new Transport(2, 7, 0, 0),
            EUnitType.BATTLESHIP => new(2, 20, 4, 4),
        };
        if (seeding){
            unit.RegionId = region.Id;
            unit.NationId = nation.Id;
        }
        else{
            unit.Region = region;
            unit.Nation = nation;
        }

        unit.Type = type;
        unit.CurrentMovement = unit.Movement;
        return unit;
    }
}