using System.Text.Json;
using DataTransfer;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace Unittests;

public class Tests{
    [SetUp]
    public void Setup(){
    }

    /*[Test]
    public void NonCombatLandMovementTest(){
        Nation self = new Nation(){
            Name = "self"
        };
        Nation friendly = new Nation(){
            Name = "friendly"
        };
        Nation enemy = new Nation(){
            Name = "enemy"
        };

        self.Allies = new List<Allies>(){
            new Allies(){
                Nation = self,
                Ally = friendly
            }
        };
        friendly.Allies = new List<Allies>(){
            new Allies(){
                Nation = friendly,
                Ally = self
            }
        };

        LandRegion landSource = new LandRegion(){
            Name = "source",
            Nation = self
        };


        LandRegion landF1 = new LandRegion(){
            Name = "landF1",
            Nation = self
        };

        LandRegion landF2 = new LandRegion(){
            Name = "landF2",
            Nation = friendly
        };

        LandRegion landF3 = new LandRegion(){
            Name = "landF3",
            Nation = self
        };

        landSource.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landSource,
                Neighbour = landF1
            }
        };

        landF1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landF1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = landF1,
                Neighbour = landF2
            }
        };
        landF2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landF2,
                Neighbour = landF1
            },
            new Neighbours(){
                Region = landF2,
                Neighbour = landF3
            }
        };
        landF3.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landF3,
                Neighbour = landF2
            }
        };

        LandRegion landFE1 = new LandRegion(){
            Name = "landFE1",
            Nation = friendly
        };

        LandRegion landFE2 = new LandRegion(){
            Name = "landFE2",
            Nation = self
        };

        LandRegion landFE3 = new LandRegion(){
            Name = "landFE3",
            Nation = enemy
        };

        landSource.Neighbours.Add(
            new Neighbours(){
                Region = landSource,
                Neighbour = landFE1
            }
        );
        landFE1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landFE1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = landFE1,
                Neighbour = landFE2
            }
        };
        landFE2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landFE2,
                Neighbour = landFE1
            },
            new Neighbours(){
                Region = landFE2,
                Neighbour = landFE3
            }
        };
        landFE3.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landFE3,
                Neighbour = landFE2
            }
        };

        LandRegion landBlitz1 = new LandRegion(){
            Name = "landBlitz1",
            Nation = enemy
        };

        LandRegion landBlitz2 = new LandRegion(){
            Name = "landBlitz2",
            Nation = enemy
        };

        landSource.Neighbours.Add(new Neighbours(){
            Region = landSource,
            Neighbour = landBlitz1
        });


        landBlitz1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landBlitz1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = landBlitz1,
                Neighbour = landBlitz2
            }
        };

        landBlitz2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landBlitz2,
                Neighbour = landBlitz1
            }
        };

        WaterRegion water1 = new WaterRegion(){
            Name = "water1"
        };

        landSource.Neighbours.Add(new Neighbours(){
            Region = landSource,
            Neighbour = water1
        });

        water1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = water1,
                Neighbour = landSource
            }
        };

        ALandUnit infantry = LandUnitFactory.CreateInfantry(landSource, self);
        ALandUnit tank = LandUnitFactory.CreateTank(landSource, self);
        ALandUnit artillery = LandUnitFactory.CreateArtillery(landSource, self);
        ALandUnit antiair = LandUnitFactory.CreateAntiAir(landSource, self);
        ALandUnit transTank = LandUnitFactory.CreateTank(landF1, self);

        ALandUnit transInf1 = LandUnitFactory.CreateInfantry(null, self);
        ALandUnit transInf2 = LandUnitFactory.CreateInfantry(null, self);

        landSource.StationedUnits = new List<ALandUnit>(){
            infantry,
            tank,
            artillery,
            antiair
        };

        WaterRegion transWater1 = new WaterRegion(){
            Name = "transWater1"
        };
        WaterRegion transWater2 = new WaterRegion(){
            Name = "transWater2"
        };
        WaterRegion transWater3 = new WaterRegion(){
            Name = "transWater3"
        };
        WaterRegion transWater4 = new WaterRegion(){
            Name = "transWater4"
        };
        WaterRegion transWater5 = new WaterRegion(){
            Name = "transWater5"
        };
        WaterRegion transWater6 = new WaterRegion(){
            Name = "transWater6"
        };

        AShip transport = ShipFactory.CreateTransport(transWater1, self);
        transWater1.StationedShips = new List<AShip>(){
            transport
        };

        AShip transport2 = ShipFactory.CreateTransport(transWater2, self);
        transWater2.StationedShips = new List<AShip>(){
            transport2
        };

        AShip transport3 = ShipFactory.CreateTransport(transWater4, self);
        transWater4.StationedShips = new List<AShip>(){
            transport3
        };

        AShip transport4 = ShipFactory.CreateTransport(transWater5, self);
        transWater5.StationedShips = new List<AShip>(){
            transport4
        };

        AShip transport5 = ShipFactory.CreateTransport(transWater6, self);
        Transport tran = (Transport)transport5;
        tran.Units.Add(transInf1);
        tran.Units.Add(transInf2);
        transport5 = tran;
        transWater6.StationedShips = new List<AShip>(){
            transport5
        };

        transWater3.IncomingUnits.Add(transport2);
        transWater3.IncomingUnits.Add(infantry);

        transWater1.IncomingUnits.Add(infantry);

        transWater5.IncomingUnits.Add(infantry);
        transWater5.IncomingUnits.Add(tank);

        landSource.Neighbours.Add(new Neighbours(){
            Region = landSource,
            Neighbour = transWater1
        });

        landSource.Neighbours.Add(new Neighbours(){
            Region = landSource,
            Neighbour = transWater3
        });

        landSource.Neighbours.Add(new Neighbours(){
            Region = landSource,
            Neighbour = transWater4
        });

        landSource.Neighbours.Add(new Neighbours(){
            Region = landSource,
            Neighbour = transWater5
        });

        landSource.Neighbours.Add(new Neighbours(){
            Region = landSource,
            Neighbour = transWater6
        });

        transWater1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = transWater1,
                Neighbour = landSource
            }
        };

        transWater3.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = transWater3,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = transWater3,
                Neighbour = transWater2
            }
        };

        transWater2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = transWater2,
                Neighbour = transWater3
            }
        };

        transWater4.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = transWater4,
                Neighbour = landSource
            }
        };

        transWater5.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = transWater5,
                Neighbour = landSource
            }
        };

        transWater6.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = transWater6,
                Neighbour = landSource
            }
        };

        Assert.IsTrue(infantry.SetTarget(EPhase.NonCombatMove, landF1));
        Assert.IsFalse(infantry.SetTarget(EPhase.NonCombatMove, landF2));
        Assert.IsFalse(infantry.SetTarget(EPhase.NonCombatMove, landBlitz1));
        Assert.IsFalse(infantry.SetTarget(EPhase.NonCombatMove, landBlitz2));
        Assert.IsFalse(infantry.SetTarget(EPhase.NonCombatMove, water1));

        Assert.IsTrue(tank.SetTarget(EPhase.NonCombatMove, landF1));
        Assert.IsTrue(tank.SetTarget(EPhase.NonCombatMove, landF2));
        Assert.IsFalse(tank.SetTarget(EPhase.NonCombatMove, landBlitz1));
        Assert.IsFalse(tank.SetTarget(EPhase.NonCombatMove, landBlitz2));
        Assert.IsFalse(tank.SetTarget(EPhase.NonCombatMove, water1));

        Assert.IsTrue(artillery.SetTarget(EPhase.NonCombatMove, landF1));
        Assert.IsFalse(artillery.SetTarget(EPhase.NonCombatMove, landF2));
        Assert.IsFalse(artillery.SetTarget(EPhase.NonCombatMove, landBlitz1));
        Assert.IsFalse(artillery.SetTarget(EPhase.NonCombatMove, landBlitz2));
        Assert.IsFalse(artillery.SetTarget(EPhase.NonCombatMove, water1));

        Assert.IsTrue(antiair.SetTarget(EPhase.NonCombatMove, landF1));
        Assert.IsFalse(antiair.SetTarget(EPhase.NonCombatMove, landF2));
        Assert.IsFalse(antiair.SetTarget(EPhase.NonCombatMove, landBlitz1));
        Assert.IsFalse(antiair.SetTarget(EPhase.NonCombatMove, landBlitz2));
        Assert.IsFalse(antiair.SetTarget(EPhase.NonCombatMove, water1));

        Assert.IsTrue(tank.SetTarget(EPhase.NonCombatMove, transWater1));

        Assert.IsTrue(transTank.SetTarget(EPhase.NonCombatMove, transWater3));

        Assert.IsTrue(transTank.SetTarget(EPhase.NonCombatMove, transWater4));
        Assert.IsTrue(artillery.SetTarget(EPhase.NonCombatMove, transWater4));

        Assert.IsFalse(artillery.SetTarget(EPhase.NonCombatMove, transWater5));
        Assert.IsFalse(transTank.SetTarget(EPhase.NonCombatMove, transWater5));

        Assert.IsFalse(artillery.SetTarget(EPhase.NonCombatMove, transWater6));
        Assert.IsFalse(transTank.SetTarget(EPhase.NonCombatMove, transWater6));
        Assert.IsFalse(tank.SetTarget(EPhase.NonCombatMove, transWater6));
        Assert.IsFalse(infantry.SetTarget(EPhase.NonCombatMove, transWater6));
    }

    [Test]
    public void CombatLandMovementTest(){
        Nation self = new Nation(){
            Name = "self"
        };
        Nation friendly = new Nation(){
            Name = "friendly"
        };
        Nation enemy = new Nation(){
            Name = "enemy"
        };

        self.Allies = new List<Allies>(){
            new Allies(){
                Nation = self,
                Ally = friendly
            }
        };
        friendly.Allies = new List<Allies>(){
            new Allies(){
                Nation = friendly,
                Ally = self
            }
        };

        LandRegion landSource = new LandRegion(){
            Name = "landSource",
            Nation = self
        };

        LandRegion landE = new LandRegion(){
            Name = "landE",
            Nation = enemy
        };

        LandRegion landS1 = new LandRegion(){
            Name = "landS1",
            Nation = self
        };

        LandRegion landE1 = new LandRegion(){
            Name = "landE1",
            Nation = enemy
        };

        LandRegion landF2 = new LandRegion(){
            Name = "landF2",
            Nation = friendly
        };

        LandRegion landE2 = new LandRegion(){
            Name = "landE2",
            Nation = enemy
        };

        LandRegion blitz1 = new LandRegion(){
            Name = "blitz1",
            Nation = enemy
        };

        LandRegion blitz2 = new LandRegion(){
            Name = "blitz2",
            Nation = enemy
        };

        LandRegion blitzE1 = new LandRegion(){
            Name = "blitzE1",
            Nation = enemy
        };

        LandRegion blitzE2 = new LandRegion(){
            Name = "blitzE2",
            Nation = enemy
        };

        LandRegion attackE1 = new LandRegion(){
            Name = "attackE1",
            Nation = enemy
        };

        LandRegion attackE2 = new LandRegion(){
            Name = "attackE2",
            Nation = enemy
        };

        LandRegion attackSE2 = new LandRegion(){
            Name = "attackSE2",
            Nation = enemy
        };

        LandRegion attackFE2 = new LandRegion(){
            Name = "attackFE2",
            Nation = enemy
        };

        LandRegion blitzF1 = new LandRegion(){
            Name = "bltizF1",
            Nation = enemy
        };
        LandRegion blitzF2 = new LandRegion(){
            Name = "bltizF2",
            Nation = friendly
        };

        LandRegion blitzFE1 = new LandRegion(){
            Name = "bltizFE1",
            Nation = enemy
        };
        LandRegion blitzFE2 = new LandRegion(){
            Name = "bltizFE2",
            Nation = friendly
        };

        WaterRegion water1 = new WaterRegion(){
            Name = "water1"
        };

        ALandUnit infantry = LandUnitFactory.CreateInfantry(landSource, self);
        ALandUnit artillery = LandUnitFactory.CreateArtillery(landSource, self);
        ALandUnit antiair = LandUnitFactory.CreateAntiAir(landSource, self);
        ALandUnit tank = LandUnitFactory.CreateTank(landSource, self);

        landSource.StationedUnits = new List<ALandUnit>(){
            infantry,
            artillery,
            antiair,
            tank
        };

        ALandUnit friendlyInf = LandUnitFactory.CreateInfantry(landF2, friendly);

        landF2.StationedUnits = new List<ALandUnit>(){
            friendlyInf
        };

        ALandUnit enemyInf = LandUnitFactory.CreateInfantry(blitzE1, enemy);
        ALandUnit enemyInf2 = LandUnitFactory.CreateInfantry(blitz2, enemy);
        ALandUnit enemyInf3 = LandUnitFactory.CreateInfantry(landE, enemy);
        ALandUnit enemyInf4 = LandUnitFactory.CreateInfantry(blitzFE1, enemy);

        blitzE1.StationedUnits = new List<ALandUnit>(){
            enemyInf
        };
        blitz2.StationedUnits = new List<ALandUnit>(){
            enemyInf2
        };
        landE.StationedUnits = new List<ALandUnit>(){
            enemyInf3
        };
        blitzFE1.StationedUnits = new List<ALandUnit>(){
            enemyInf4
        };

        landSource.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landSource,
                Neighbour = landE
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = landF2
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = landS1
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = water1
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = blitz1
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = blitzE1
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = attackE1
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = blitzF1
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = blitzFE1
            }
        };

        landE.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landE,
                Neighbour = landSource
            }
        };

        blitzF1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = blitzF1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = blitzF1,
                Neighbour = blitzF2
            }
        };

        blitzF2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = blitzF2,
                Neighbour = blitzF1
            }
        };

        blitzFE1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = blitzFE1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = blitzFE1,
                Neighbour = blitzFE2
            }
        };

        blitzFE2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = blitzFE2,
                Neighbour = blitzFE1
            }
        };

        landS1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landS1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = landS1,
                Neighbour = landE1
            },
            new Neighbours(){
                Region = landS1,
                Neighbour = attackSE2
            }
        };

        landE1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landE1,
                Neighbour = landS1
            }
        };

        attackSE2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = attackSE2,
                Neighbour = landS1
            }
        };

        landF2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landF2,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = landF2,
                Neighbour = landE2
            },
            new Neighbours(){
                Region = landF2,
                Neighbour = attackFE2
            }
        };

        landE2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landE2,
                Neighbour = landF2
            }
        };

        attackFE2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = attackFE2,
                Neighbour = landF2
            }
        };

        water1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = water1,
                Neighbour = landSource
            }
        };

        blitz1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = blitz1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = blitz1,
                Neighbour = blitz2
            }
        };

        blitz2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = blitz2,
                Neighbour = blitz1
            }
        };

        blitzE1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = blitzE1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = blitzE1,
                Neighbour = blitzE2
            }
        };

        blitzE2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = blitzE2,
                Neighbour = blitzE1
            }
        };

        attackE1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = attackE1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = attackE1,
                Neighbour = attackE2
            }
        };

        attackE2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = attackE2,
                Neighbour = attackE1
            }
        };

        Assert.IsTrue(infantry.SetTarget(EPhase.CombatMove, landE));
        Assert.IsFalse(infantry.SetTarget(EPhase.CombatMove, landS1));
        Assert.IsFalse(infantry.SetTarget(EPhase.CombatMove, landE1));
        Assert.IsFalse(infantry.SetTarget(EPhase.CombatMove, attackSE2));
        Assert.IsFalse(infantry.SetTarget(EPhase.CombatMove, landF2));
        Assert.IsFalse(infantry.SetTarget(EPhase.CombatMove, landE2));
        Assert.IsFalse(infantry.SetTarget(EPhase.CombatMove, attackFE2));
        Assert.IsFalse(infantry.SetTarget(EPhase.CombatMove, water1));
        Assert.IsTrue(infantry.SetTarget(EPhase.CombatMove, blitz1));
        Assert.IsFalse(infantry.SetTarget(EPhase.CombatMove, blitz2));
        Assert.IsTrue(infantry.SetTarget(EPhase.CombatMove, blitzE1));
        Assert.IsFalse(infantry.SetTarget(EPhase.CombatMove, blitzE2));
        Assert.IsTrue(infantry.SetTarget(EPhase.CombatMove, attackE1));
        Assert.IsFalse(infantry.SetTarget(EPhase.CombatMove, attackE2));

        Assert.IsTrue(artillery.SetTarget(EPhase.CombatMove, landE));
        Assert.IsFalse(artillery.SetTarget(EPhase.CombatMove, landS1));
        Assert.IsFalse(artillery.SetTarget(EPhase.CombatMove, landE1));
        Assert.IsFalse(artillery.SetTarget(EPhase.CombatMove, attackSE2));
        Assert.IsFalse(artillery.SetTarget(EPhase.CombatMove, landF2));
        Assert.IsFalse(artillery.SetTarget(EPhase.CombatMove, landE2));
        Assert.IsFalse(artillery.SetTarget(EPhase.CombatMove, attackFE2));
        Assert.IsFalse(artillery.SetTarget(EPhase.CombatMove, water1));
        Assert.IsTrue(artillery.SetTarget(EPhase.CombatMove, blitz1));
        Assert.IsFalse(artillery.SetTarget(EPhase.CombatMove, blitz2));
        Assert.IsTrue(artillery.SetTarget(EPhase.CombatMove, blitzE1));
        Assert.IsFalse(artillery.SetTarget(EPhase.CombatMove, blitzE2));
        Assert.IsTrue(artillery.SetTarget(EPhase.CombatMove, attackE1));
        Assert.IsFalse(artillery.SetTarget(EPhase.CombatMove, attackE2));

        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, landE));
        Assert.IsFalse(tank.SetTarget(EPhase.CombatMove, landS1));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, landE1));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, attackSE2));
        Assert.IsFalse(tank.SetTarget(EPhase.CombatMove, landF2));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, landE2));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, attackFE2));
        Assert.IsFalse(tank.SetTarget(EPhase.CombatMove, water1));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, blitz1));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, blitz2));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, blitzE1));
        Assert.IsFalse(tank.SetTarget(EPhase.CombatMove, blitzE2));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, attackE1));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, attackE2));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, blitzF1));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, blitzFE1));
        Assert.IsTrue(tank.SetTarget(EPhase.CombatMove, blitzF2));
        Assert.IsFalse(tank.SetTarget(EPhase.CombatMove, blitzFE2));

        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, landE));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, landS1));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, landE1));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, attackSE2));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, landF2));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, landE2));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, attackFE2));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, water1));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, blitz1));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, blitz2));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, blitzE1));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, blitzE2));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, attackE1));
        Assert.IsFalse(antiair.SetTarget(EPhase.CombatMove, attackE2));
    }

    [Test]
    public void NonCombatPlaneMovementTest(){
        Nation self = new Nation(){
            Name = "self"
        };
        Nation friendly = new Nation(){
            Name = "friendly"
        };
        Nation enemy = new Nation(){
            Name = "enemy"
        };

        self.Allies = new List<Allies>(){
            new Allies(){
                Nation = self,
                Ally = friendly
            }
        };
        friendly.Allies = new List<Allies>(){
            new Allies(){
                Nation = friendly,
                Ally = self
            }
        };

        LandRegion landSource = new LandRegion(){
            Name = "landSource",
            Nation = self
        };

        LandRegion landSEESWFW1 = new LandRegion(){
            Name = "landSEESWFW1",
            Nation = self
        };
        LandRegion landSEESWFW2 = new LandRegion(){
            Name = "landSEESWFW2",
            Nation = enemy
        };
        LandRegion landSEESWFW3 = new LandRegion(){
            Name = "landSEESWFW3",
            Nation = enemy
        };
        LandRegion landSEESWFW4 = new LandRegion(){
            Name = "landSEESWFW4",
            Nation = self
        };
        WaterRegion landSEESWFW5 = new WaterRegion(){
            Name = "landSEESWFW5"
        };
        LandRegion landSEESWFW6 = new LandRegion(){
            Name = "landSEESWFW6",
            Nation = friendly
        };
        WaterRegion landSEESWFW7 = new WaterRegion(){
            Name = "landSEESWFW7"
        };

        WaterRegion water1 = new WaterRegion(){
            Name = "water1"
        };

        WaterRegion waterC1 = new WaterRegion(){
            Name = "waterC1"
        };

        WaterRegion waterMC1 = new WaterRegion(){
            Name = "waterMC1"
        };
        WaterRegion waterMC2 = new WaterRegion(){
            Name = "waterMC2"
        };

        WaterRegion waterCE1 = new WaterRegion(){
            Name = "waterCE1"
        };
        WaterRegion waterCE2 = new WaterRegion(){
            Name = "waterCE2"
        };

        WaterRegion waterSource = new WaterRegion(){
            Name = "waterSource"
        };

        AShip carrier1 = ShipFactory.CreateAircraftCarrier(waterC1, self);
        AShip carrier2 = ShipFactory.CreateAircraftCarrier(waterSource, self);
        AShip carrier3 = ShipFactory.CreateAircraftCarrier(waterCE1, self);
        AShip carrier4 = ShipFactory.CreateAircraftCarrier(waterMC1, self);
        AShip carrier5 = ShipFactory.CreateAircraftCarrier(waterCE2, self);

        APlane fighter = PlaneFactory.CreateFighter(landSource, self);
        APlane bomber = PlaneFactory.CreateBomber(landSource, self);
        APlane waterFighter = PlaneFactory.CreateFighter(waterSource, self);
        APlane waterBomber = PlaneFactory.CreateBomber(waterSource, self);
        landSource.StationedPlanes = new List<APlane>(){
            fighter,
            bomber
        };
        waterSource.StationedPlanes = new List<APlane>(){
            waterFighter,
            waterBomber
        };

        AircraftCarrier car = (AircraftCarrier)carrier2;
        car.Planes = new List<APlane>(){
            waterFighter,
            waterBomber
        };
        carrier2 = car;

        waterC1.StationedShips = new List<AShip>(){
            carrier1
        };
        waterSource.StationedShips = new List<AShip>(){
            carrier2
        };
        waterCE1.StationedShips = new List<AShip>(){
            carrier3
        };

        waterMC1.StationedShips = new List<AShip>(){
            carrier4
        };
        waterMC2.IncomingUnits = new List<AUnit>(){
            carrier4
        };

        waterCE2.StationedShips = new List<AShip>(){
            carrier5
        };

        AShip submarine = ShipFactory.CreateSubmarine(waterCE1, enemy);
        AShip cruiser = ShipFactory.CreateCruiser(waterCE2, enemy);
        waterCE1.StationedShips.Add(submarine);
        waterCE2.StationedShips.Add(cruiser);

        landSource.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landSource,
                Neighbour = landSEESWFW1
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = water1
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = waterC1
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = waterCE1
            },
            new Neighbours(){
                Region = landSource,
                Neighbour = waterMC1
            }
        };

        waterSource.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = waterSource,
                Neighbour = landSEESWFW1
            },
            new Neighbours(){
                Region = waterSource,
                Neighbour = water1
            },
            new Neighbours(){
                Region = waterSource,
                Neighbour = waterC1
            },
            new Neighbours(){
                Region = waterSource,
                Neighbour = waterCE1
            },
            new Neighbours(){
                Region = waterSource,
                Neighbour = waterMC1
            }
        };

        landSEESWFW1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landSEESWFW1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = landSEESWFW1,
                Neighbour = waterSource
            },
            new Neighbours(){
                Region = landSEESWFW1,
                Neighbour = landSEESWFW2
            },
        };

        landSEESWFW2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landSEESWFW2,
                Neighbour = landSEESWFW1
            },
            new Neighbours(){
                Region = landSEESWFW2,
                Neighbour = landSEESWFW3
            },
        };

        landSEESWFW3.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landSEESWFW3,
                Neighbour = landSEESWFW2
            },
            new Neighbours(){
                Region = landSEESWFW3,
                Neighbour = landSEESWFW4
            },
        };

        landSEESWFW4.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landSEESWFW4,
                Neighbour = landSEESWFW3
            },
            new Neighbours(){
                Region = landSEESWFW4,
                Neighbour = landSEESWFW5
            },
        };

        landSEESWFW5.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landSEESWFW5,
                Neighbour = landSEESWFW4
            },
            new Neighbours(){
                Region = landSEESWFW5,
                Neighbour = landSEESWFW6
            },
        };

        landSEESWFW6.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landSEESWFW6,
                Neighbour = landSEESWFW5
            },
            new Neighbours(){
                Region = landSEESWFW6,
                Neighbour = landSEESWFW7
            },
        };

        landSEESWFW7.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = landSEESWFW7,
                Neighbour = landSEESWFW6
            }
        };

        water1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = water1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = water1,
                Neighbour = waterSource
            }
        };

        waterC1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = waterC1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = waterC1,
                Neighbour = waterSource
            }
        };

        waterCE1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = waterCE1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = waterCE1,
                Neighbour = waterSource
            },
            new Neighbours(){
                Region = waterCE1,
                Neighbour = waterCE2
            }
        };

        waterCE2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = waterCE2,
                Neighbour = waterCE1
            }
        };

        waterMC1.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = waterMC1,
                Neighbour = landSource
            },
            new Neighbours(){
                Region = waterMC1,
                Neighbour = waterSource
            },
            new Neighbours(){
                Region = waterMC1,
                Neighbour = waterMC2
            }
        };

        waterMC2.Neighbours = new List<Neighbours>(){
            new Neighbours(){
                Region = waterMC2,
                Neighbour = waterMC1
            }
        };
        Assert.IsTrue(carrier4.SetTarget(EPhase.NonCombatMove, waterMC2));

        Assert.IsTrue(fighter.SetTarget(EPhase.NonCombatMove, landSEESWFW1));
        Assert.IsFalse(fighter.SetTarget(EPhase.NonCombatMove, landSEESWFW2));
        Assert.IsFalse(fighter.SetTarget(EPhase.NonCombatMove, landSEESWFW3));
        Assert.IsTrue(fighter.SetTarget(EPhase.NonCombatMove, landSEESWFW4));
        Assert.IsFalse(fighter.SetTarget(EPhase.NonCombatMove, landSEESWFW5));
        Assert.IsFalse(fighter.SetTarget(EPhase.NonCombatMove, landSEESWFW6));
        Assert.IsFalse(fighter.SetTarget(EPhase.NonCombatMove, landSEESWFW7));
        Assert.IsFalse(fighter.SetTarget(EPhase.NonCombatMove, water1));
        Assert.IsTrue(fighter.SetTarget(EPhase.NonCombatMove, waterC1));
        Assert.IsTrue(fighter.SetTarget(EPhase.NonCombatMove, waterCE1));
        Assert.IsTrue(fighter.SetTarget(EPhase.NonCombatMove, waterCE2));
        Assert.IsFalse(fighter.SetTarget(EPhase.NonCombatMove, waterMC1));
        Assert.IsTrue(fighter.SetTarget(EPhase.NonCombatMove, waterMC2));

        Assert.IsTrue(bomber.SetTarget(EPhase.NonCombatMove, landSEESWFW1));
        Assert.IsFalse(bomber.SetTarget(EPhase.NonCombatMove, landSEESWFW2));
        Assert.IsFalse(bomber.SetTarget(EPhase.NonCombatMove, landSEESWFW3));
        Assert.IsTrue(bomber.SetTarget(EPhase.NonCombatMove, landSEESWFW4));
        Assert.IsFalse(bomber.SetTarget(EPhase.NonCombatMove, landSEESWFW5));
        Assert.IsTrue(bomber.SetTarget(EPhase.NonCombatMove, landSEESWFW6));
        Assert.IsFalse(bomber.SetTarget(EPhase.NonCombatMove, landSEESWFW7));
        Assert.IsFalse(bomber.SetTarget(EPhase.NonCombatMove, water1));
        Assert.IsTrue(bomber.SetTarget(EPhase.NonCombatMove, waterC1));
        Assert.IsTrue(bomber.SetTarget(EPhase.NonCombatMove, waterCE1));
        Assert.IsTrue(bomber.SetTarget(EPhase.NonCombatMove, waterCE2));
        Assert.IsFalse(bomber.SetTarget(EPhase.NonCombatMove, waterMC1));
        Assert.IsTrue(bomber.SetTarget(EPhase.NonCombatMove, waterMC2));

        Assert.IsTrue(waterFighter.SetTarget(EPhase.NonCombatMove, landSEESWFW1));
        Assert.IsFalse(waterFighter.SetTarget(EPhase.NonCombatMove, landSEESWFW2));
        Assert.IsFalse(waterFighter.SetTarget(EPhase.NonCombatMove, landSEESWFW3));
        Assert.IsTrue(waterFighter.SetTarget(EPhase.NonCombatMove, landSEESWFW4));
        Assert.IsFalse(waterFighter.SetTarget(EPhase.NonCombatMove, landSEESWFW5));
        Assert.IsFalse(waterFighter.SetTarget(EPhase.NonCombatMove, landSEESWFW6));
        Assert.IsFalse(waterFighter.SetTarget(EPhase.NonCombatMove, landSEESWFW7));
        Assert.IsFalse(waterFighter.SetTarget(EPhase.NonCombatMove, water1));
        Assert.IsTrue(waterFighter.SetTarget(EPhase.NonCombatMove, waterC1));
        Assert.IsTrue(waterFighter.SetTarget(EPhase.NonCombatMove, waterCE1));
        Assert.IsTrue(waterFighter.SetTarget(EPhase.NonCombatMove, waterCE2));
        Assert.IsFalse(waterFighter.SetTarget(EPhase.NonCombatMove, waterMC1));
        Assert.IsTrue(waterFighter.SetTarget(EPhase.NonCombatMove, waterMC2));

        Assert.IsTrue(waterBomber.SetTarget(EPhase.NonCombatMove, landSEESWFW1));
        Assert.IsFalse(waterBomber.SetTarget(EPhase.NonCombatMove, landSEESWFW2));
        Assert.IsFalse(waterBomber.SetTarget(EPhase.NonCombatMove, landSEESWFW3));
        Assert.IsTrue(waterBomber.SetTarget(EPhase.NonCombatMove, landSEESWFW4));
        Assert.IsFalse(waterBomber.SetTarget(EPhase.NonCombatMove, landSEESWFW5));
        Assert.IsTrue(waterBomber.SetTarget(EPhase.NonCombatMove, landSEESWFW6));
        Assert.IsFalse(waterBomber.SetTarget(EPhase.NonCombatMove, landSEESWFW7));
        Assert.IsFalse(waterBomber.SetTarget(EPhase.NonCombatMove, water1));
        Assert.IsTrue(waterBomber.SetTarget(EPhase.NonCombatMove, waterC1));
        Assert.IsTrue(waterBomber.SetTarget(EPhase.NonCombatMove, waterCE1));
        Assert.IsTrue(waterBomber.SetTarget(EPhase.NonCombatMove, waterCE2));
        Assert.IsFalse(waterBomber.SetTarget(EPhase.NonCombatMove, waterMC1));
        Assert.IsTrue(waterBomber.SetTarget(EPhase.NonCombatMove, waterMC2));
    }

    [Test]
    public void CombatPlaneMovementTest(){
        Nation self = new Nation(){
            Name = "self"
        };
        Nation friendly = new Nation(){
            Name = "friendly"
        };
        Nation enemy = new Nation(){
            Name = "enemy"
        };

        self.Allies = new List<Allies>(){
            new Allies(){
                Nation = self,
                Ally = friendly
            }
        };
        friendly.Allies = new List<Allies>(){
            new Allies(){
                Nation = friendly,
                Ally = self
            }
        };

        LandRegion source = new LandRegion(){
            Name = "source",
            Nation = self
        };

        LandRegion attack1 = new LandRegion(){
            Name = "attack1",
            Nation = friendly
        };
        WaterRegion attack2 = new WaterRegion(){
            Name = "attack2"
        };
        LandRegion attack3 = new LandRegion(){
            Name = "attack3",
            Nation = enemy
        };
        LandRegion attack4 = new LandRegion(){
            Name = "attack4",
            Nation = enemy
        };
        LandRegion attack5 = new LandRegion(){
            Name = "attack5",
            Nation = friendly
        };
        LandRegion attack6 = new LandRegion(){
            Name = "attack6",
            Nation = enemy
        };
        LandRegion attack7 = new LandRegion(){
            Name = "attack7",
            Nation = enemy
        };
        LandRegion attack8 = new LandRegion(){
            Name = "attack8",
            Nation = friendly
        };
        LandRegion attack9 = new LandRegion(){
            Name = "attack9",
            Nation = enemy
        };
        WaterRegion attack10 = new WaterRegion(){
            Name = "attack10"
        };
        LandRegion attack11 = new LandRegion(){
            Name = "attack11",
            Nation = enemy
        };
        WaterRegion attack12 = new WaterRegion(){
            Name = "attack12"
        };
        WaterRegion attack13 = new WaterRegion(){
            Name = "attack13"
        };
        LandRegion attack14 = new LandRegion(){
            Name = "attack14",
            Nation = enemy
        };

        APlane fighter = PlaneFactory.CreateFighter(source, self);
        source.StationedPlanes.Add(fighter);
        APlane bomber = PlaneFactory.CreateBomber(source, self);
        source.StationedPlanes.Add(bomber);
        ALandUnit friendlyUnit = LandUnitFactory.CreateInfantry(attack1, friendly);
        attack1.StationedUnits.Add(friendlyUnit);
        AShip enemyShip = ShipFactory.CreateDestroyer(attack2, enemy);
        attack2.StationedShips.Add(enemyShip);
        ALandUnit enemyUnit = LandUnitFactory.CreateInfantry(attack4, enemy);
        attack4.StationedUnits.Add(enemyUnit);
        ALandUnit enemyUnit2 = LandUnitFactory.CreateInfantry(attack7, enemy);
        attack7.StationedUnits.Add(enemyUnit2);
        ALandUnit enemyUnit3 = LandUnitFactory.CreateInfantry(attack9, enemy);
        attack9.StationedUnits.Add(enemyUnit3);
        ALandUnit enemyUnit4 = LandUnitFactory.CreateInfantry(attack11, enemy);
        attack11.StationedUnits.Add(enemyUnit4);
        ALandUnit enemyUnit5 = LandUnitFactory.CreateInfantry(attack14, enemy);
        attack14.StationedUnits.Add(enemyUnit5);

        AShip carrier = ShipFactory.CreateAircraftCarrier(attack10, self);
        attack10.StationedShips.Add(carrier);
        AShip carrier2 = ShipFactory.CreateAircraftCarrier(attack13, self);
        attack13.StationedShips.Add(carrier2);
        attack12.IncomingUnits.Add(carrier2);

        source.Neighbours.Add(
            new Neighbours(){
                Region = source,
                Neighbour = attack1
            });
        
        attack1.Neighbours.Add(new Neighbours(){
            Region = attack1,
            Neighbour = source
        });
        attack1.Neighbours.Add(new Neighbours(){
            Region = attack1,
            Neighbour = attack2
        });
        
        attack2.Neighbours.Add(new Neighbours(){
            Region = attack2,
            Neighbour = attack1
        });
        attack2.Neighbours.Add(new Neighbours(){
            Region = attack2,
            Neighbour = attack3
        });
        attack2.Neighbours.Add(new Neighbours(){
            Region = attack2,
            Neighbour = attack7
        });
        
        attack3.Neighbours.Add(new Neighbours(){
            Region = attack3,
            Neighbour = attack2
        });
        attack3.Neighbours.Add(new Neighbours(){
            Region = attack3,
            Neighbour = attack4
        });
        attack3.Neighbours.Add(new Neighbours(){
            Region = attack3,
            Neighbour = attack5
        });
        attack3.Neighbours.Add(new Neighbours(){
            Region = attack3,
            Neighbour = attack6
        });
        
        attack4.Neighbours.Add(new Neighbours(){
            Region = attack4,
            Neighbour = attack3
        });
        attack4.Neighbours.Add(new Neighbours(){
            Region = attack4,
            Neighbour = attack9
        });
        attack4.Neighbours.Add(new Neighbours(){
            Region = attack4,
            Neighbour = attack11
        });
        
        attack5.Neighbours.Add(new Neighbours(){
            Region = attack5,
            Neighbour = attack3
        });
        
        attack6.Neighbours.Add(new Neighbours(){
            Region = attack6,
            Neighbour = attack3
        });
        
        attack7.Neighbours.Add(new Neighbours(){
            Region = attack7,
            Neighbour = attack2
        });
        attack7.Neighbours.Add(new Neighbours(){
            Region = attack7,
            Neighbour = attack8
        });
        
        attack8.Neighbours.Add(new Neighbours(){
            Region = attack8,
            Neighbour = attack7
        });
        
        attack9.Neighbours.Add(new Neighbours(){
            Region = attack9,
            Neighbour = attack4
        });
        attack9.Neighbours.Add(new Neighbours(){
            Region = attack9,
            Neighbour = attack10
        });
        attack9.Neighbours.Add(new Neighbours(){
            Region = attack9,
            Neighbour = attack14
        });
        
        attack10.Neighbours.Add(new Neighbours(){
            Region = attack10,
            Neighbour = attack9
        });
        
        attack11.Neighbours.Add(new Neighbours(){
            Region = attack11,
            Neighbour = attack4
        });
        attack11.Neighbours.Add(new Neighbours(){
            Region = attack11,
            Neighbour = attack12
        });
        
        attack12.Neighbours.Add(new Neighbours(){
            Region = attack12,
            Neighbour = attack11
        });
        attack12.Neighbours.Add(new Neighbours(){
            Region = attack12,
            Neighbour = attack13
        });
        
        attack13.Neighbours.Add(new Neighbours(){
            Region = attack13,
            Neighbour = attack12
        });
        
        attack14.Neighbours.Add(new Neighbours(){
            Region = attack14,
            Neighbour = attack9
        });

        fighter.Target = attack1;
        Assert.That(fighter.GetPathToTarget(EPhase.CombatMove), Is.EqualTo(new List<ARegion>(){source,attack1}));
        Assert.IsFalse(fighter.SetTarget(EPhase.CombatMove, attack1));
        Assert.IsTrue(fighter.SetTarget(EPhase.CombatMove, attack2));
        Assert.IsFalse(fighter.SetTarget(EPhase.CombatMove, attack3));
        Assert.IsFalse(fighter.SetTarget(EPhase.CombatMove, attack4));
        Assert.IsFalse(fighter.SetTarget(EPhase.CombatMove, attack5));
        Assert.IsFalse(fighter.SetTarget(EPhase.CombatMove, attack6));
        Assert.IsTrue(fighter.SetTarget(EPhase.CombatMove, attack7));
        Assert.IsFalse(fighter.SetTarget(EPhase.CombatMove, attack8));
        
        Assert.IsFalse(bomber.SetTarget(EPhase.CombatMove, attack1));
        Assert.IsTrue(bomber.SetTarget(EPhase.CombatMove, attack2));
        Assert.IsFalse(bomber.SetTarget(EPhase.CombatMove, attack3));
        Assert.IsTrue(bomber.SetTarget(EPhase.CombatMove, attack4));
        Assert.IsFalse(bomber.SetTarget(EPhase.CombatMove, attack5));
        Assert.IsFalse(bomber.SetTarget(EPhase.CombatMove, attack6));
        Assert.IsTrue(bomber.SetTarget(EPhase.CombatMove, attack7));
        Assert.IsFalse(bomber.SetTarget(EPhase.CombatMove, attack8));
        Assert.IsTrue(bomber.SetTarget(EPhase.CombatMove, attack9));
        Assert.IsFalse(bomber.SetTarget(EPhase.CombatMove, attack10));
        Assert.IsTrue(bomber.SetTarget(EPhase.CombatMove, attack11));
        Assert.IsFalse(bomber.SetTarget(EPhase.CombatMove, attack12));
        Assert.IsFalse(bomber.SetTarget(EPhase.CombatMove, attack13));
        Assert.IsFalse(bomber.SetTarget(EPhase.CombatMove, attack14));
    }

    [Test]
    public void NonCombatShipMovementTest(){
        Nation self = new Nation(){
            Name = "self"
        };
        Nation friendly = new Nation(){
            Name = "friendly"
        };
        Nation enemy = new Nation(){
            Name = "enemy"
        };

        self.Allies = new List<Allies>(){
            new Allies(){
                Nation = self,
                Ally = friendly
            }
        };
        friendly.Allies = new List<Allies>(){
            new Allies(){
                Nation = friendly,
                Ally = self
            }
        };

        WaterRegion source = new WaterRegion(){
            Name = "source"
        };
        WaterRegion friendly2 = new WaterRegion(){
            Name = "friendly2"
        };
        WaterRegion friendly3 = new WaterRegion(){
            Name = "friendly3"
        };
        WaterRegion friendly4 = new WaterRegion(){
            Name = "friendly4"
        };
        WaterRegion enemy5 = new WaterRegion(){
            Name = "enemy5"
        };
        WaterRegion enemy6 = new WaterRegion(){
            Name = "enemy6"
        };
        WaterRegion enemy7 = new WaterRegion(){
            Name = "enemy7"
        };
        WaterRegion enemy8 = new WaterRegion(){
            Name = "enemy8"
        };
        WaterRegion friendly9 = new WaterRegion(){
            Name = "friendly9"
        };
        WaterRegion destroyer10 = new WaterRegion(){
            Name = "destroyer10"
        };
        WaterRegion friendly11 = new WaterRegion(){
            Name = "friendly11"
        };
        WaterRegion destroyer12 = new WaterRegion(){
            Name = "destroyer12"
        };
        WaterRegion enemy13 = new WaterRegion(){
            Name = "enemy13"
        };
        WaterRegion neutral14 = new WaterRegion(){
            Name = "neutral14"
        };
        LandRegion landF15 = new LandRegion(){
            Name = "landF15",
            Nation = self
        };
        WaterRegion neutral16 = new WaterRegion(){
            Name = "neutral16"
        };
        LandRegion landE17 = new LandRegion(){
            Name = "landE17",
            Nation = enemy
        };
        WaterRegion enemy18 = new WaterRegion(){
            Name = "enemy18"
        };
        LandRegion landE19 = new LandRegion(){
            Name = "landE19",
            Nation = enemy
        };
        WaterRegion neutral20 = new WaterRegion(){
            Name = "neutral20"
        };
        LandRegion landE21 = new LandRegion(){
            Name = "landE21",
            Nation = enemy
        };

        AShip shipSub = ShipFactory.CreateSubmarine(source, self);
        AShip shipDes = ShipFactory.CreateDestroyer(source, self);
        AShip shipCru = ShipFactory.CreateCruiser(source, self);
        AShip shipBat = ShipFactory.CreateBattleship(source, self);
        AShip shipAir = ShipFactory.CreateAircraftCarrier(source, self);
        AShip shipTra = ShipFactory.CreateTransport(source, self);
        
        source.StationedShips.AddRange(new List<AShip>(){
            shipSub,
            shipDes,
            shipCru,
            shipBat,
            shipAir,
            shipTra
        });

        AShip enemyShip1 = ShipFactory.CreateBattleship(enemy5, enemy);
        enemy5.StationedShips.Add(enemyShip1);
        AShip enemyShip2 = ShipFactory.CreateBattleship(enemy6, enemy);
        enemy6.StationedShips.Add(enemyShip2);
        AShip enemyShip3 = ShipFactory.CreateBattleship(enemy7, enemy);
        enemy7.StationedShips.Add(enemyShip3);
        AShip enemyShip4 = ShipFactory.CreateBattleship(enemy8, enemy);
        enemy8.StationedShips.Add(enemyShip4);
        AShip enemyShip5 = ShipFactory.CreateBattleship(enemy13, enemy);
        enemy13.StationedShips.Add(enemyShip5);
        AShip enemyShip6 = ShipFactory.CreateBattleship(enemy18, enemy);
        enemy18.StationedShips.Add(enemyShip6);
        
        AShip destroyerShip1 = ShipFactory.CreateDestroyer(destroyer10, enemy);
        destroyer10.StationedShips.Add(destroyerShip1);
        AShip destroyerShip2 = ShipFactory.CreateDestroyer(destroyer12, enemy);
        destroyer12.StationedShips.Add(destroyerShip2);
        
        ALandUnit enemyInf1 = LandUnitFactory.CreateInfantry(landE17, enemy);
        landE17.StationedUnits.Add(enemyInf1);
        ALandUnit enemyInf2 = LandUnitFactory.CreateInfantry(landE19, enemy);
        landE19.StationedUnits.Add(enemyInf2);
        
        source.Neighbours.AddRange(new List<Neighbours>(){
            new Neighbours(){
                Region = source,
                Neighbour = friendly2
            },
            new Neighbours(){
                Region = source,
                Neighbour = friendly4
            },
            new Neighbours(){
                Region = source,
                Neighbour = enemy6
            },
            new Neighbours(){
                Region = source,
                Neighbour = enemy8
            },
            new Neighbours(){
                Region = source,
                Neighbour = destroyer10
            },
            new Neighbours(){
                Region = source,
                Neighbour = destroyer12
            },
            new Neighbours(){
                Region = source,
                Neighbour = neutral14
            },
            new Neighbours(){
                Region = source,
                Neighbour = neutral16
            },
            new Neighbours(){
                Region = source,
                Neighbour = enemy18
            },
            new Neighbours(){
                Region = source,
                Neighbour = neutral20
            }
        });
        
        friendly2.Neighbours.Add(new Neighbours(){
            Region = friendly2,
            Neighbour = source
        });
        friendly2.Neighbours.Add(new Neighbours(){
            Region = friendly2,
            Neighbour = friendly3
        });
        
        friendly3.Neighbours.Add(new Neighbours(){
            Region = friendly3,
            Neighbour = friendly2
        });
        
        friendly4.Neighbours.Add(new Neighbours(){
            Region = friendly4,
            Neighbour = source
        });
        friendly4.Neighbours.Add(new Neighbours(){
            Region = friendly4,
            Neighbour = enemy5
        });
        
        enemy5.Neighbours.Add(new Neighbours(){
            Region = enemy5,
            Neighbour = friendly4
        });
        
        enemy6.Neighbours.Add(new Neighbours(){
            Region = enemy6,
            Neighbour = source
        });
        enemy6.Neighbours.Add(new Neighbours(){
            Region = enemy6,
            Neighbour = enemy7
        });
        
        enemy7.Neighbours.Add(new Neighbours(){
            Region = enemy7,
            Neighbour = enemy6
        });
        
        enemy8.Neighbours.Add(new Neighbours(){
            Region = enemy8,
            Neighbour = source
        });
        enemy8.Neighbours.Add(new Neighbours(){
            Region = enemy8,
            Neighbour = friendly9
        });
        
        destroyer10.Neighbours.Add(new Neighbours(){
            Region = destroyer10,
            Neighbour = source
        });
        destroyer10.Neighbours.Add(new Neighbours(){
            Region = destroyer10,
            Neighbour = friendly11
        });
        
        friendly11.Neighbours.Add(new Neighbours(){
            Region = friendly11,
            Neighbour = destroyer10
        });
        
        destroyer12.Neighbours.Add(new Neighbours(){
            Region = destroyer12,
            Neighbour = source
        });
        destroyer12.Neighbours.Add(new Neighbours(){
            Region = destroyer12,
            Neighbour = enemy13
        });
        
        enemy13.Neighbours.Add(new Neighbours(){
            Region = enemy13,
            Neighbour = destroyer12
        });
        
        neutral14.Neighbours.Add(new Neighbours(){
            Region = neutral14,
            Neighbour = source
        });
        neutral14.Neighbours.Add(new Neighbours(){
            Region = neutral14,
            Neighbour = landF15
        });
        
        landF15.Neighbours.Add(new Neighbours(){
            Region = landF15,
            Neighbour = neutral14
        });
        
        neutral16.Neighbours.Add(new Neighbours(){
            Region = neutral16,
            Neighbour = source
        });
        neutral16.Neighbours.Add(new Neighbours(){
            Region = neutral16,
            Neighbour = landE17
        });
        
        landE17.Neighbours.Add(new Neighbours(){
            Region = landE17,
            Neighbour = neutral16
        });
        
        enemy18.Neighbours.Add(new Neighbours(){
            Region = enemy18,
            Neighbour = source
        });
        enemy18.Neighbours.Add(new Neighbours(){
            Region = enemy18,
            Neighbour = landE19
        });
        
        landE19.Neighbours.Add(new Neighbours(){
            Region = landE19,
            Neighbour = enemy18
        });
        
        neutral20.Neighbours.Add(new Neighbours(){
            Region = neutral20,
            Neighbour = source
        });
        neutral20.Neighbours.Add(new Neighbours(){
            Region = neutral20,
            Neighbour = landE21
        });
        
        landE21.Neighbours.Add(new Neighbours(){
            Region = landE21,
            Neighbour = neutral20
        });
        
        Assert.IsTrue(shipSub.SetTarget(EPhase.NonCombatMove,friendly2));
        Assert.IsTrue(shipSub.SetTarget(EPhase.NonCombatMove,friendly3));
        Assert.IsTrue(shipSub.SetTarget(EPhase.NonCombatMove,friendly4));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,enemy5));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,enemy6));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,enemy7));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,enemy8));
        Assert.IsTrue(shipSub.SetTarget(EPhase.NonCombatMove,friendly9));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,destroyer10));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,friendly11));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,destroyer12));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,enemy13));
        Assert.IsTrue(shipSub.SetTarget(EPhase.NonCombatMove,neutral14));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,landF15));
        Assert.IsTrue(shipSub.SetTarget(EPhase.NonCombatMove,neutral16));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,landE17));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,enemy18));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,landE19));
        Assert.IsTrue(shipSub.SetTarget(EPhase.NonCombatMove,neutral20));
        Assert.IsFalse(shipSub.SetTarget(EPhase.NonCombatMove,landE21));
        
        Assert.IsTrue(shipCru.SetTarget(EPhase.NonCombatMove,friendly2));
        Assert.IsTrue(shipCru.SetTarget(EPhase.NonCombatMove,friendly3));
        Assert.IsTrue(shipCru.SetTarget(EPhase.NonCombatMove,friendly4));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,enemy5));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,enemy6));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,enemy7));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,enemy8));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,friendly9));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,destroyer10));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,friendly11));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,destroyer12));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,enemy13));
        Assert.IsTrue(shipCru.SetTarget(EPhase.NonCombatMove,neutral14));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,landF15));
        Assert.IsTrue(shipCru.SetTarget(EPhase.NonCombatMove,neutral16));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,landE17));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,enemy18));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,landE19));
        Assert.IsTrue(shipCru.SetTarget(EPhase.NonCombatMove,neutral20));
        Assert.IsFalse(shipCru.SetTarget(EPhase.NonCombatMove,landE21));
        
        Assert.IsTrue(shipDes.SetTarget(EPhase.NonCombatMove,friendly2));
        Assert.IsTrue(shipDes.SetTarget(EPhase.NonCombatMove,friendly3));
        Assert.IsTrue(shipDes.SetTarget(EPhase.NonCombatMove,friendly4));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,enemy5));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,enemy6));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,enemy7));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,enemy8));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,friendly9));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,destroyer10));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,friendly11));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,destroyer12));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,enemy13));
        Assert.IsTrue(shipDes.SetTarget(EPhase.NonCombatMove,neutral14));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,landF15));
        Assert.IsTrue(shipDes.SetTarget(EPhase.NonCombatMove,neutral16));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,landE17));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,enemy18));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,landE19));
        Assert.IsTrue(shipDes.SetTarget(EPhase.NonCombatMove,neutral20));
        Assert.IsFalse(shipDes.SetTarget(EPhase.NonCombatMove,landE21));
        
        Assert.IsTrue(shipBat.SetTarget(EPhase.NonCombatMove,friendly2));
        Assert.IsTrue(shipBat.SetTarget(EPhase.NonCombatMove,friendly3));
        Assert.IsTrue(shipBat.SetTarget(EPhase.NonCombatMove,friendly4));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,enemy5));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,enemy6));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,enemy7));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,enemy8));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,friendly9));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,destroyer10));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,friendly11));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,destroyer12));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,enemy13));
        Assert.IsTrue(shipBat.SetTarget(EPhase.NonCombatMove,neutral14));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,landF15));
        Assert.IsTrue(shipBat.SetTarget(EPhase.NonCombatMove,neutral16));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,landE17));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,enemy18));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,landE19));
        Assert.IsTrue(shipBat.SetTarget(EPhase.NonCombatMove,neutral20));
        Assert.IsFalse(shipBat.SetTarget(EPhase.NonCombatMove,landE21));
        
        Assert.IsTrue(shipAir.SetTarget(EPhase.NonCombatMove,friendly2));
        Assert.IsTrue(shipAir.SetTarget(EPhase.NonCombatMove,friendly3));
        Assert.IsTrue(shipAir.SetTarget(EPhase.NonCombatMove,friendly4));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,enemy5));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,enemy6));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,enemy7));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,enemy8));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,friendly9));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,destroyer10));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,friendly11));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,destroyer12));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,enemy13));
        Assert.IsTrue(shipAir.SetTarget(EPhase.NonCombatMove,neutral14));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,landF15));
        Assert.IsTrue(shipAir.SetTarget(EPhase.NonCombatMove,neutral16));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,landE17));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,enemy18));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,landE19));
        Assert.IsTrue(shipAir.SetTarget(EPhase.NonCombatMove,neutral20));
        Assert.IsFalse(shipAir.SetTarget(EPhase.NonCombatMove,landE21));
        
        Assert.IsTrue(shipTra.SetTarget(EPhase.NonCombatMove,friendly2));
        Assert.IsTrue(shipTra.SetTarget(EPhase.NonCombatMove,friendly3));
        Assert.IsTrue(shipTra.SetTarget(EPhase.NonCombatMove,friendly4));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,enemy5));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,enemy6));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,enemy7));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,enemy8));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,friendly9));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,destroyer10));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,friendly11));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,destroyer12));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,enemy13));
        Assert.IsTrue(shipTra.SetTarget(EPhase.NonCombatMove,neutral14));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,landF15));
        Assert.IsTrue(shipTra.SetTarget(EPhase.NonCombatMove,neutral16));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,landE17));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,enemy18));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,landE19));
        Assert.IsTrue(shipTra.SetTarget(EPhase.NonCombatMove,neutral20));
        Assert.IsFalse(shipTra.SetTarget(EPhase.NonCombatMove,landE21));
    }
    
    [Test]
    public void CombatShipMovementTest(){
        Nation self = new Nation(){
            Name = "self"
        };
        Nation friendly = new Nation(){
            Name = "friendly"
        };
        Nation enemy = new Nation(){
            Name = "enemy"
        };

        self.Allies = new List<Allies>(){
            new Allies(){
                Nation = self,
                Ally = friendly
            }
        };
        friendly.Allies = new List<Allies>(){
            new Allies(){
                Nation = friendly,
                Ally = self
            }
        };

        WaterRegion source = new WaterRegion(){
            Name = "source"
        };
        WaterRegion friendly2 = new WaterRegion(){
            Name = "friendly2"
        };
        WaterRegion friendly3 = new WaterRegion(){
            Name = "friendly3"
        };
        WaterRegion friendly4 = new WaterRegion(){
            Name = "friendly4"
        };
        WaterRegion enemy5 = new WaterRegion(){
            Name = "enemy5"
        };
        WaterRegion enemy6 = new WaterRegion(){
            Name = "enemy6"
        };
        WaterRegion enemy7 = new WaterRegion(){
            Name = "enemy7"
        };
        WaterRegion enemy8 = new WaterRegion(){
            Name = "enemy8"
        };
        WaterRegion friendly9 = new WaterRegion(){
            Name = "friendly9"
        };
        WaterRegion destroyer10 = new WaterRegion(){
            Name = "destroyer10"
        };
        WaterRegion friendly11 = new WaterRegion(){
            Name = "friendly11"
        };
        WaterRegion destroyer12 = new WaterRegion(){
            Name = "destroyer12"
        };
        WaterRegion enemy13 = new WaterRegion(){
            Name = "enemy13"
        };
        WaterRegion neutral14 = new WaterRegion(){
            Name = "neutral14"
        };
        LandRegion landF15 = new LandRegion(){
            Name = "landF15",
            Nation = self
        };
        WaterRegion neutral16 = new WaterRegion(){
            Name = "neutral16"
        };
        LandRegion landE17 = new LandRegion(){
            Name = "landE17",
            Nation = enemy
        };
        WaterRegion enemy18 = new WaterRegion(){
            Name = "enemy18"
        };
        LandRegion landE19 = new LandRegion(){
            Name = "landE19",
            Nation = enemy
        };
        WaterRegion neutral20 = new WaterRegion(){
            Name = "neutral20"
        };
        LandRegion landE21 = new LandRegion(){
            Name = "landE21",
            Nation = enemy
        };
        WaterRegion transport22 = new WaterRegion(){
            Name = "transport22"
        };
        LandRegion landE23 = new LandRegion(){
            Name = "landE23",
            Nation = enemy
        };

        AShip shipSub = ShipFactory.CreateSubmarine(source, self);
        AShip shipDes = ShipFactory.CreateDestroyer(source, self);
        AShip shipCru = ShipFactory.CreateCruiser(source, self);
        AShip shipBat = ShipFactory.CreateBattleship(source, self);
        AShip shipAir = ShipFactory.CreateAircraftCarrier(source, self);
        AShip shipTra = ShipFactory.CreateTransport(source, self);
        
        source.StationedShips.AddRange(new List<AShip>(){
            shipSub,
            shipDes,
            shipCru,
            shipBat,
            shipAir,
            shipTra
        });

        AShip enemyShip1 = ShipFactory.CreateBattleship(enemy5, enemy);
        enemy5.StationedShips.Add(enemyShip1);
        AShip enemyShip2 = ShipFactory.CreateBattleship(enemy6, enemy);
        enemy6.StationedShips.Add(enemyShip2);
        AShip enemyShip3 = ShipFactory.CreateBattleship(enemy7, enemy);
        enemy7.StationedShips.Add(enemyShip3);
        AShip enemyShip4 = ShipFactory.CreateBattleship(enemy8, enemy);
        enemy8.StationedShips.Add(enemyShip4);
        AShip enemyShip5 = ShipFactory.CreateBattleship(enemy13, enemy);
        enemy13.StationedShips.Add(enemyShip5);
        AShip enemyShip6 = ShipFactory.CreateBattleship(enemy18, enemy);
        enemy18.StationedShips.Add(enemyShip6);
        
        AShip destroyerShip1 = ShipFactory.CreateDestroyer(destroyer10, enemy);
        destroyer10.StationedShips.Add(destroyerShip1);
        AShip destroyerShip2 = ShipFactory.CreateDestroyer(destroyer12, enemy);
        destroyer12.StationedShips.Add(destroyerShip2);
        
        ALandUnit enemyInf1 = LandUnitFactory.CreateInfantry(landE17, enemy);
        landE17.StationedUnits.Add(enemyInf1);
        ALandUnit enemyInf2 = LandUnitFactory.CreateInfantry(landE19, enemy);
        landE19.StationedUnits.Add(enemyInf2);
        ALandUnit enemyInf3 = LandUnitFactory.CreateInfantry(landE23, enemy);
        landE23.StationedUnits.Add(enemyInf3);

        AShip transport = ShipFactory.CreateTransport(transport22, self);
        transport22.StationedShips.Add(transport);
        landE23.IncomingUnits.Add(transport);
        
        AShip transport2 = ShipFactory.CreateTransport(neutral20, self);
        neutral20.StationedShips.Add(transport2);
        
        source.Neighbours.AddRange(new List<Neighbours>(){
            new Neighbours(){
                Region = source,
                Neighbour = friendly2
            },
            new Neighbours(){
                Region = source,
                Neighbour = friendly4
            },
            new Neighbours(){
                Region = source,
                Neighbour = enemy6
            },
            new Neighbours(){
                Region = source,
                Neighbour = enemy8
            },
            new Neighbours(){
                Region = source,
                Neighbour = destroyer10
            },
            new Neighbours(){
                Region = source,
                Neighbour = destroyer12
            },
            new Neighbours(){
                Region = source,
                Neighbour = neutral14
            },
            new Neighbours(){
                Region = source,
                Neighbour = neutral16
            },
            new Neighbours(){
                Region = source,
                Neighbour = enemy18
            },
            new Neighbours(){
                Region = source,
                Neighbour = neutral20
            },
            new Neighbours(){
                Region = source,
                Neighbour = transport22
            }
        });
        
        friendly2.Neighbours.Add(new Neighbours(){
            Region = friendly2,
            Neighbour = source
        });
        friendly2.Neighbours.Add(new Neighbours(){
            Region = friendly2,
            Neighbour = friendly3
        });
        
        friendly3.Neighbours.Add(new Neighbours(){
            Region = friendly3,
            Neighbour = friendly2
        });
        
        friendly4.Neighbours.Add(new Neighbours(){
            Region = friendly4,
            Neighbour = source
        });
        friendly4.Neighbours.Add(new Neighbours(){
            Region = friendly4,
            Neighbour = enemy5
        });
        
        enemy5.Neighbours.Add(new Neighbours(){
            Region = enemy5,
            Neighbour = friendly4
        });
        
        enemy6.Neighbours.Add(new Neighbours(){
            Region = enemy6,
            Neighbour = source
        });
        enemy6.Neighbours.Add(new Neighbours(){
            Region = enemy6,
            Neighbour = enemy7
        });
        
        enemy7.Neighbours.Add(new Neighbours(){
            Region = enemy7,
            Neighbour = enemy6
        });
        
        enemy8.Neighbours.Add(new Neighbours(){
            Region = enemy8,
            Neighbour = source
        });
        enemy8.Neighbours.Add(new Neighbours(){
            Region = enemy8,
            Neighbour = friendly9
        });
        
        destroyer10.Neighbours.Add(new Neighbours(){
            Region = destroyer10,
            Neighbour = source
        });
        destroyer10.Neighbours.Add(new Neighbours(){
            Region = destroyer10,
            Neighbour = friendly11
        });
        
        friendly11.Neighbours.Add(new Neighbours(){
            Region = friendly11,
            Neighbour = destroyer10
        });
        
        destroyer12.Neighbours.Add(new Neighbours(){
            Region = destroyer12,
            Neighbour = source
        });
        destroyer12.Neighbours.Add(new Neighbours(){
            Region = destroyer12,
            Neighbour = enemy13
        });
        
        enemy13.Neighbours.Add(new Neighbours(){
            Region = enemy13,
            Neighbour = destroyer12
        });
        
        neutral14.Neighbours.Add(new Neighbours(){
            Region = neutral14,
            Neighbour = source
        });
        neutral14.Neighbours.Add(new Neighbours(){
            Region = neutral14,
            Neighbour = landF15
        });
        
        landF15.Neighbours.Add(new Neighbours(){
            Region = landF15,
            Neighbour = neutral14
        });
        
        neutral16.Neighbours.Add(new Neighbours(){
            Region = neutral16,
            Neighbour = source
        });
        neutral16.Neighbours.Add(new Neighbours(){
            Region = neutral16,
            Neighbour = landE17
        });
        
        landE17.Neighbours.Add(new Neighbours(){
            Region = landE17,
            Neighbour = neutral16
        });
        
        enemy18.Neighbours.Add(new Neighbours(){
            Region = enemy18,
            Neighbour = source
        });
        enemy18.Neighbours.Add(new Neighbours(){
            Region = enemy18,
            Neighbour = landE19
        });
        
        landE19.Neighbours.Add(new Neighbours(){
            Region = landE19,
            Neighbour = enemy18
        });
        
        neutral20.Neighbours.Add(new Neighbours(){
            Region = neutral20,
            Neighbour = source
        });
        neutral20.Neighbours.Add(new Neighbours(){
            Region = neutral20,
            Neighbour = landE21
        });
        
        landE21.Neighbours.Add(new Neighbours(){
            Region = landE21,
            Neighbour = neutral20
        });
        
        transport22.Neighbours.Add(new Neighbours(){
            Region = transport22,
            Neighbour = source
        });
        transport22.Neighbours.Add(new Neighbours(){
            Region = transport22,
            Neighbour = landE23
        });
        
        landE23.Neighbours.Add(new Neighbours(){
            Region = landE23,
            Neighbour = transport22
        });
        
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,friendly2));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,friendly3));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,friendly4));
        Assert.IsTrue(shipSub.SetTarget(EPhase.CombatMove,enemy5));
        Assert.IsTrue(shipSub.SetTarget(EPhase.CombatMove,enemy6));
        Assert.IsTrue(shipSub.SetTarget(EPhase.CombatMove,enemy7));
        Assert.IsTrue(shipSub.SetTarget(EPhase.CombatMove,enemy8));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,friendly9));
        Assert.IsTrue(shipSub.SetTarget(EPhase.CombatMove,destroyer10));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,friendly11));
        Assert.IsTrue(shipSub.SetTarget(EPhase.CombatMove,destroyer12));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,enemy13));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,neutral14));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,landF15));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,neutral16));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,landE17));
        Assert.IsTrue(shipSub.SetTarget(EPhase.CombatMove,enemy18));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,landE19));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,neutral20));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,landE21));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,transport22));
        Assert.IsFalse(shipSub.SetTarget(EPhase.CombatMove,landE23));
        
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,friendly2));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,friendly3));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,friendly4));
        Assert.IsTrue(shipCru.SetTarget(EPhase.CombatMove,enemy5));
        Assert.IsTrue(shipCru.SetTarget(EPhase.CombatMove,enemy6));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,enemy7));
        Assert.IsTrue(shipCru.SetTarget(EPhase.CombatMove,enemy8));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,friendly9));
        Assert.IsTrue(shipCru.SetTarget(EPhase.CombatMove,destroyer10));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,friendly11));
        Assert.IsTrue(shipCru.SetTarget(EPhase.CombatMove,destroyer12));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,enemy13));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,neutral14));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,landF15));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,neutral16));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,landE17));
        Assert.IsTrue(shipCru.SetTarget(EPhase.CombatMove,enemy18));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,landE19));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,neutral20));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,landE21));
        Assert.IsFalse(shipCru.SetTarget(EPhase.CombatMove,transport22));
        Assert.IsTrue(shipCru.SetTarget(EPhase.CombatMove,landE23));
        
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,friendly2));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,friendly3));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,friendly4));
        Assert.IsTrue(shipDes.SetTarget(EPhase.CombatMove,enemy5));
        Assert.IsTrue(shipDes.SetTarget(EPhase.CombatMove,enemy6));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,enemy7));
        Assert.IsTrue(shipDes.SetTarget(EPhase.CombatMove,enemy8));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,friendly9));
        Assert.IsTrue(shipDes.SetTarget(EPhase.CombatMove,destroyer10));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,friendly11));
        Assert.IsTrue(shipDes.SetTarget(EPhase.CombatMove,destroyer12));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,enemy13));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,neutral14));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,landF15));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,neutral16));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,landE17));
        Assert.IsTrue(shipDes.SetTarget(EPhase.CombatMove,enemy18));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,landE19));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,neutral20));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,landE21));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,transport22));
        Assert.IsFalse(shipDes.SetTarget(EPhase.CombatMove,landE23));
        
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,friendly2));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,friendly3));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,friendly4));
        Assert.IsTrue(shipBat.SetTarget(EPhase.CombatMove,enemy5));
        Assert.IsTrue(shipBat.SetTarget(EPhase.CombatMove,enemy6));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,enemy7));
        Assert.IsTrue(shipBat.SetTarget(EPhase.CombatMove,enemy8));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,friendly9));
        Assert.IsTrue(shipBat.SetTarget(EPhase.CombatMove,destroyer10));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,friendly11));
        Assert.IsTrue(shipBat.SetTarget(EPhase.CombatMove,destroyer12));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,enemy13));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,neutral14));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,landF15));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,neutral16));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,landE17));
        Assert.IsTrue(shipBat.SetTarget(EPhase.CombatMove,enemy18));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,landE19));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,neutral20));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,landE21));
        Assert.IsFalse(shipBat.SetTarget(EPhase.CombatMove,transport22));
        Assert.IsTrue(shipBat.SetTarget(EPhase.CombatMove,landE23));
        
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,friendly2));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,friendly3));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,friendly4));
        Assert.IsTrue(shipAir.SetTarget(EPhase.CombatMove,enemy5));
        Assert.IsTrue(shipAir.SetTarget(EPhase.CombatMove,enemy6));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,enemy7));
        Assert.IsTrue(shipAir.SetTarget(EPhase.CombatMove,enemy8));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,friendly9));
        Assert.IsTrue(shipAir.SetTarget(EPhase.CombatMove,destroyer10));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,friendly11));
        Assert.IsTrue(shipAir.SetTarget(EPhase.CombatMove,destroyer12));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,enemy13));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,neutral14));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,landF15));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,neutral16));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,landE17));
        Assert.IsTrue(shipAir.SetTarget(EPhase.CombatMove,enemy18));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,landE19));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,neutral20));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,landE21));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,transport22));
        Assert.IsFalse(shipAir.SetTarget(EPhase.CombatMove,landE23));
        
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,friendly2));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,friendly3));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,friendly4));
        Assert.IsTrue(shipTra.SetTarget(EPhase.CombatMove,enemy5));
        Assert.IsTrue(shipTra.SetTarget(EPhase.CombatMove,enemy6));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,enemy7));
        Assert.IsTrue(shipTra.SetTarget(EPhase.CombatMove,enemy8));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,friendly9));
        Assert.IsTrue(shipTra.SetTarget(EPhase.CombatMove,destroyer10));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,friendly11));
        Assert.IsTrue(shipTra.SetTarget(EPhase.CombatMove,destroyer12));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,enemy13));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,neutral14));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,landF15));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,neutral16));
        Assert.IsTrue(shipTra.SetTarget(EPhase.CombatMove,landE17));
        Assert.IsTrue(shipTra.SetTarget(EPhase.CombatMove,enemy18));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,landE19));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,neutral20));
        Assert.IsTrue(shipTra.SetTarget(EPhase.CombatMove,landE21));
        Assert.IsFalse(shipTra.SetTarget(EPhase.CombatMove,transport22));
        Assert.IsTrue(shipTra.SetTarget(EPhase.CombatMove,landE23));
        Assert.IsTrue(transport2.SetTarget(EPhase.CombatMove,landE21));
    }

    [Test]
    public void CanalMovementTest(){
        Nation self = new Nation(){
            Name = "self"
        };
        Nation friendly = new Nation(){
            Name = "friendly"
        };
        Nation enemy = new Nation(){
            Name = "enemy"
        };

        self.Allies = new List<Allies>(){
            new Allies(){
                Nation = self,
                Ally = friendly
            }
        };
        friendly.Allies = new List<Allies>(){
            new Allies(){
                Nation = friendly,
                Ally = self
            }
        };

        WaterRegion water1 = new WaterRegion(){
            Name = "water1"
        };
        WaterRegion water2 = new WaterRegion(){
            Name = "water2"
        };
        WaterRegion water3 = new WaterRegion(){
            Name = "water3"
        };

        LandRegion canalOwner1 = new LandRegion(){
            Name = "canalOwner1",
            Nation = self
        };
        LandRegion canalOwner2 = new LandRegion(){
            Name = "canalOwner2",
            Nation = friendly
        };
        LandRegion canalOwner3 = new LandRegion(){
            Name = "canalOwner3",
            Nation = enemy
        };
        
        Neighbours n1 = new Neighbours(){
            Region = water1,
            Neighbour = water2
        };
        n1.CanalOwners.AddRange(new List<CanalOwners>(){
            new CanalOwners(){
                CanalOwner = canalOwner1,
                Neighbours = n1
            },
            new CanalOwners(){
                CanalOwner = canalOwner2,
                Neighbours = n1
            },
        });
        
        Neighbours n2 = new Neighbours(){
            Region = water1,
            Neighbour = water3
        };
        n2.CanalOwners.AddRange(new List<CanalOwners>(){
            new CanalOwners(){
                CanalOwner = canalOwner1,
                Neighbours = n2
            },
            new CanalOwners(){
                CanalOwner = canalOwner3,
                Neighbours = n2
            },
        });
        
        Neighbours n3 = new Neighbours(){
            Region = water2,
            Neighbour = water1
        };
        n3.CanalOwners.AddRange(new List<CanalOwners>(){
            new CanalOwners(){
                CanalOwner = canalOwner1,
                Neighbours = n3
            },
            new CanalOwners(){
                CanalOwner = canalOwner2,
                Neighbours = n3
            },
        });
        
        Neighbours n4 = new Neighbours(){
            Region = water3,
            Neighbour = water1
        };
        n4.CanalOwners.AddRange(new List<CanalOwners>(){
            new CanalOwners(){
                CanalOwner = canalOwner1,
                Neighbours = n4
            },
            new CanalOwners(){
                CanalOwner = canalOwner3,
                Neighbours = n4
            },
        });
        
        water1.Neighbours.Add(n1);
        water1.Neighbours.Add(n2);
        water2.Neighbours.Add(n3);
        water3.Neighbours.Add(n4);

        AShip ship1 = ShipFactory.CreateSubmarine(water1, self);
        AShip ship2 = ShipFactory.CreateDestroyer(water1, self);
        AShip ship3 = ShipFactory.CreateCruiser(water1, self);
        AShip ship4 = ShipFactory.CreateBattleship(water1, self);
        AShip ship5 = ShipFactory.CreateAircraftCarrier(water1, self);
        AShip ship6 = ShipFactory.CreateTransport(water1, self);
        water1.StationedShips.AddRange(new List<AShip>(){
            ship1,
            ship2,
            ship3,
            ship4,
            ship5,
            ship6
        });
        
        Assert.IsTrue(ship1.SetTarget(EPhase.NonCombatMove,water2));
        Assert.IsFalse(ship1.SetTarget(EPhase.NonCombatMove,water3));
        
        Assert.IsTrue(ship2.SetTarget(EPhase.NonCombatMove,water2));
        Assert.IsFalse(ship2.SetTarget(EPhase.NonCombatMove,water3));
        
        Assert.IsTrue(ship3.SetTarget(EPhase.NonCombatMove,water2));
        Assert.IsFalse(ship3.SetTarget(EPhase.NonCombatMove,water3));
        
        Assert.IsTrue(ship4.SetTarget(EPhase.NonCombatMove,water2));
        Assert.IsFalse(ship4.SetTarget(EPhase.NonCombatMove,water3));
        
        Assert.IsTrue(ship5.SetTarget(EPhase.NonCombatMove,water2));
        Assert.IsFalse(ship5.SetTarget(EPhase.NonCombatMove,water3));
        
        Assert.IsTrue(ship6.SetTarget(EPhase.NonCombatMove,water2));
        Assert.IsFalse(ship6.SetTarget(EPhase.NonCombatMove,water3));
    }

    [Test]
    public void GetUnitTests(){
        Nation nation = new Nation(){
            Name = "nation"
        };
        
        LandRegion region = new LandRegion(){
            Name = "region"
        };

        List<ALandUnit> units = new List<ALandUnit>(){
            LandUnitFactory.CreateInfantry(region, nation),
            LandUnitFactory.CreateTank(region, nation),
            LandUnitFactory.CreateTank(region, nation),
            LandUnitFactory.CreateArtillery(region, nation),
            LandUnitFactory.CreateArtillery(region, nation),
            LandUnitFactory.CreateArtillery(region, nation)
        };

        region.StationedUnits.AddRange(units);

        ALandUnit unit = LandUnitFactory.CreateTank(region, nation);

        Assert.AreEqual( 3,region.GetOneStationedUnitPerType().Count);
        Assert.IsTrue(region.GetOneStationedUnitPerType().Contains(units[0]));
        Assert.IsTrue(region.GetOneStationedUnitPerType().Contains(units[1]));
        Assert.IsTrue(region.GetOneStationedUnitPerType().Contains(units[3]));
        
        Assert.AreEqual(3,region.GetStationedUnitCounts().Count);
        Assert.IsTrue(region.GetStationedUnitCounts().Keys.Contains(units[0]));
        Assert.IsTrue(region.GetStationedUnitCounts().Keys.Contains(units[1]));
        Assert.IsTrue(region.GetStationedUnitCounts().Keys.Contains(units[3]));
        
        Assert.AreEqual(1, region.GetUnitCount(units[0]));
        Assert.AreEqual(2, region.GetUnitCount(units[1]));
        Assert.AreEqual(2, region.GetUnitCount(units[2]));
        Assert.AreEqual(3, region.GetUnitCount(units[3]));
        Assert.AreEqual(3, region.GetUnitCount(units[4]));
        Assert.AreEqual(3, region.GetUnitCount(units[5]));
    }*/
    
    [Test]
    public void Test1(){
        string asd = JsonSerializer.Serialize(new StateHasChangedEvent());
        Assert.AreEqual("a",asd);
    }

    [Test]
    public void Test2(){
        string asd = JsonSerializer.Serialize(new ReadyEvent());
        Assert.AreEqual("a",asd);
    }
}