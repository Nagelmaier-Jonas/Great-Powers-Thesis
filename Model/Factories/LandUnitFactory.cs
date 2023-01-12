using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Model.Factories;

public static class LandUnitFactory{
    private static ALandUnit Create(ALandUnit unit,LandRegion? region, Nation? nation, bool seeding){
        if (seeding){
            if (region != null) unit.RegionId = region.Id;
            if (nation != null) unit.NationId = nation.Id;
        }
        else{
           if (region != null) unit.SetLocation(region);
           if (nation != null) unit.Nation = nation;
        }
        unit.CurrentMovement = unit.Movement;
        return unit;
    }
    public static ALandUnit CreateInfantry(LandRegion? region, Nation? nation, bool seeding = false) => Create(new Infantry(),region,nation,seeding);
    public static ALandUnit CreateTank(LandRegion? region, Nation? nation, bool seeding = false) => Create(new Tank(),region,nation,seeding);
    public static ALandUnit CreateArtillery(LandRegion? region, Nation? nation, bool seeding = false) => Create(new Artillery(),region,nation,seeding);
    public static ALandUnit CreateAntiAir(LandRegion? region, Nation? nation, bool seeding = false) => Create(new AntiAir(),region,nation,seeding);
}