using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Model.Factories;

public static class LandUnitFactory{
    public static LandUnit Create(EUnitType type, LandRegion region, Nation nation, bool seeding = false){
        LandUnit unit = type switch{
            EUnitType.INFANTRY => new(1,3,1,2),
            EUnitType.TANK => new(2,6,3,3),
            EUnitType.ANTI_AIR => new(1,5,0,0),
            EUnitType.ARTILLERY => new(1,4,2,2)
        };
        if (seeding){
            if(region != null)unit.RegionId = region.Id;
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