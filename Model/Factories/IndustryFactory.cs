using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Model.Factories;

public static class IndustryFactory{

    public static Factory Create(LandRegion region, bool seeding = false, int id = 0){
        Factory unit = new Factory();
        if (seeding){
            if(region != null)unit.RegionId = region.Id;
            if(region != null)unit.NationId = region.NationId;
            if (id != 0) unit.Id = id;
        }
        else{
            unit.Region = region;
            unit.Nation = region.Nation;
        }
        unit.CurrentMovement = unit.Movement;
        unit.Damage = 0;
        return unit;
    }
}