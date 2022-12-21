using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Domain.Factories;

public static class PlaneFactory{
    public static Plane Create(EUnitType type, ARegion region, Nation nation){
        Plane unit = type switch{
            EUnitType.FIGHTER => new(4,10,3,4),
            EUnitType.BOMBER => new(6,12,4,1)
        };
        unit.Region = region;
        unit.Nation = nation;
        unit.Type = type;
        unit.CurrentMovement = unit.Movement;
        return unit;
    }
}