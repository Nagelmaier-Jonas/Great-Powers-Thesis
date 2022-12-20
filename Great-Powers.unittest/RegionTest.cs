using System.Collections.Generic;
using Domain.Factories;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;
using NUnit.Framework;

namespace Great_Powers.unittest;

public class RegionTest{
    [SetUp]
    public void Setup(){
    }

    [Test]
    public void GetNeighboursTest(){
        #region Nations

        Nation deutsch = new Nation();
        Nation japan = new Nation();
        Nation usa = new Nation();
        Nation gb = new Nation();

        deutsch.Allies = new List<Allies>(){
            new Allies(){
                Nation = deutsch,
                Ally = japan
            }
        };
        japan.Allies = new List<Allies>(){
            new Allies(){
                Nation = japan,
                Ally = deutsch
            }
        };
        usa.Allies = new List<Allies>(){
            new Allies(){
                Nation = usa,
                Ally = gb
            }
        };
        gb.Allies = new List<Allies>(){
            new Allies(){
                Nation = gb,
                Ally = usa
            }
        };

        #endregion
        
        #region Regions

        LandRegion deutschland = new LandRegion(){
            Name = "deutschland",
            Type = ERegionType.LAND,
            Nation = deutsch
        };
        LandRegion polen = new LandRegion(){
            Name = "polen",
            Type = ERegionType.LAND,
            Nation = deutsch
        };
        LandRegion dänemark = new LandRegion(){
            Name = "dänemark",
            Type = ERegionType.LAND,
            Nation = japan
        };
        LandRegion russland = new LandRegion(){
            Name = "russland",
            Type = ERegionType.LAND,
            Nation = gb
        };
        LandRegion tschechien = new LandRegion(){
            Name = "tschechien",
            Type = ERegionType.LAND,
            Nation = usa
        };
        LandRegion china = new LandRegion(){
            Name = "china",
            Type = ERegionType.LAND,
            Nation = japan
        };
        LandRegion finnland = new LandRegion(){
            Name = "finnland",
            Type = ERegionType.LAND,
            Nation = deutsch
        };

        WaterRegion nordsee = new WaterRegion(){
            Name = "nordsee",
            Type = ERegionType.WATER
        };
        WaterRegion ostsee = new WaterRegion(){
            Name = "ostsee",
            Type = ERegionType.WATER
        };
        WaterRegion atlantik = new WaterRegion(){
            Name = "atlantik",
            Type = ERegionType.WATER
        };
        #endregion

        #region Neighbours

        deutschland.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = deutschland,
                Neighbour = polen
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = dänemark
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = ostsee
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = nordsee
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = tschechien
            }
        };
        polen.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = polen,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = polen,
                Neighbour = russland
            },
            new Neighbours(){
                Region = polen,
                Neighbour = tschechien
            },
            new Neighbours(){
                Region = polen,
                Neighbour = ostsee
            }
        };
        dänemark.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = dänemark,
                Neighbour = nordsee
            },
            new Neighbours(){
                Region = dänemark,
                Neighbour = ostsee
            },
            new Neighbours(){
                Region = dänemark,
                Neighbour = deutschland
            }
        };
        finnland.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = finnland,
                Neighbour = russland
            },
            new Neighbours(){
                Region = finnland,
                Neighbour = ostsee
            }
        };
        russland.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = russland,
                Neighbour = ostsee
            },
            new Neighbours(){
                Region = russland,
                Neighbour = polen
            },
            new Neighbours(){
                Region = russland,
                Neighbour = china
            },
            new Neighbours(){
                Region = russland,
                Neighbour = finnland
            }
        };
        tschechien.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = tschechien,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = tschechien,
                Neighbour = polen
            }
        };
        nordsee.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = nordsee,
                Neighbour = dänemark
            },
            new Neighbours(){
                Region = nordsee,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = nordsee,
                Neighbour = atlantik
            }
        };
        ostsee.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = ostsee,
                Neighbour = dänemark
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = polen
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = russland
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = finnland
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = nordsee
            }
        };
        atlantik.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = atlantik,
                Neighbour = nordsee
            }
        };
        china.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = china,
                Neighbour = russland
            }
        };

        #endregion
        
        #region Units

        LandUnit panzer = LandUnitFactory.Create(EUnitType.TANK, deutschland, deutsch);
        LandUnit infantrie = LandUnitFactory.Create(EUnitType.INFANTRY, deutschland, japan);
        Plane jäger = PlaneFactory.Create(EUnitType.FIGHTER, deutschland, deutsch);
        Plane jäger2 = PlaneFactory.Create(EUnitType.FIGHTER, ostsee, japan);
        Ship aircraftCarrier = ShipFactory.Create(EUnitType.AIRCRAFT_CARRIER, ostsee, deutsch);
        Ship submarine = ShipFactory.Create(EUnitType.SUBMARINE, ostsee, usa);

        deutschland.StationedUnits = new List<LandUnit>(){
            panzer,
            infantrie
        };
        deutschland.StationedPlanes = new List<Plane>(){
            jäger
        };
        ostsee.StationedShips = new List<Ship>(){
            aircraftCarrier,
            submarine
        };
        ostsee.StationedPlanes = new List<Plane>(){
            jäger2
        };

        #endregion

        Assert.IsTrue(deutschland.GetAllNeighbours(1).Count == 5);
        Assert.IsTrue(deutschland.GetNeighboursByType(1, ERegionType.LAND).Count == 3);
        Assert.IsTrue(deutschland.GetNeighboursByType(1, ERegionType.WATER).Count == 2);

        Assert.IsTrue(deutschland.GetAllNeighbours(2).Count == 8);
        Assert.IsTrue(deutschland.GetNeighboursByType(2, ERegionType.LAND).Count == 4);
        Assert.IsTrue(deutschland.GetNeighboursByType(2, ERegionType.WATER).Count == 3);
        Assert.IsTrue(tschechien.GetNeighboursByType(1, ERegionType.WATER).Count == 0);

        Assert.IsTrue(deutschland.GetAllNeighbours(2).Contains(finnland));
        Assert.IsTrue(deutschland.GetAllNeighbours(3).Contains(china));
        Assert.IsTrue(russland.GetNeighboursByType(3, ERegionType.WATER).Contains(atlantik));

        Assert.IsFalse(deutschland.GetAllNeighbours(2).Contains(deutschland));
        
        Assert.IsTrue(deutschland.GetAllNeighboursWithSource(2).Count == 9);
        Assert.IsTrue(deutschland.GetAllNeighboursWithSource(2).Contains(deutschland));
        
        Assert.IsTrue(deutschland.GetNeighboursByTypeWithSource(2,ERegionType.LAND).Count == 5);
        Assert.IsTrue(deutschland.GetNeighboursByTypeWithSource(2,ERegionType.LAND).Contains(deutschland));
    }
    
    [Test]
    public void GetFriendlyNeighboursTest(){
        #region Nations

        Nation deutsch = new Nation(){
            Name = "Deutschland"
        };
        Nation japan = new Nation(){
            Name = "Japan"
        };
        Nation usa = new Nation(){
            Name = "USA"
        };
        Nation gb = new Nation(){
            Name = "Großbritannien"
        };

        deutsch.Allies = new List<Allies>(){
            new Allies(){
                Nation = deutsch,
                Ally = japan
            }
        };
        japan.Allies = new List<Allies>(){
            new Allies(){
                Nation = japan,
                Ally = deutsch
            }
        };
        usa.Allies = new List<Allies>(){
            new Allies(){
                Nation = usa,
                Ally = gb
            }
        };
        gb.Allies = new List<Allies>(){
            new Allies(){
                Nation = gb,
                Ally = usa
            }
        };

        #endregion
        
        #region Regions

        LandRegion deutschland = new LandRegion(){
            Name = "deutschland",
            Type = ERegionType.LAND,
            Nation = deutsch
        };
        LandRegion polen = new LandRegion(){
            Name = "polen",
            Type = ERegionType.LAND,
            Nation = deutsch
        };
        LandRegion dänemark = new LandRegion(){
            Name = "dänemark",
            Type = ERegionType.LAND,
            Nation = japan
        };
        LandRegion russland = new LandRegion(){
            Name = "russland",
            Type = ERegionType.LAND,
            Nation = gb
        };
        LandRegion tschechien = new LandRegion(){
            Name = "tschechien",
            Type = ERegionType.LAND,
            Nation = usa
        };
        LandRegion china = new LandRegion(){
            Name = "china",
            Type = ERegionType.LAND,
            Nation = japan
        };
        LandRegion finnland = new LandRegion(){
            Name = "finnland",
            Type = ERegionType.LAND,
            Nation = deutsch
        };
        LandRegion ukraine = new LandRegion(){
            Name = "ukraine",
            Type = ERegionType.LAND,
            Nation = deutsch
        };

        WaterRegion nordsee = new WaterRegion(){
            Name = "nordsee",
            Type = ERegionType.WATER
        };
        WaterRegion ostsee = new WaterRegion(){
            Name = "ostsee",
            Type = ERegionType.WATER
        };
        WaterRegion atlantik = new WaterRegion(){
            Name = "atlantik",
            Type = ERegionType.WATER
        };
        WaterRegion afrikaUmrundung = new WaterRegion(){
            Name = "afrikaUmrundung",
            Type = ERegionType.WATER
        };
        #endregion

        #region Neighbours

        deutschland.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = deutschland,
                Neighbour = polen
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = dänemark
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = ostsee
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = nordsee
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = tschechien
            }
        };
        polen.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = polen,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = polen,
                Neighbour = russland
            },
            new Neighbours(){
                Region = polen,
                Neighbour = tschechien
            },
            new Neighbours(){
                Region = polen,
                Neighbour = ostsee
            },
            new Neighbours(){
                Region = polen,
                Neighbour = ukraine
            }
        };
        dänemark.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = dänemark,
                Neighbour = nordsee
            },
            new Neighbours(){
                Region = dänemark,
                Neighbour = ostsee
            },
            new Neighbours(){
                Region = dänemark,
                Neighbour = deutschland
            }
        };
        finnland.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = finnland,
                Neighbour = russland
            },
            new Neighbours(){
                Region = finnland,
                Neighbour = ostsee
            }
        };
        russland.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = russland,
                Neighbour = ostsee
            },
            new Neighbours(){
                Region = russland,
                Neighbour = polen
            },
            new Neighbours(){
                Region = russland,
                Neighbour = china
            },
            new Neighbours(){
                Region = russland,
                Neighbour = finnland
            }
        };
        tschechien.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = tschechien,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = tschechien,
                Neighbour = polen
            }
        };
        nordsee.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = nordsee,
                Neighbour = dänemark
            },
            new Neighbours(){
                Region = nordsee,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = nordsee,
                Neighbour = atlantik
            }
        };
        ostsee.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = ostsee,
                Neighbour = dänemark
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = polen
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = russland
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = finnland
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = nordsee
            }
        };
        atlantik.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = atlantik,
                Neighbour = nordsee
            },
            new Neighbours(){
                Region = atlantik,
                Neighbour = afrikaUmrundung
            }
        };
        china.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = china,
                Neighbour = russland
            },
            new Neighbours(){
                Region = china,
                Neighbour = afrikaUmrundung
            }
        };
        afrikaUmrundung.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = afrikaUmrundung,
                Neighbour = atlantik
            },
            new Neighbours(){
                Region = afrikaUmrundung,
                Neighbour = china
            }
        };
        ukraine.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = ukraine,
                Neighbour = polen
            }
        };

        #endregion
        
        #region Units

        LandUnit panzer = LandUnitFactory.Create(EUnitType.TANK, deutschland, deutsch);
        LandUnit infantrie = LandUnitFactory.Create(EUnitType.INFANTRY, deutschland, japan);
        Plane jäger = PlaneFactory.Create(EUnitType.FIGHTER, deutschland, deutsch);
        Plane jäger2 = PlaneFactory.Create(EUnitType.FIGHTER, ostsee, japan);
        Ship aircraftCarrier = ShipFactory.Create(EUnitType.AIRCRAFT_CARRIER, ostsee, deutsch);
        Ship submarine = ShipFactory.Create(EUnitType.SUBMARINE, ostsee, usa);

        deutschland.StationedUnits = new List<LandUnit>(){
            panzer,
            infantrie
        };
        deutschland.StationedPlanes = new List<Plane>(){
            jäger
        };
        ostsee.StationedShips = new List<Ship>(){
            aircraftCarrier,
            submarine
        };
        ostsee.StationedPlanes = new List<Plane>(){
            jäger2
        };

        #endregion

        Assert.IsTrue(deutschland.GetAllFriendlyNeighbours(2).Count == 3);
        Assert.IsTrue(dänemark.GetAllFriendlyNeighbours(2).Count == 2);
        Assert.IsTrue(deutschland.GetAllFriendlyNeighbours(3).Count == 3);
        Assert.IsFalse(deutschland.GetAllFriendlyNeighbours(2).Contains(russland));
        Assert.IsFalse(deutschland.GetAllFriendlyNeighbours(2).Contains(tschechien));
        
        Assert.IsTrue(deutschland.GetAllFriendlyNeighboursWithSource(2).Count == 4);
        Assert.IsTrue(deutschland.GetAllFriendlyNeighboursWithSource(3).Count == 4);
        Assert.IsTrue(deutschland.GetAllFriendlyNeighboursWithSource(2).Contains(deutschland));
        Assert.IsFalse(deutschland.GetAllFriendlyNeighboursWithSource(2).Contains(tschechien));
        Assert.IsTrue(dänemark.GetAllFriendlyNeighboursWithSource(2).Contains(dänemark));
        Assert.IsTrue(dänemark.GetAllFriendlyNeighboursWithSource(2).Count == 3);
        
        Assert.IsTrue(deutschland.GetFriendlyNeighboursByLand(2).Count == 3);
        Assert.IsTrue(dänemark.GetFriendlyNeighboursByLand(2).Count == 2);
        Assert.IsTrue(dänemark.GetFriendlyNeighboursByLand(3).Count == 3);
        
        Assert.IsTrue(deutschland.GetFriendlyNeighboursByLandWithSource(2).Count == 4);
        Assert.IsTrue(deutschland.GetFriendlyNeighboursByLandWithSource(2).Contains(deutschland));
        Assert.IsTrue(dänemark.GetFriendlyNeighboursByLandWithSource(2).Count == 3);
        Assert.IsTrue(dänemark.GetFriendlyNeighboursByLandWithSource(2).Contains(dänemark));
        Assert.IsTrue(dänemark.GetFriendlyNeighboursByLandWithSource(3).Count == 4);
        Assert.IsTrue(dänemark.GetFriendlyNeighboursByLandWithSource(3).Contains(dänemark));
    }

    [Test]
    public void GetStationedUnitsTest(){
        #region Regions

        LandRegion deutschland = new LandRegion(){
            Name = "deutschland",
            Type = ERegionType.LAND
        };
        LandRegion polen = new LandRegion(){
            Name = "polen",
            Type = ERegionType.LAND
        };
        LandRegion dänemark = new LandRegion(){
            Name = "dänemark",
            Type = ERegionType.LAND
        };
        LandRegion russland = new LandRegion(){
            Name = "russland",
            Type = ERegionType.LAND
        };
        LandRegion tschechien = new LandRegion(){
            Name = "tschechien",
            Type = ERegionType.LAND
        };
        LandRegion china = new LandRegion(){
            Name = "china",
            Type = ERegionType.LAND
        };
        LandRegion finnland = new LandRegion(){
            Name = "finnland",
            Type = ERegionType.LAND
        };

        WaterRegion nordsee = new WaterRegion(){
            Name = "nordsee",
            Type = ERegionType.WATER
        };
        WaterRegion ostsee = new WaterRegion(){
            Name = "ostsee",
            Type = ERegionType.WATER
        };
        WaterRegion atlantik = new WaterRegion(){
            Name = "atlantik",
            Type = ERegionType.WATER
        };

        #endregion

        #region Neighbours

        deutschland.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = deutschland,
                Neighbour = polen
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = dänemark
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = ostsee
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = nordsee
            },
            new Neighbours(){
                Region = deutschland,
                Neighbour = tschechien
            }
        };
        polen.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = polen,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = polen,
                Neighbour = russland
            },
            new Neighbours(){
                Region = polen,
                Neighbour = tschechien
            },
            new Neighbours(){
                Region = polen,
                Neighbour = ostsee
            }
        };
        dänemark.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = dänemark,
                Neighbour = nordsee
            },
            new Neighbours(){
                Region = dänemark,
                Neighbour = ostsee
            },
            new Neighbours(){
                Region = dänemark,
                Neighbour = deutschland
            }
        };
        finnland.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = finnland,
                Neighbour = russland
            },
            new Neighbours(){
                Region = finnland,
                Neighbour = ostsee
            }
        };
        russland.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = russland,
                Neighbour = ostsee
            },
            new Neighbours(){
                Region = russland,
                Neighbour = polen
            },
            new Neighbours(){
                Region = russland,
                Neighbour = china
            },
            new Neighbours(){
                Region = russland,
                Neighbour = finnland
            }
        };
        tschechien.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = tschechien,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = tschechien,
                Neighbour = polen
            }
        };
        nordsee.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = nordsee,
                Neighbour = dänemark
            },
            new Neighbours(){
                Region = nordsee,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = nordsee,
                Neighbour = atlantik
            }
        };
        ostsee.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = ostsee,
                Neighbour = dänemark
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = deutschland
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = polen
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = russland
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = finnland
            },
            new Neighbours(){
                Region = ostsee,
                Neighbour = nordsee
            }
        };
        atlantik.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = atlantik,
                Neighbour = nordsee
            }
        };
        china.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = china,
                Neighbour = russland
            }
        };

        #endregion

        #region Nations

        Nation deutsch = new Nation();
        Nation japan = new Nation();
        Nation usa = new Nation();
        Nation gb = new Nation();

        deutsch.Allies = new List<Allies>(){
            new Allies(){
                Nation = deutsch,
                Ally = japan
            }
        };
        japan.Allies = new List<Allies>(){
            new Allies(){
                Nation = japan,
                Ally = deutsch
            }
        };
        usa.Allies = new List<Allies>(){
            new Allies(){
                Nation = usa,
                Ally = gb
            }
        };
        gb.Allies = new List<Allies>(){
            new Allies(){
                Nation = gb,
                Ally = usa
            }
        };

        #endregion
        
        #region Units

        LandUnit panzer = LandUnitFactory.Create(EUnitType.TANK, deutschland, deutsch);
        LandUnit infantrie = LandUnitFactory.Create(EUnitType.INFANTRY, deutschland, japan);
        Plane jäger = PlaneFactory.Create(EUnitType.FIGHTER, deutschland, deutsch);
        Plane jäger2 = PlaneFactory.Create(EUnitType.FIGHTER, ostsee, japan);
        Ship aircraftCarrier = ShipFactory.Create(EUnitType.AIRCRAFT_CARRIER, ostsee, deutsch);
        Ship submarine = ShipFactory.Create(EUnitType.SUBMARINE, ostsee, usa);

        deutschland.StationedUnits = new List<LandUnit>(){
            panzer,
            infantrie
        };
        deutschland.StationedPlanes = new List<Plane>(){
            jäger
        };
        ostsee.StationedShips = new List<Ship>(){
            aircraftCarrier,
            submarine
        };
        ostsee.StationedPlanes = new List<Plane>(){
            jäger2
        };

        #endregion

        Assert.IsTrue(deutschland.GetStationedUnits().Count == 3);
        Assert.IsTrue(ostsee.GetStationedUnits().Count == 3);
        Assert.IsTrue(ostsee.GetStationedFriendlyUnits(deutsch).Count == 2);
        Assert.IsFalse(ostsee.GetStationedFriendlyUnits(deutsch).Contains(submarine));
        Assert.IsTrue(ostsee.GetStationedFriendlyUnits(deutsch).Contains(jäger2));
        
        Assert.IsTrue(deutschland.ContainsEnemies(usa));
        Assert.IsFalse(deutschland.ContainsEnemies(japan));
        Assert.IsTrue(ostsee.ContainsEnemies(deutsch));
    }
}