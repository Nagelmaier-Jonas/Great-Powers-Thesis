using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Model.Factories;

public static class PlaneFactory{
    public static Plane Create(EUnitType type, ARegion region, Nation nation, bool seeding = false){
        Plane unit = type switch{
            EUnitType.FIGHTER => new(4,10,3,4),
            EUnitType.BOMBER => new(6,12,4,1)
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