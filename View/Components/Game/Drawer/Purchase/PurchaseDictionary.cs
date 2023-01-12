using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace View.Components.Game.Drawer.Purchase;

public static class PurchaseDictionary{
    public static Dictionary<AUnit, int> GetLandUnitDictionary(Nation currentNation){
        return new Dictionary<AUnit, int>(){
            { LandUnitFactory.Create(EUnitType.INFANTRY, null, currentNation), 0 },
            { LandUnitFactory.Create(EUnitType.ARTILLERY, null, currentNation), 0 },
            { LandUnitFactory.Create(EUnitType.TANK, null, currentNation), 0 },
            { LandUnitFactory.Create(EUnitType.ANTI_AIR, null, currentNation), 0 }
        };
    }

    public static Dictionary<AUnit, int> GetWaterUnitDictionary(Nation currentNation){
        return new Dictionary<AUnit, int>(){
            { ShipFactory.Create(EUnitType.SUBMARINE, null, currentNation), 0 },
            { ShipFactory.Create(EUnitType.TRANSPORT, null, currentNation), 0 },
            { ShipFactory.Create(EUnitType.DESTROYER, null, currentNation), 0 },
            { ShipFactory.Create(EUnitType.CRUISER, null, currentNation), 0 },
            { ShipFactory.Create(EUnitType.AIRCRAFT_CARRIER, null, currentNation), 0 },
            { ShipFactory.Create(EUnitType.BATTLESHIP, null, currentNation), 0 }
        };
    }

    public static Dictionary<AUnit, int> GetAirUnitDictionary(Nation currentNation){
        return new Dictionary<AUnit, int>(){
            { PlaneFactory.Create(EUnitType.FIGHTER, null, currentNation), 0 },
            { PlaneFactory.Create(EUnitType.BOMBER, null, currentNation), 0 }
        };
    }

    public static Dictionary<AUnit, int> GetIndustrialUnitDictionary(Nation currentNation){
        return new Dictionary<AUnit, int>(){
            { IndustryFactory.Create(EUnitType.FACTORY, null, currentNation), 0 }
        };
    }

    public static Dictionary<AUnit, int> GetCheckoutDictionary(Nation currentNation){
        return new Dictionary<AUnit, int>(){
            { LandUnitFactory.Create(EUnitType.INFANTRY, null, currentNation), 0 },
            { LandUnitFactory.Create(EUnitType.ARTILLERY, null, currentNation), 0 },
            { LandUnitFactory.Create(EUnitType.TANK, null, currentNation), 0 },
            { LandUnitFactory.Create(EUnitType.ANTI_AIR, null, currentNation), 0 },
            { ShipFactory.Create(EUnitType.SUBMARINE, null, currentNation), 0 },
            { ShipFactory.Create(EUnitType.TRANSPORT, null, currentNation), 0 },
            { ShipFactory.Create(EUnitType.DESTROYER, null, currentNation), 0 },
            { ShipFactory.Create(EUnitType.CRUISER, null, currentNation), 0 },
            { ShipFactory.Create(EUnitType.AIRCRAFT_CARRIER, null, currentNation), 0 },
            { ShipFactory.Create(EUnitType.BATTLESHIP, null, currentNation), 0 },
            { PlaneFactory.Create(EUnitType.FIGHTER, null, currentNation), 0 },
            { PlaneFactory.Create(EUnitType.BOMBER, null, currentNation), 0 },
            { new Factory{ Type = EUnitType.FACTORY }, 0 }
        };
    }
}