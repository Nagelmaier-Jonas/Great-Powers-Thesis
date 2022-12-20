using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Domain.Factories;

public static class LandUnitFactory{
    public static LandUnit Create(EUnitType type, LandRegion region, Nation nation){
        LandUnit unit = type switch{
            EUnitType.INFANTRY => new(1,3,1,2),
            EUnitType.TANK => new(2,6,3,3),
            EUnitType.ANTI_AIR => new(1,5,0,0),
            EUnitType.ARTILLERY => new(1,4,2,2)
        };
        unit.Region = region;
        unit.Nation = nation;
        return unit;
    }
}