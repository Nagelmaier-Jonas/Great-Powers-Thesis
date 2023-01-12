using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Model.Factories;

public static class ShipFactory{
    private static AShip Create(AShip unit,WaterRegion? region, Nation? nation, bool seeding){
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
    public static AShip CreateSubmarine(WaterRegion? region, Nation? nation, bool seeding = false) => Create(new Submarine(),region,nation,seeding);
    public static AShip CreateDestroyer(WaterRegion? region, Nation? nation, bool seeding = false) => Create(new Destroyer(),region,nation,seeding);
    public static AShip CreateCruiser(WaterRegion? region, Nation? nation, bool seeding = false) => Create(new Cruiser(),region,nation,seeding);
    public static AShip CreateBattleship(WaterRegion? region, Nation? nation, bool seeding = false) => Create(new Battleship(),region,nation,seeding);
    public static AShip CreateTransport(WaterRegion? region, Nation? nation, bool seeding = false) => Create(new Transport(),region,nation,seeding);
    public static AShip CreateAircraftCarrier(WaterRegion? region, Nation? nation, bool seeding = false) => Create(new AircraftCarrier(),region,nation,seeding);
}