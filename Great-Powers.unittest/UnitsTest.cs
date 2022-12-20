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
    public void CheckIfReachableTest(){
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
        LandRegion japan = new LandRegion(){
            Name = "japan",
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
        WaterRegion ostAtlantik = new WaterRegion(){
            Name = "ostAtlantik",
            Type = ERegionType.WATER
        };
        WaterRegion chinesischesMeer = new WaterRegion(){
            Name = "chinesischesMeer",
            Type = ERegionType.WATER
        };
        WaterRegion westAtlantik = new WaterRegion(){
            Name = "westAtlantik",
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
                Neighbour = ostAtlantik
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
        ostAtlantik.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = ostAtlantik,
                Neighbour = nordsee
            },
            new Neighbours(){
                Region = ostAtlantik,
                Neighbour = westAtlantik
            }
        };
        westAtlantik.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = westAtlantik,
                Neighbour = ostAtlantik
            }
        };
        china.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = china,
                Neighbour = russland
            },
            new Neighbours(){
                Region = china,
                Neighbour = chinesischesMeer
            }
        };
        chinesischesMeer.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = chinesischesMeer,
                Neighbour = china
            },
            new Neighbours(){
                Region = chinesischesMeer,
                Neighbour = japan
            }
        };
        japan.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = japan,
                Neighbour = chinesischesMeer
            }
        };
        #endregion

        Nation deutsch = new Nation();
        LandUnit panzer = LandUnitFactory.Create(EUnitType.TANK, deutschland, deutsch);
        Plane jäger = PlaneFactory.Create(EUnitType.FIGHTER, deutschland, deutsch);
        Ship kreuzer = ShipFactory.Create(EUnitType.CRUISER, ostsee, deutsch);
        panzer.CurrentMovement = panzer.Movement;
        jäger.CurrentMovement = jäger.Movement;
        kreuzer.CurrentMovement = kreuzer.Movement;
        
        Assert.IsTrue(panzer.CheckIfReachable(dänemark));
        Assert.IsTrue(panzer.CheckIfReachable(russland));
        Assert.IsFalse(panzer.CheckIfReachable(china));
        
        Assert.IsTrue(jäger.CheckIfReachable(dänemark));
        Assert.IsTrue(jäger.CheckIfReachable(russland));
        Assert.IsTrue(jäger.CheckIfReachable(china));
        Assert.IsTrue(jäger.CheckIfReachable(chinesischesMeer));
        Assert.IsFalse(jäger.CheckIfReachable(japan));
        
        Assert.IsTrue(kreuzer.CheckIfReachable(nordsee));
        Assert.IsTrue(kreuzer.CheckIfReachable(ostAtlantik));
        Assert.IsFalse(kreuzer.CheckIfReachable(westAtlantik));
    }
}