using System.Collections.Generic;
using Domain.Factories;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;
using NUnit.Framework;

namespace Great_Powers.unittest;

public class UnitsTest{
    [SetUp]
    public void Setup(){
    }

    [Test]
    public void CheckGetPathToCurrentTarget(){
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
        
        Ship submarine = ShipFactory.Create(EUnitType.SUBMARINE, atlantik, usa);

        deutschland.StationedUnits = new List<LandUnit>(){
            panzer,
            infantrie
        };
        deutschland.StationedPlanes = new List<Plane>(){
            jäger
        };
        ostsee.StationedShips = new List<Ship>(){
            aircraftCarrier
        };
        ostsee.StationedPlanes = new List<Plane>(){
            jäger2
        };
        atlantik.StationedShips = new List<Ship>(){
            submarine
        };

        Assert.AreEqual(ostsee,aircraftCarrier.GetLocation());
        Assert.IsFalse(aircraftCarrier.SetTarget(atlantik));
        Assert.IsTrue(aircraftCarrier.SetTarget(nordsee));
        Assert.AreEqual(2,aircraftCarrier.GetPathToCurrentTarget().Count);
        Assert.IsTrue(aircraftCarrier.MoveToTarget());
        Assert.AreEqual(nordsee,aircraftCarrier.GetLocation());
        
        Assert.AreEqual(deutschland,infantrie.GetLocation());
        Assert.IsFalse(infantrie.SetTarget(tschechien));
        Assert.IsFalse(infantrie.SetTarget(ukraine));
        Assert.IsTrue(infantrie.SetTarget(polen));
        Assert.AreEqual(2,infantrie.GetPathToCurrentTarget().Count);
        Assert.IsTrue(infantrie.MoveToTarget());
        Assert.AreEqual(polen,infantrie.GetLocation());
        Assert.AreEqual(0,infantrie.CurrentMovement);

        LandRegion filler = new LandRegion(){
            Name = "filler",
            Type = ERegionType.LAND,
            Nation = deutsch
        };
        LandRegion filler2 = new LandRegion(){
            Name = "filler2",
            Type = ERegionType.LAND,
            Nation = gb
        };
        
        china.Neighbours.Add(new Neighbours(){
            Region = china,
            Neighbour = filler
        });
        filler.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = filler,
                Neighbour = china
            },
            new Neighbours(){
                Region = filler,
                Neighbour = filler2
            }
        };
        filler2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = filler2,
                Neighbour = filler
            }
        };
        
        Assert.AreEqual(deutschland, jäger.GetLocation());
        Assert.IsFalse(jäger.SetTarget(filler2));
        Assert.AreEqual(null,jäger.GetTarget());
        Assert.IsTrue(jäger.SetTarget(china));
        Assert.AreEqual(4,jäger.GetPathToCurrentTarget().Count);
        Assert.IsTrue(jäger.MoveToTarget());
        Assert.AreEqual(china,jäger.GetLocation());
        Assert.AreEqual(1,jäger.CurrentMovement);
        
        Assert.AreEqual(deutschland,panzer.GetLocation());
        Assert.IsFalse(panzer.SetTarget(tschechien));
        Assert.IsTrue(panzer.SetTarget(ukraine));
        Assert.AreEqual(3,panzer.GetPathToCurrentTarget().Count);
        Assert.IsTrue(panzer.MoveToTarget());
        Assert.AreEqual(ukraine,panzer.GetLocation());
        Assert.AreEqual(0,panzer.CurrentMovement);

        #endregion
    }
}