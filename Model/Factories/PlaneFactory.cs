using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Model.Factories;

public static class PlaneFactory{
    private static APlane Create(APlane unit,ARegion region, Nation nation, bool seeding){
        if (seeding){
            if (region != null) unit.RegionId = region.Id;
            unit.NationId = nation.Id;
        }
        else{
            unit.SetLocation(region);
            unit.Nation = nation;
        }
        unit.CurrentMovement = unit.Movement;
        return unit;
    }
    public static APlane CreateFighter(ARegion region, Nation nation, bool seeding = false) => Create(new Fighter(),region,nation,seeding);
    public static APlane CreateBomber(ARegion region, Nation nation, bool seeding = false) => Create(new Bomber(),region,nation,seeding);
}