using System.Collections.Generic;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;
using Model.Factories;
using NUnit.Framework;

namespace Great_Powers.unittest;

public class UnitsTest{
    [SetUp]
    public void Setup(){
    }

    [Test]
    public void CheckNonCombatMovement(){
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
            Nation = deutsch
        };
        LandRegion polen = new LandRegion(){
            Name = "polen",
            Nation = deutsch
        };
        LandRegion dänemark = new LandRegion(){
            Name = "dänemark",
            Nation = japan
        };
        LandRegion russland = new LandRegion(){
            Name = "russland",
            Nation = gb
        };
        LandRegion tschechien = new LandRegion(){
            Name = "tschechien",
            Nation = usa
        };
        LandRegion china = new LandRegion(){
            Name = "china",
            Nation = japan
        };
        LandRegion finnland = new LandRegion(){
            Name = "finnland",
            Nation = deutsch
        };
        LandRegion ukraine = new LandRegion(){
            Name = "ukraine",
            Nation = deutsch
        };

        WaterRegion nordsee = new WaterRegion(){
            Name = "nordsee"
        };
        WaterRegion ostsee = new WaterRegion(){
            Name = "ostsee"
        };
        WaterRegion atlantik = new WaterRegion(){
            Name = "atlantik"
        };
        WaterRegion afrikaUmrundung = new WaterRegion(){
            Name = "afrikaUmrundung"
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
        
        #endregion

        Assert.AreEqual(ostsee,aircraftCarrier.GetLocation());
        Assert.IsFalse(aircraftCarrier.SetTarget(EPhase.NonCombatMove,afrikaUmrundung));
        Assert.IsTrue(aircraftCarrier.SetTarget(EPhase.NonCombatMove,nordsee));
        Assert.AreEqual(2,aircraftCarrier.GetPathToTarget(EPhase.NonCombatMove).Count);
        Assert.IsTrue(aircraftCarrier.MoveToTarget(EPhase.NonCombatMove));
        Assert.AreEqual(nordsee,aircraftCarrier.GetLocation());
        
        Assert.AreEqual(deutschland,infantrie.GetLocation());
        Assert.IsFalse(infantrie.SetTarget(EPhase.NonCombatMove, tschechien));
        Assert.IsFalse(infantrie.SetTarget(EPhase.NonCombatMove, ukraine));
        Assert.IsTrue(infantrie.SetTarget(EPhase.NonCombatMove, polen));
        Assert.AreEqual(2,infantrie.GetPathToTarget(EPhase.NonCombatMove).Count);
        Assert.IsTrue(infantrie.MoveToTarget(EPhase.NonCombatMove));
        Assert.AreEqual(polen,infantrie.GetLocation());
        Assert.AreEqual(0,infantrie.CurrentMovement);

        LandRegion filler = new LandRegion(){
            Name = "filler",
            Nation = deutsch
        };
        LandRegion filler2 = new LandRegion(){
            Name = "filler2",
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
        Assert.IsFalse(jäger.SetTarget(EPhase.NonCombatMove,filler2));
        Assert.AreEqual(null,jäger.GetTarget());
        Assert.IsTrue(jäger.SetTarget(EPhase.NonCombatMove,china));
        Assert.AreEqual(4,jäger.GetPathToTarget(EPhase.NonCombatMove).Count);
        Assert.IsTrue(jäger.MoveToTarget(EPhase.NonCombatMove));
        Assert.AreEqual(china,jäger.GetLocation());
        Assert.AreEqual(1,jäger.CurrentMovement);
        
        Assert.AreEqual(deutschland,panzer.GetLocation());
        Assert.IsFalse(panzer.SetTarget(EPhase.NonCombatMove,tschechien));
        Assert.IsTrue(panzer.SetTarget(EPhase.NonCombatMove,ukraine));
        Assert.AreEqual(3,panzer.GetPathToTarget(EPhase.NonCombatMove).Count);
        Assert.IsTrue(panzer.MoveToTarget(EPhase.NonCombatMove));
        Assert.AreEqual(ukraine,panzer.GetLocation());
        Assert.AreEqual(0,panzer.CurrentMovement);
    }
}