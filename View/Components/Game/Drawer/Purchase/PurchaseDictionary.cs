using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace View.Components.Game.Drawer.Purchase;

public static class PurchaseDictionary{
    public static Dictionary<AUnit, int> GetLandUnitDictionary(Nation currentNation){
        return new Dictionary<AUnit, int>(){
            { LandUnitFactory.CreateInfantry(null, currentNation), 0 },
            { LandUnitFactory.CreateArtillery(null, currentNation), 0 },
            { LandUnitFactory.CreateTank(null, currentNation), 0 },
            { LandUnitFactory.CreateAntiAir(null, currentNation), 0 }
        };
    }

    public static Dictionary<AUnit, int> GetWaterUnitDictionary(Nation currentNation){
        return new Dictionary<AUnit, int>(){
            { ShipFactory.CreateSubmarine(null, currentNation), 0 },
            { ShipFactory.CreateTransport(null, currentNation), 0 },
            { ShipFactory.CreateDestroyer(null, currentNation), 0 },
            { ShipFactory.CreateCruiser( null, currentNation), 0 },
            { ShipFactory.CreateAircraftCarrier(null, currentNation), 0 },
            { ShipFactory.CreateBattleship(null, currentNation), 0 }
        };
    }

    public static Dictionary<AUnit, int> GetAirUnitDictionary(Nation currentNation){
        return new Dictionary<AUnit, int>(){
            { PlaneFactory.CreateFighter(null, currentNation), 0 },
            { PlaneFactory.CreateBomber(null, currentNation), 0 }
        };
    }

    public static Dictionary<AUnit, int> GetIndustrialUnitDictionary(){
        return new Dictionary<AUnit, int>(){
            { IndustryFactory.Create(null), 0 }
        };
    }

    public static Dictionary<AUnit, int> GetCheckoutDictionary(Nation currentNation){
        return new Dictionary<AUnit, int>(){
            { LandUnitFactory.CreateInfantry( null, currentNation), 0 },
            { LandUnitFactory.CreateArtillery( null, currentNation), 0 },
            { LandUnitFactory.CreateTank( null, currentNation), 0 },
            { LandUnitFactory.CreateAntiAir( null, currentNation), 0 },
            { ShipFactory.CreateSubmarine( null, currentNation), 0 },
            { ShipFactory.CreateTransport( null, currentNation), 0 },
            { ShipFactory.CreateDestroyer( null, currentNation), 0 },
            { ShipFactory.CreateCruiser( null, currentNation), 0 },
            { ShipFactory.CreateAircraftCarrier( null, currentNation), 0 },
            { ShipFactory.CreateBattleship( null, currentNation), 0 },
            { PlaneFactory.CreateFighter( null, currentNation), 0 },
            { PlaneFactory.CreateBomber( null, currentNation), 0 },
            { IndustryFactory.Create(null), 0 }
        };
    }
}