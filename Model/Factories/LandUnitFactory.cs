using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Model.Factories;

public static class LandUnitFactory{
    public static ALandUnit Create(EUnitType type, LandRegion region, Nation nation, bool seeding = false){
        ALandUnit unit = type switch{
            EUnitType.INFANTRY => new Infantry(),
            EUnitType.TANK => new Tank(),
            EUnitType.ANTI_AIR => new AntiAir(),
            EUnitType.ARTILLERY => new Artillery()
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