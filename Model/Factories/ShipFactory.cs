using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Model.Factories;

public static class ShipFactory{
    public static AShip Create(EUnitType type, WaterRegion region, Nation nation, bool seeding = false){
        AShip unit = type switch{
            EUnitType.DESTROYER => new Destroyer(),
            EUnitType.CRUISER => new Cruiser(),
            EUnitType.SUBMARINE => new Submarine(),
            EUnitType.AIRCRAFT_CARRIER => new AircraftCarrier(),
            EUnitType.TRANSPORT => new Transport(),
            EUnitType.BATTLESHIP => new Battleship()
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