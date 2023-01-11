using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Model.Factories;

public static class PlaneFactory{
    public static APlane Create(EUnitType type, ARegion region, Nation nation, bool seeding = false){
        APlane unit = type switch{
            EUnitType.FIGHTER => new Fighter(),
            EUnitType.BOMBER => new Bomber()
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