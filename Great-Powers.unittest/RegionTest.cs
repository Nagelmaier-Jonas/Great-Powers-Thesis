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

        Assert.IsTrue(deutschland.GetAllNeighbours(1).Count == 5);
        Assert.IsTrue(deutschland.GetNeighboursByType(1, ERegionType.LAND).Count == 3);
        Assert.IsTrue(deutschland.GetNeighboursByType(1, ERegionType.WATER).Count == 2);

        Assert.IsTrue(deutschland.GetAllNeighbours(2).Count == 9);
        Assert.IsTrue(deutschland.GetNeighboursByType(2, ERegionType.LAND).Count == 5);
        Assert.IsTrue(deutschland.GetNeighboursByType(2, ERegionType.WATER).Count == 3);
        Assert.IsTrue(tschechien.GetNeighboursByType(1, ERegionType.WATER).Count == 0);

        Assert.IsTrue(deutschland.GetAllNeighbours(2).Contains(finnland));
        Assert.IsTrue(deutschland.GetAllNeighbours(3).Contains(china));
        Assert.IsTrue(russland.GetNeighboursByType(3, ERegionType.WATER).Contains(atlantik));

        Assert.IsFalse(deutschland.GetAllNeighbours(2).Contains(deutschland));
        
        Assert.IsTrue(deutschland.GetAllNeighboursWithSource(2).Count == 10);
        Assert.IsTrue(deutschland.GetAllNeighboursWithSource(2).Contains(deutschland));
        
        Assert.IsTrue(deutschland.GetNeighboursByTypeWithSource(2,ERegionType.LAND).Count == 6);
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

        Assert.IsTrue(deutschland.GetAllFriendlyNeighbours(2).Count == 5);
        Assert.IsTrue(dänemark.GetAllFriendlyNeighbours(2).Count == 4);
        Assert.IsTrue(deutschland.GetAllFriendlyNeighbours(3).Count == 6);
        Assert.IsFalse(deutschland.GetAllFriendlyNeighbours(2).Contains(russland));
        Assert.IsFalse(deutschland.GetAllFriendlyNeighbours(2).Contains(tschechien));
        
        Assert.IsTrue(deutschland.GetAllFriendlyNeighboursWithSource(2).Count == 6);
        Assert.IsTrue(deutschland.GetAllFriendlyNeighboursWithSource(3).Count == 7);
        Assert.IsTrue(deutschland.GetAllFriendlyNeighboursWithSource(2).Contains(deutschland));
        Assert.IsFalse(deutschland.GetAllFriendlyNeighboursWithSource(2).Contains(tschechien));
        Assert.IsTrue(dänemark.GetAllFriendlyNeighboursWithSource(2).Contains(dänemark));
        Assert.IsTrue(dänemark.GetAllFriendlyNeighboursWithSource(2).Count == 5);
        
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

    [Test]
    public void GetDistanceTest(){
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
        
        Assert.IsTrue(deutschland.GetMinimalDistance(dänemark) == 1);
        Assert.IsTrue(deutschland.GetMinimalDistance(russland) == 2);
        Assert.IsTrue(deutschland.GetMinimalDistance(finnland) == 2);
        Assert.IsTrue(deutschland.GetMinimalDistance(ukraine) == 2);
        
        Assert.IsTrue(deutschland.GetMinimalDistanceWithMax(ukraine,3) == 2);
        Assert.IsTrue(deutschland.GetMinimalDistanceWithMax(ukraine,1) == 0);
        
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendlies(dänemark) == 1);
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendlies(russland) == 0);
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendlies(finnland) == 0);
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendlies(atlantik) == 2);
        
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendliesWithMax(atlantik,3) == 2);
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendliesWithMax(atlantik,1) == 0);
        
        Assert.IsTrue(deutschland.GetMinimalDistanceByLand(atlantik) == 0);
        Assert.IsTrue(deutschland.GetMinimalDistanceByLand(dänemark) == 1);
        Assert.IsTrue(deutschland.GetMinimalDistanceByLand(russland) == 2);
        Assert.IsTrue(deutschland.GetMinimalDistanceByLand(finnland) == 3);
        
        Assert.IsTrue(deutschland.GetMinimalDistanceByLandWithMax(finnland, 4) == 3);
        Assert.IsTrue(deutschland.GetMinimalDistanceByLandWithMax(finnland, 2) == 0);
        
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendlyLand(finnland) == 0);
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendlyLand(dänemark) == 1);
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendlyLand(russland) == 0);
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendlyLand(atlantik) == 0);
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendlyLand(ukraine) == 2);
        
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendlyLandWithMax(ukraine,3) == 2);
        Assert.IsTrue(deutschland.GetMinimalDistanceByFriendlyLandWithMax(ukraine, 1) == 0);
    }
    
    [Test]
    public void GetPathTest(){
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
        
        Assert.IsTrue(deutschland.GetPathToTarget(ukraine).Count == 3);
        Assert.IsTrue(deutschland.GetPathToTarget(ukraine).Contains(deutschland));
        Assert.IsTrue(deutschland.GetPathToTarget(ukraine).Contains(polen));
        Assert.IsTrue(deutschland.GetPathToTarget(ukraine).Contains(ukraine));
        
        Assert.IsTrue(deutschland.GetPathToTarget(china).Count == 4);
        Assert.IsTrue(deutschland.GetPathToTarget(china).Contains(deutschland));
        Assert.IsTrue(deutschland.GetPathToTarget(china).Contains(polen));
        Assert.IsTrue(deutschland.GetPathToTarget(china).Contains(russland));
        Assert.IsTrue(deutschland.GetPathToTarget(china).Contains(china));
        
        Assert.IsTrue(deutschland.GetPathToTargetByFriendlies(china).Count == 5);
        Assert.IsTrue(deutschland.GetPathToTargetByFriendlies(china).Contains(deutschland));
        Assert.IsTrue(deutschland.GetPathToTargetByFriendlies(china).Contains(nordsee));
        Assert.IsTrue(deutschland.GetPathToTargetByFriendlies(china).Contains(atlantik));
        Assert.IsTrue(deutschland.GetPathToTargetByFriendlies(china).Contains(afrikaUmrundung));
        Assert.IsTrue(deutschland.GetPathToTargetByFriendlies(china).Contains(china));
        
        Assert.IsTrue(deutschland.GetPathToTarget(finnland).Count == 3);
        Assert.IsTrue(deutschland.GetPathToTarget(finnland).Contains(deutschland));
        Assert.IsTrue(deutschland.GetPathToTarget(finnland).Contains(ostsee));
        Assert.IsTrue(deutschland.GetPathToTarget(finnland).Contains(finnland));
        
        Assert.IsTrue(deutschland.GetPathToTargetByLand(finnland).Count == 4);
        Assert.IsTrue(deutschland.GetPathToTargetByLand(finnland).Contains(deutschland));
        Assert.IsTrue(deutschland.GetPathToTargetByLand(finnland).Contains(polen));
        Assert.IsTrue(deutschland.GetPathToTargetByLand(finnland).Contains(russland));
        Assert.IsTrue(deutschland.GetPathToTargetByLand(finnland).Contains(finnland));
        
        Assert.IsTrue(deutschland.GetPathToTargetWithMax(china,6).Count == 4);
        Assert.IsTrue(deutschland.GetPathToTargetWithMax(china,3).Count == 0);
        
        Assert.IsTrue(deutschland.GetPathToTargetByFriendlyLand(china).Count == 0);
        Assert.IsTrue(deutschland.GetPathToTargetByFriendlyLand(finnland).Count == 0);
        Assert.IsTrue(deutschland.GetPathToTargetByFriendlyLand(ukraine).Count == 3);
        Assert.IsTrue(deutschland.GetPathToTargetByFriendlyLand(dänemark).Count == 2);
        Assert.IsTrue(deutschland.GetPathToTargetByFriendlyLand(tschechien).Count == 0);

        #region pathRegions
        
        
        LandRegion d = new LandRegion(){
            Name = "d",
            Type = ERegionType.LAND,
            Nation = deutsch
        };
        LandRegion d1 = new LandRegion(){
            Name = "d1",
            Type = ERegionType.LAND,
            Nation = deutsch
        };
        LandRegion d2 = new LandRegion(){
            Name = "d2",
            Type = ERegionType.LAND,
            Nation = deutsch
        };
        LandRegion d3 = new LandRegion(){
            Name = "d3",
            Type = ERegionType.LAND,
            Nation = deutsch
        };
        LandRegion d4 = new LandRegion(){
            Name = "d4",
            Type = ERegionType.LAND,
            Nation = deutsch
        };
        LandRegion d5 = new LandRegion(){
            Name = "d5",
            Type = ERegionType.LAND,
            Nation = deutsch
        };

        d.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = d,
                Neighbour = d1
            },
            new Neighbours(){
                Region = d,
                Neighbour = d2
            }
        };
        d1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = d1,
                Neighbour = d
            },
            new Neighbours(){
                Region = d1,
                Neighbour = d3
            }
        };
        d2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = d2,
                Neighbour = d
            },
            new Neighbours(){
                Region = d2,
                Neighbour = d4
            }
        };
        d3.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = d3,
                Neighbour = d1
            },
            new Neighbours(){
                Region = d3,
                Neighbour = d5
            }
        };
        d4.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = d4,
                Neighbour = d2
            },
            new Neighbours(){
                Region = d4,
                Neighbour = d5
            }
        };
        d5.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = d5,
                Neighbour = d3
            },
            new Neighbours(){
                Region = d5,
                Neighbour = d4
            }
        };
        #endregion
        
        Assert.IsTrue(d.GetPathToTarget(d5).Count == 4);
        Assert.IsTrue(d1.GetPathToTarget(d5).Count == 3);
        Assert.IsTrue(d1.GetPathToTargetByFriendlies(d5).Count == 3);
        Assert.IsTrue(d1.GetPathToTargetByLand(d5).Count == 3);
        
    }
}