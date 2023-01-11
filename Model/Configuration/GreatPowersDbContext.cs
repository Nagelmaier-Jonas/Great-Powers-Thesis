using System.Drawing;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;
using Model.Factories;

namespace Model.Configuration;

public sealed class GreatPowersDbContext : IdentityDbContext<User>{
    public GreatPowersDbContext(DbContextOptions options) : base(options){
        Database.SetConnectionString(
            $"server={AppSettings.IpAddress}; port={AppSettings.Port}; database=greatpowers; user=greatpowers; password=greatpowers; Persist Security Info=False; Connect Timeout=300");
    }

    public DbSet<User> User{ get; set; }
    public DbSet<Nation> Nations{ get; set; }
    public DbSet<ARegion> Regions{ get; set; }
    public DbSet<Neighbours> Neighbours{ get; set; }
    public DbSet<CanalOwners> CanalOwners{ get; set; }
    public DbSet<WaterRegion> WaterRegions{ get; set; }
    public DbSet<LandRegion> LandRegions{ get; set; }
    public DbSet<Factory> Factories{ get; set; }
    public DbSet<AUnit> Units{ get; set; }
    public DbSet<ALandUnit> LandUnits{ get; set; }
    public DbSet<Infantry> Infantry{ get; set; }
    public DbSet<Tank> Tanks{ get; set; }
    public DbSet<Artillery> Artillery{ get; set; }
    public DbSet<AntiAir> AntiAir{ get; set; }
    public DbSet<AShip> Ships{ get; set; }
    public DbSet<Submarine> Submarines{ get; set; }
    public DbSet<Destroyer> Destroyers{ get; set; }
    public DbSet<Cruiser> Cruisers{ get; set; }
    public DbSet<Battleship> Battleships{ get; set; }
    public DbSet<AircraftCarrier> AircraftCarriers{ get; set; }
    public DbSet<Transport> Transports{ get; set; }
    public DbSet<APlane> Planes{ get; set; }
    public DbSet<Fighter> Fighters{ get; set; }
    public DbSet<Bomber> Bombers{ get; set; }
    public DbSet<SessionInfo> Session{ get; set; }
    public DbSet<Allies> Allies{ get; set; }
    public DbSet<Capital> Capitals{ get; set; }

    protected override void OnModelCreating(ModelBuilder builder){
        builder.UseCollation("utf8_general_ci").HasCharSet("utf8");

        builder.Entity<User>().HasIndex(u => new{ u.UserName, u.Email }).IsUnique();

        builder.Entity<Nation>().HasOne(n => n.User)
            .WithMany(u => u.Nations);

        builder.Entity<Allies>().HasOne(n => n.Nation)
            .WithMany(r => r.Allies);
        builder.Entity<Allies>().HasOne(n => n.Ally)
            .WithMany();
        builder.Entity<Allies>().HasKey(n => new{ n.NationId, n.AllyId });

        builder.Entity<Capital>().HasIndex(u => u.Name).IsUnique();
        builder.Entity<Capital>().HasOne(f => f.Region).WithOne(r => r.Capital);

        builder.Entity<LandRegion>().HasOne(n => n.Nation)
            .WithMany(u => u.Regions);

        builder.Entity<Factory>().HasOne(f => f.Region).WithOne(r => r.Factory);

        builder.Entity<Neighbours>().HasOne(n => n.Region)
            .WithMany(r => r.Neighbours);
        builder.Entity<Neighbours>().HasOne(n => n.Neighbour)
            .WithMany();
        builder.Entity<Neighbours>().HasKey(n => new{ n.NeighbourId, n.RegionId });

        builder.Entity<CanalOwners>().HasOne(c => c.CanalOwner)
            .WithMany(r => r.Canals);
        builder.Entity<CanalOwners>().HasOne(c => c.Neighbours)
            .WithMany(n => n.CanalOwners);
        builder.Entity<CanalOwners>().HasKey(c => new{ c.NeighboursNeighbourId, c.NeighboursRegionId, c.CanalOwnerId });

        builder.Entity<AUnit>().HasOne(u => u.Nation).WithMany(n => n.Units);
        builder.Entity<AUnit>().HasOne(n => n.Target)
            .WithMany(u => u.IncomingUnits);

        builder.Entity<ALandUnit>().HasOne(n => n.Transport)
            .WithMany(u => u.Units);
        builder.Entity<ALandUnit>().HasOne(n => n.Region)
            .WithMany(u => u.StationedUnits);

        builder.Entity<APlane>().HasOne(n => n.AircraftCarrier)
            .WithMany(u => u.Planes);
        builder.Entity<APlane>().HasOne(n => n.Region)
            .WithMany(u => u.StationedPlanes);

        builder.Entity<AShip>().HasOne(s => s.Region)
            .WithMany(w => w.StationedShips);

        builder.Entity<SessionInfo>().HasOne(s => s.Nation).WithOne()
            .HasForeignKey<SessionInfo>(s => s.CurrentNationId);

        #region Nations

        Nation Germany = new Nation(){
            Id = 1,
            Name = "Germany",
            Color = "#8ea39e",
            Treasury = 41,
            Type = ENation.Germany
        };
        Nation Japan = new Nation(){
            Id = 2,
            Name = "Japan",
            Color = "#d29151",
            Treasury = 30,
            Type = ENation.Japan
        };
        Nation Soviet_Union = new Nation(){
            Id = 3,
            Name = "Soviet_Union",
            Color = "#ba8772",
            Treasury = 24,
            Type = ENation.SovietUnion
        };
        Nation United_States = new Nation(){
            Id = 4,
            Name = "United_States",
            Color = "#97a95f",
            Treasury = 42,
            Type = ENation.UnitedStates
        };
        Nation United_Kingdom = new Nation(){
            Id = 5,
            Name = "United_Kingdom",
            Color = "#b59b68",
            Treasury = 31,
            Type = ENation.UnitedKingdom
        };
        Nation Neutral = new Nation(){
            Id = 6,
            Name = "Neutral",
            Color = "#4f4d49",
            Treasury = 24,
            Type = ENation.Neutral
        };

        builder.Entity<Nation>().HasData(new List<Nation>(){
            Germany,
            Japan,
            Soviet_Union,
            United_States,
            United_Kingdom,
            Neutral
        });

        #endregion

        builder.Entity<Allies>().HasData(new List<Allies>(){
            new Allies(){
                NationId = 1,
                AllyId = 2
            },
            new Allies(){
                NationId = 2,
                AllyId = 1
            },
            new Allies(){
                NationId = 3,
                AllyId = 4
            },
            new Allies(){
                NationId = 3,
                AllyId = 5
            },
            new Allies(){
                NationId = 4,
                AllyId = 3
            },
            new Allies(){
                NationId = 4,
                AllyId = 5
            },
            new Allies(){
                NationId = 5,
                AllyId = 4
            },
            new Allies(){
                NationId = 5,
                AllyId = 3
            }
        });

        builder.Entity<Factory>().HasData(new List<Factory>(){
            new Factory(){
                Id = 229,
                Damage = 0,
                RegionId = 66
            },
            new Factory(){
                Id = 230,
                Damage = 0,
                RegionId = 74
            },
            new Factory(){
                Id = 231,
                Damage = 0,
                RegionId = 77
            },
            new Factory(){
                Id = 232,
                Damage = 0,
                RegionId = 84
            },
            new Factory(){
                Id = 233,
                Damage = 0,
                RegionId = 110
            },
            new Factory(){
                Id = 234,
                Damage = 0,
                RegionId = 115
            },
            new Factory(){
                Id = 235,
                Damage = 0,
                RegionId = 117
            },
            new Factory(){
                Id = 236,
                Damage = 0,
                RegionId = 86
            },
            new Factory(){
                Id = 237,
                Damage = 0,
                RegionId = 96
            },
            new Factory(){
                Id = 238,
                Damage = 0,
                RegionId = 130
            }
        });

        builder.Entity<Capital>().HasData(new List<Capital>(){
            new Capital(){
                Id = 1,
                Name = "Berlin",
                RegionId = 66
            },
            new Capital(){
                Id = 2,
                Name = "Paris",
                RegionId = 78
            },
            new Capital(){
                Id = 3,
                Name = "Rom",
                RegionId = 77
            },
            new Capital(){
                Id = 4,
                Name = "Leningrad",
                RegionId = 84
            },
            new Capital(){
                Id = 5,
                Name = "Moskau",
                RegionId = 86
            },
            new Capital(){
                Id = 6,
                Name = "London",
                RegionId = 110
            },
            new Capital(){
                Id = 7,
                Name = "Kalkutta",
                RegionId = 96
            },
            new Capital(){
                Id = 8,
                Name = "Shanghai",
                RegionId = 132
            },
            new Capital(){
                Id = 9,
                Name = "Tokyo",
                RegionId = 130
            },
            new Capital(){
                Id = 10,
                Name = "Manila",
                RegionId = 140
            },
            new Capital(){
                Id = 11,
                Name = "San Francisco",
                RegionId = 115
            },
            new Capital(){
                Id = 12,
                Name = "Washington",
                RegionId = 117
            },
            new Capital(){
                Id = 13,
                Name = "Honululu",
                RegionId = 125
            }
        });

        #region Deutschland

        LandRegion deutschland = new LandRegion(){
            Id = 66,
            Income = 10,
            Name = "Deutschland",
            NationId = 1,
            Identifier = ERegion.Germany,
            PositionX = 606,
            PositionY = 313
        };
        LandRegion polen = new LandRegion(){
            Id = 67,
            Income = 2,
            Name = "Polen",
            NationId = 1,
            Identifier = ERegion.Poland,
            PositionX = 694,
            PositionY = 291
        };
        LandRegion baltische_staaten = new LandRegion(){
            Id = 68,
            Income = 2,
            Name = "Baltische Staaten",
            NationId = 1,
            Identifier = ERegion.BalticStates,
            PositionX = 686,
            PositionY = 210
        };
        LandRegion weissrussland = new LandRegion(){
            Id = 69,
            Income = 2,
            Name = "Weissrussland",
            NationId = 1,
            Identifier = ERegion.WhiteRussia,
            PositionX = 745,
            PositionY = 224
        };
        LandRegion ukrainischessr = new LandRegion(){
            Id = 70,
            Income = 2,
            Name = "Ukrainische SSR",
            NationId = 1,
            Identifier = ERegion.Ukraine,
            PositionX = 769,
            PositionY = 342
        };
        LandRegion westrussland = new LandRegion(){
            Id = 71,
            Income = 2,
            Name = "West Russland",
            NationId = 1,
            Identifier = ERegion.WestRussia,
            PositionX = 811,
            PositionY = 273
        };
        LandRegion finnland = new LandRegion(){
            Id = 72,
            Income = 1,
            Name = "Finnland",
            NationId = 1,
            Identifier = ERegion.Finland,
            PositionX = 700,
            PositionY = 100
        };
        LandRegion norwegen = new LandRegion(){
            Id = 73,
            Income = 2,
            Name = "Norwegen",
            NationId = 1,
            Identifier = ERegion.Norway,
            PositionX = 592,
            PositionY = 130
        };
        LandRegion bulgarien_rumänien = new LandRegion(){
            Id = 75,
            Income = 2,
            Name = "Bulgarien Rumänien",
            NationId = 1,
            Identifier = ERegion.BulgariaRomania,
            PositionX = 697,
            PositionY = 407
        };
        LandRegion südeuropa = new LandRegion(){
            Id = 76,
            Income = 2,
            Name = "Südeuropa",
            NationId = 1,
            Identifier = ERegion.SouthEurope,
            PositionX = 657,
            PositionY = 427
        };
        LandRegion italien = new LandRegion(){
            Id = 77,
            Income = 3,
            Name = "Italien",
            NationId = 1,
            Identifier = ERegion.Italy,
            PositionX = 578,
            PositionY = 415
        };
        LandRegion frankreich = new LandRegion(){
            Id = 78,
            Income = 6,
            Name = "Frankreich",
            NationId = 1,
            Identifier = ERegion.France,
            PositionX = 506,
            PositionY = 367
        };
        LandRegion nordwesteuropa = new LandRegion(){
            Id = 79,
            Income = 2,
            Name = "Nordwesteuropa",
            NationId = 1,
            Identifier = ERegion.NorthWestEurope,
            PositionX = 543,
            PositionY = 293
        };
        LandRegion marokko = new LandRegion(){
            Id = 80,
            Income = 1,
            Name = "Marokko",
            NationId = 1,
            Identifier = ERegion.Morocco,
            PositionX = 460,
            PositionY = 552
        };
        LandRegion algerien = new LandRegion(){
            Id = 81,
            Income = 1,
            Name = "Algerien",
            NationId = 1,
            Identifier = ERegion.Algeria,
            PositionX = 535,
            PositionY = 540
        };
        LandRegion lybien = new LandRegion(){
            Id = 82,
            Income = 1,
            Name = "Libyen",
            NationId = 1,
            Identifier = ERegion.Libya,
            PositionX = 636,
            PositionY = 606
        };

        #endregion

        #region Russland

        LandRegion kaukasus = new LandRegion(){
            Id = 74,
            Income = 4,
            Name = "Kaukasus",
            NationId = 3,
            Identifier = ERegion.Caucasus,
            PositionX = 854,
            PositionY = 416
        };
        LandRegion karelo_finnnischessr = new LandRegion(){
            Id = 84,
            Income = 2,
            Name = "Karelo-Finnische SSR",
            NationId = 3,
            Identifier = ERegion.Karelia,
            PositionX = 765,
            PositionY = 125
        };
        LandRegion archangelsk = new LandRegion(){
            Id = 85,
            Income = 1,
            Name = "Archangelsk",
            NationId = 3,
            Identifier = ERegion.Archangelsk,
            PositionX = 905,
            PositionY = 100
        };
        LandRegion russland = new LandRegion(){
            Id = 86,
            Income = 8,
            Name = "Russland",
            NationId = 3,
            Identifier = ERegion.Russia,
            PositionX = 888,
            PositionY = 257
        };
        LandRegion autonomer_kreis_der_ewenken = new LandRegion(){
            Id = 87,
            Income = 1,
            Name = "Autonomer Kreis der Ewenken",
            NationId = 3,
            Identifier = ERegion.EwenkiAutonomousDistrict,
            PositionX = 1122,
            PositionY = 87
        };
        LandRegion wologda = new LandRegion(){
            Id = 88,
            Income = 2,
            Name = "Wologda",
            NationId = 3,
            Identifier = ERegion.Vologda,
            PositionX = 991,
            PositionY = 161
        };
        LandRegion nowosibirsk = new LandRegion(){
            Id = 89,
            Income = 1,
            Name = "Nowosibirsk",
            NationId = 3,
            Identifier = ERegion.Novosibirsk,
            PositionX = 1000,
            PositionY = 288
        };
        LandRegion kasachischessr = new LandRegion(){
            Id = 90,
            Income = 2,
            Name = "Kasachische SSR",
            NationId = 3,
            Identifier = ERegion.Kazakhstan,
            PositionX = 945,
            PositionY = 450
        };
        LandRegion jakutischessr = new LandRegion(){
            Id = 91,
            Income = 1,
            Name = "Jakutische SSR",
            NationId = 3,
            Identifier = ERegion.Yakutia,
            PositionX = 1286,
            PositionY = 48
        };
        LandRegion burjatischessr = new LandRegion(){
            Id = 92,
            Income = 1,
            Name = "Burjatische SSR",
            NationId = 3,
            Identifier = ERegion.Buryatia,
            PositionX = 1362,
            PositionY = 185
        };
        LandRegion sowjetischer_ferner_osten = new LandRegion(){
            Id = 93,
            Income = 1,
            Name = "Sowjetischer Ferner Osten",
            NationId = 3,
            Identifier = ERegion.SovietFarEast,
            PositionX = 1490,
            PositionY = 57
        };

        #endregion

        #region Großbritannien

        LandRegion ägypten = new LandRegion(){
            Id = 83,
            Income = 2,
            Name = "Ägypten",
            NationId = 5,
            Identifier = ERegion.Egypt,
            PositionX = 722,
            PositionY = 631
        };
        LandRegion transjordanien = new LandRegion(){
            Id = 94,
            Income = 1,
            Name = "Transjordanien",
            NationId = 5,
            Identifier = ERegion.Transjordan,
            PositionX = 823,
            PositionY = 555
        };
        LandRegion persien = new LandRegion(){
            Id = 95,
            Income = 1,
            Name = "Persien",
            NationId = 5,
            Identifier = ERegion.Persia,
            PositionX = 910,
            PositionY = 571
        };
        LandRegion indien = new LandRegion(){
            Id = 96,
            Income = 3,
            Name = "Indien",
            NationId = 5,
            Identifier = ERegion.India,
            PositionX = 1017,
            PositionY = 681
        };
        LandRegion burma = new LandRegion(){
            Id = 97,
            Income = 1,
            Name = "Burma",
            NationId = 5,
            Identifier = ERegion.Burma,
            PositionX = 1131,
            PositionY = 685
        };
        LandRegion westaustralien = new LandRegion(){
            Id = 98,
            Income = 1,
            Name = "Westaustralien",
            NationId = 5,
            Identifier = ERegion.WestAustralia,
            PositionX = 1373,
            PositionY = 966
        };
        LandRegion ostaustralien = new LandRegion(){
            Id = 99,
            Income = 1,
            Name = "Ostaustralien",
            NationId = 5,
            Identifier = ERegion.EastAustralia,
            PositionX = 1478,
            PositionY = 953
        };
        LandRegion neuseeland = new LandRegion(){
            Id = 100,
            Income = 1,
            Name = "Neuseeland",
            NationId = 5,
            Identifier = ERegion.NewZealand,
            PositionX = 1614,
            PositionY = 1076
        };
        LandRegion französisch_madagaskar = new LandRegion(){
            Id = 101,
            Income = 1,
            Name = "Französisch Madagaskar",
            NationId = 5,
            Identifier = ERegion.FrenchMadagascar,
            PositionX = 804,
            PositionY = 962
        };
        LandRegion südafrikanische_union = new LandRegion(){
            Id = 102,
            Income = 2,
            Name = "Südafrikanische Union",
            NationId = 5,
            Identifier = ERegion.SouthAfricanUnion,
            PositionX = 679,
            PositionY = 981
        };
        LandRegion rhodesien = new LandRegion(){
            Id = 103,
            Income = 1,
            Name = "Rhodesien",
            NationId = 5,
            Identifier = ERegion.Rhodesia,
            PositionX = 749,
            PositionY = 873
        };
        LandRegion belgisch_kongo = new LandRegion(){
            Id = 104,
            Income = 1,
            Name = "Belgisch-Kongo",
            NationId = 5,
            Identifier = ERegion.BelgianCongo,
            PositionX = 675,
            PositionY = 846
        };
        LandRegion anglo_ägyptischer_sudan = new LandRegion(){
            Id = 105,
            Income = 0,
            Name = "Anglo-Ägyptischer Sudan",
            NationId = 5,
            Identifier = ERegion.AngloEgyptianSudan,
            PositionX = 710,
            PositionY = 754
        };
        LandRegion italienisch_ostafrika = new LandRegion(){
            Id = 106,
            Income = 1,
            Name = "Italienisch-Ostafrika",
            NationId = 5,
            Identifier = ERegion.ItalianEastAfrica,
            PositionX = 795,
            PositionY = 795
        };
        LandRegion französisch_äquatorialafrika = new LandRegion(){
            Id = 107,
            Income = 1,
            Name = "Französisch-Äquatorialafrika",
            NationId = 5,
            Identifier = ERegion.FrenchEquatorialAfrica,
            PositionX = 610,
            PositionY = 778
        };
        LandRegion französisch_westafrika = new LandRegion(){
            Id = 108,
            Income = 1,
            Name = "Französisch-Westafrika",
            NationId = 5,
            Identifier = ERegion.FrenchWestAfrica,
            PositionX = 479,
            PositionY = 734
        };
        LandRegion gibraltar = new LandRegion(){
            Id = 109,
            Income = 0,
            Name = "Gibraltar",
            NationId = 5,
            Identifier = ERegion.Gibraltar,
            PositionX = 453,
            PositionY = 497
        };
        LandRegion vereinigtes_königreich = new LandRegion(){
            Id = 110,
            Income = 8,
            Name = "Vereinigtes Königreich",
            NationId = 5,
            Identifier = ERegion.UnitedKingdom,
            PositionX = 494,
            PositionY = 269
        };
        LandRegion island = new LandRegion(){
            Id = 111,
            Income = 0,
            Name = "Island",
            NationId = 5,
            Identifier = ERegion.Iceland,
            PositionX = 487,
            PositionY = 22
        };
        LandRegion ostkanada = new LandRegion(){
            Id = 112,
            Income = 3,
            Name = "Ostkanada",
            NationId = 5,
            Identifier = ERegion.EastCanada,
            PositionX = 67,
            PositionY = 143
        };
        LandRegion westkanada = new LandRegion(){
            Id = 113,
            Income = 1,
            Name = "Westkanada",
            NationId = 5,
            Identifier = ERegion.WestCanada,
            PositionX = 1856,
            PositionY = 145
        };

        #endregion

        #region USA

        LandRegion alaska = new LandRegion(){
            Id = 114,
            Income = 2,
            Name = "Alaska",
            NationId = 4,
            Identifier = ERegion.Alaska,
            PositionX = 1725,
            PositionY = 85
        };
        LandRegion westliche_vereinigte_staaten = new LandRegion(){
            Id = 115,
            Income = 10,
            Name = "Westliche Vereinigte Staaten",
            NationId = 4,
            Identifier = ERegion.WesternUnitedStates,
            PositionX = 1850,
            PositionY = 357
        };
        LandRegion zentrale_vereinigte_staaten = new LandRegion(){
            Id = 116,
            Income = 6,
            Name = "Zentrale Vereinigte Staaten",
            NationId = 4,
            Identifier = ERegion.CentralUnitedStates,
            PositionX = 30,
            PositionY = 350
        };
        LandRegion östliche_vereinigte_staaten = new LandRegion(){
            Id = 117,
            Income = 12,
            Name = "Östliche Vereinigte Staaten",
            NationId = 4,
            Identifier = ERegion.EasternUnitedStates,
            PositionX = 125,
            PositionY = 370
        };
        LandRegion westindien = new LandRegion(){
            Id = 118,
            Income = 1,
            Name = "Westindien",
            NationId = 4,
            Identifier = ERegion.WestIndia,
            PositionX = 176,
            PositionY = 524
        };
        LandRegion ostmexiko = new LandRegion(){
            Id = 119,
            Income = 0,
            Name = "Ostmexiko",
            NationId = 4,
            Identifier = ERegion.EastMexico,
            PositionX = 30,
            PositionY = 490
        };
        LandRegion zentralamerika = new LandRegion(){
            Id = 120,
            Income = 1,
            Name = "Zentralamerika",
            NationId = 4,
            Identifier = ERegion.CentralAmerica,
            PositionX = 95,
            PositionY = 600
        };
        LandRegion mexiko = new LandRegion(){
            Id = 121,
            Income = 2,
            Name = "Mexiko",
            NationId = 4,
            Identifier = ERegion.Mexico,
            PositionX = 1885,
            PositionY = 457
        };
        LandRegion brasilien = new LandRegion(){
            Id = 122,
            Income = 3,
            Name = "Brasilien",
            NationId = 4,
            Identifier = ERegion.Brazil,
            PositionX = 205,
            PositionY = 780
        };
        LandRegion grönland = new LandRegion(){
            Id = 123,
            Income = 0,
            Name = "Grönland",
            NationId = 4,
            Identifier = ERegion.Greenland,
            PositionX = 242,
            PositionY = 10
        };
        LandRegion midway_atoll = new LandRegion(){
            Id = 124,
            Income = 0,
            Name = "Midway-Atoll",
            NationId = 4,
            Identifier = ERegion.MidwayAtoll,
            PositionX = 1658,
            PositionY = 439
        };
        LandRegion hawaii_inseln = new LandRegion(){
            Id = 125,
            Income = 1,
            Name = "Hawaii-Inseln",
            NationId = 4,
            Identifier = ERegion.HawaiiIslands,
            PositionX = 1674,
            PositionY = 556
        };
        LandRegion sinkiang = new LandRegion(){
            Id = 126,
            Income = 1,
            Name = "Sinkiang",
            NationId = 4,
            Identifier = ERegion.Sinkiang,
            PositionX = 1085,
            PositionY = 396
        };
        LandRegion anhwei = new LandRegion(){
            Id = 127,
            Income = 1,
            Name = "Anhwei",
            NationId = 4,
            Identifier = ERegion.Anhwei,
            PositionX = 1212,
            PositionY = 440
        };
        LandRegion sezuan = new LandRegion(){
            Id = 128,
            Income = 1,
            Name = "Sezuan",
            NationId = 4,
            Identifier = ERegion.Sezuan,
            PositionX = 1116,
            PositionY = 530
        };
        LandRegion yunnan = new LandRegion(){
            Id = 129,
            Income = 1,
            Name = "Yunnan",
            NationId = 4,
            Identifier = ERegion.Yunnan,
            PositionX = 1182,
            PositionY = 618
        };

        #endregion

        #region Japan

        LandRegion japan = new LandRegion(){
            Id = 130,
            Income = 8,
            Name = "Japan",
            NationId = 2,
            Identifier = ERegion.Japan,
            PositionX = 1453,
            PositionY = 380
        };
        LandRegion mandschurei = new LandRegion(){
            Id = 131,
            Income = 3,
            Name = "Mandschurei",
            NationId = 2,
            Identifier = ERegion.Mandschurei,
            PositionX = 1297,
            PositionY = 285
        };
        LandRegion jiangsu = new LandRegion(){
            Id = 132,
            Income = 2,
            Name = "Jiangsu",
            NationId = 2,
            Identifier = ERegion.Jiangsu,
            PositionX = 1288,
            PositionY = 470
        };
        LandRegion guandong = new LandRegion(){
            Id = 133,
            Income = 2,
            Name = "Guandong",
            NationId = 2,
            Identifier = ERegion.Guandong,
            PositionX = 1241,
            PositionY = 588
        };
        LandRegion französisch_indochina_thailand = new LandRegion(){
            Id = 134,
            Income = 2,
            Name = "Französisch-Indochina-Thailand",
            NationId = 2,
            Identifier = ERegion.FrenchIndochinaThailand,
            PositionX = 1191,
            PositionY = 749
        };
        LandRegion malaysia = new LandRegion(){
            Id = 135,
            Income = 1,
            Name = "Malaysia",
            NationId = 2,
            Identifier = ERegion.Malaysia,
            PositionX = 1177,
            PositionY = 850
        };
        LandRegion borneo = new LandRegion(){
            Id = 136,
            Income = 4,
            Name = "Borneo",
            NationId = 2,
            Identifier = ERegion.Borneo,
            PositionX = 1269,
            PositionY = 878
        };
        LandRegion ostindien = new LandRegion(){
            Id = 137,
            Income = 4,
            Name = "Ostindien",
            NationId = 2,
            Identifier = ERegion.EastIndia,
            PositionX = 1190,
            PositionY = 929
        };
        LandRegion salomon_inseln = new LandRegion(){
            Id = 138,
            Income = 0,
            Name = "Salomon-Inseln",
            NationId = 2,
            Identifier = ERegion.SolomonIslands,
            PositionX = 1593,
            PositionY = 878
        };
        LandRegion neuguniea = new LandRegion(){
            Id = 139,
            Income = 1,
            Name = "Neuguniea",
            NationId = 2,
            Identifier = ERegion.NewGuinea,
            PositionX = 1462,
            PositionY = 839
        };
        LandRegion philippinische_inseln = new LandRegion(){
            Id = 140,
            Income = 3,
            Name = "Philippinische Inseln",
            NationId = 2,
            Identifier = ERegion.PhilippineIslands,
            PositionX = 1354,
            PositionY = 700
        };
        LandRegion formosa = new LandRegion(){
            Id = 141,
            Income = 0,
            Name = "Formosa",
            NationId = 2,
            Identifier = ERegion.Formosa,
            PositionX = 1293,
            PositionY = 583
        };
        LandRegion okinawa = new LandRegion(){
            Id = 142,
            Income = 0,
            Name = "Okinawa",
            NationId = 2,
            Identifier = ERegion.Okinawa,
            PositionX = 1429,
            PositionY = 525
        };
        LandRegion iwojima = new LandRegion(){
            Id = 143,
            Income = 0,
            Name = "Iwojima",
            NationId = 2,
            Identifier = ERegion.Iwojima,
            PositionX = 1521,
            PositionY = 459
        };
        LandRegion wake = new LandRegion(){
            Id = 144,
            Income = 0,
            Name = "Wake",
            NationId = 2,
            Identifier = ERegion.Wake,
            PositionX = 1567,
            PositionY = 566
        };
        LandRegion caroline_atoll = new LandRegion(){
            Id = 145,
            Income = 0,
            Name = "Caroline-Atoll",
            NationId = 2,
            Identifier = ERegion.CarolineAtoll,
            PositionX = 1489,
            PositionY = 657
        };

        #endregion

        builder.Entity<LandRegion>().HasData(new List<LandRegion>(){
            #region Deutschland

            deutschland,
            polen,
            baltische_staaten,
            weissrussland,
            ukrainischessr,
            westrussland,
            finnland,
            norwegen,
            bulgarien_rumänien,
            südeuropa,
            italien,
            frankreich,
            nordwesteuropa,
            marokko,
            algerien,
            lybien,

            #endregion

            #region Russland

            kaukasus,
            karelo_finnnischessr,
            archangelsk,
            russland,
            autonomer_kreis_der_ewenken,
            wologda,
            nowosibirsk,
            kasachischessr,
            jakutischessr,
            burjatischessr,
            sowjetischer_ferner_osten,

            #endregion

            #region Großbritannien

            ägypten,
            transjordanien,
            persien,
            indien,
            burma,
            westaustralien,
            ostaustralien,
            neuseeland,
            französisch_madagaskar,
            südafrikanische_union,
            rhodesien,
            belgisch_kongo,
            anglo_ägyptischer_sudan,
            italienisch_ostafrika,
            französisch_äquatorialafrika,
            französisch_westafrika,
            gibraltar,
            vereinigtes_königreich,
            island,
            ostkanada,
            westkanada,

            #endregion

            #region USA

            alaska,
            westliche_vereinigte_staaten,
            zentrale_vereinigte_staaten,
            östliche_vereinigte_staaten,
            westindien,
            ostmexiko,
            zentralamerika,
            mexiko,
            brasilien,
            grönland,
            midway_atoll,
            hawaii_inseln,
            sinkiang,
            anhwei,
            sezuan,
            yunnan,

            #endregion

            #region Japan

            japan,
            mandschurei,
            jiangsu,
            guandong,
            französisch_indochina_thailand,
            malaysia,
            borneo,
            ostindien,
            salomon_inseln,
            neuguniea,
            philippinische_inseln,
            formosa,
            okinawa,
            iwojima,
            wake,
            caroline_atoll,

            #endregion

            #region Neutral

            new LandRegion(){
                Id = 146,
                Income = 0,
                Name = "Mogolei",
                NationId = 6,
                Identifier = ERegion.Mongolia,
                PositionX = 1180,
                PositionY = 259
            },
            new LandRegion(){
                Id = 147,
                Income = 0,
                Name = "Himalaya",
                NationId = 6,
                Identifier = ERegion.Himalaya,
                PositionX = 1054,
                PositionY = 602
            },
            new LandRegion(){
                Id = 148,
                Income = 0,
                Name = "Afghanistan",
                NationId = 6,
                Identifier = ERegion.Afghanistan,
                PositionX = 978,
                PositionY = 559
            },
            new LandRegion(){
                Id = 149,
                Income = 0,
                Name = "Saudi Arabien",
                NationId = 6,
                Identifier = ERegion.SaudiArabia,
                PositionX = 853,
                PositionY = 686
            },
            new LandRegion(){
                Id = 150,
                Income = 0,
                Name = "Türkei",
                NationId = 6,
                Identifier = ERegion.Turkey,
                PositionX = 778,
                PositionY = 503
            },
            new LandRegion(){
                Id = 151,
                Income = 0,
                Name = "Schweden",
                NationId = 6,
                Identifier = ERegion.Sweden,
                PositionX = 620,
                PositionY = 162
            },
            new LandRegion(){
                Id = 152,
                Income = 0,
                Name = "Irland",
                NationId = 6,
                Identifier = ERegion.Ireland,
                PositionX = 449,
                PositionY = 254
            },
            new LandRegion(){
                Id = 153,
                Income = 0,
                Name = "Spanien Portugal",
                NationId = 6,
                Identifier = ERegion.SpainPortugal,
                PositionX = 445,
                PositionY = 470
            },
            new LandRegion(){
                Id = 154,
                Income = 0,
                Name = "Sahara",
                NationId = 6,
                Identifier = ERegion.Sahara,
                PositionX = 548,
                PositionY = 651
            },
            new LandRegion(){
                Id = 155,
                Income = 0,
                Name = "Angola",
                NationId = 6,
                Identifier = ERegion.Angola,
                PositionX = 644,
                PositionY = 904
            },
            new LandRegion(){
                Id = 156,
                Income = 0,
                Name = "Mosambik",
                NationId = 6,
                Identifier = ERegion.Mozambique,
                PositionX = 750,
                PositionY = 939
            },
            new LandRegion(){
                Id = 157,
                Income = 0,
                Name = "Schweiz",
                NationId = 6,
                Identifier = ERegion.Switzerland,
                PositionX = 558,
                PositionY = 367
            },
            new LandRegion(){
                Id = 158,
                Income = 0,
                Name = "Venezuela",
                NationId = 6,
                Identifier = ERegion.Venezuela,
                PositionX = 165,
                PositionY = 610
            },
            new LandRegion(){
                Id = 159,
                Income = 0,
                Name = "Kolumbien Ecuador",
                NationId = 6,
                Identifier = ERegion.ColombiaEcuador,
                PositionX = 105,
                PositionY = 650
            },
            new LandRegion(){
                Id = 160,
                Income = 0,
                Name = "Peru Argentinien",
                NationId = 6,
                Identifier = ERegion.PeruArgentina,
                PositionX = 139,
                PositionY = 823
            },
            new LandRegion(){
                Id = 161,
                Income = 0,
                Name = "Chile",
                NationId = 6,
                Identifier = ERegion.Chile,
                PositionX = 89,
                PositionY = 905
            }

            #endregion
        });

        #region WaterRegions

        WaterRegion see1 = new WaterRegion()
            { Id = 1, Name = "Seezone 1", Identifier = ERegion.SeeZone1, PositionX = 156, PositionY = 105 };
        WaterRegion see2 = new WaterRegion()
            { Id = 2, Name = "Seezone 2", Identifier = ERegion.SeeZone2, PositionX = 331, PositionY = 85 };
        WaterRegion see3 = new WaterRegion()
            { Id = 3, Name = "Seezone 3", Identifier = ERegion.SeeZone3, PositionX = 545, PositionY = 31 };
        WaterRegion see4 = new WaterRegion()
            { Id = 4, Name = "Seezone 4", Identifier = ERegion.SeeZone4, PositionX = 823, PositionY = 0 };
        WaterRegion see5 = new WaterRegion()
            { Id = 5, Name = "Seezone 5", Identifier = ERegion.SeeZone5, PositionX = 654, PositionY = 181 };
        WaterRegion see6 = new WaterRegion()
            { Id = 6, Name = "Seezone 6", Identifier = ERegion.SeeZone6, PositionX = 537, PositionY = 194 };
        WaterRegion see7 = new WaterRegion()
            { Id = 7, Name = "Seezone 7", Identifier = ERegion.SeeZone7, PositionX = 447, PositionY = 151 };
        WaterRegion see8 = new WaterRegion()
            { Id = 8, Name = "Seezone 8", Identifier = ERegion.SeeZone8, PositionX = 442, PositionY = 320 };
        WaterRegion see9 = new WaterRegion()
            { Id = 9, Name = "Seezone 9", Identifier = ERegion.SeeZone9, PositionX = 377, PositionY = 302 };
        WaterRegion see10 = new WaterRegion()
            { Id = 10, Name = "Seezone 10", Identifier = ERegion.SeeZone10, PositionX = 279, PositionY = 309 };
        WaterRegion see11 = new WaterRegion()
            { Id = 11, Name = "Seezone 11", Identifier = ERegion.SeeZone11, PositionX = 188, PositionY = 430 };
        WaterRegion see12 = new WaterRegion()
            { Id = 12, Name = "Seezone 12", Identifier = ERegion.SeeZone12, PositionX = 293, PositionY = 506 };
        WaterRegion see13 = new WaterRegion()
            { Id = 13, Name = "Seezone 13", Identifier = ERegion.SeeZone13, PositionX = 395, PositionY = 534 };
        WaterRegion see14 = new WaterRegion()
            { Id = 14, Name = "Seezone 14", Identifier = ERegion.SeeZone14, PositionX = 517, PositionY = 478 };
        WaterRegion see15 = new WaterRegion()
            { Id = 15, Name = "Seezone 15", Identifier = ERegion.SeeZone15, PositionX = 643, PositionY = 534 };
        WaterRegion see16 = new WaterRegion()
            { Id = 16, Name = "Seezone 16", Identifier = ERegion.SeeZone16, PositionX = 774, PositionY = 444 };
        WaterRegion see17 = new WaterRegion()
            { Id = 17, Name = "Seezone 17", Identifier = ERegion.SeeZone17, PositionX = 738, PositionY = 551 };
        WaterRegion see18 = new WaterRegion()
            { Id = 18, Name = "Seezone 18", Identifier = ERegion.SeeZone18, PositionX = 179, PositionY = 560 };
        WaterRegion see19 = new WaterRegion()
            { Id = 19, Name = "Seezone 19", Identifier = ERegion.SeeZone19, PositionX = 40, PositionY = 645 };
        WaterRegion see20 = new WaterRegion()
            { Id = 20, Name = "Seezone 20", Identifier = ERegion.SeeZone20, PositionX = 49, PositionY = 829 };
        WaterRegion see21 = new WaterRegion()
            { Id = 21, Name = "Seezone 21", Identifier = ERegion.SeeZone21, PositionX = 143, PositionY = 1020 };
        WaterRegion see22 = new WaterRegion()
            { Id = 22, Name = "Seezone 22", Identifier = ERegion.SeeZone22, PositionX = 277, PositionY = 649 };
        WaterRegion see23 = new WaterRegion()
            { Id = 23, Name = "Seezone 23", Identifier = ERegion.SeeZone23, PositionX = 382, PositionY = 741 };
        WaterRegion see24 = new WaterRegion()
            { Id = 24, Name = "Seezone 24", Identifier = ERegion.SeeZone24, PositionX = 565, PositionY = 867 };
        WaterRegion see25 = new WaterRegion()
            { Id = 25, Name = "Seezone 25", Identifier = ERegion.SeeZone25, PositionX = 382, PositionY = 886 };
        WaterRegion see26 = new WaterRegion()
            { Id = 26, Name = "Seezone 26", Identifier = ERegion.SeeZone26, PositionX = 363, PositionY = 1028 };
        WaterRegion see27 = new WaterRegion()
            { Id = 27, Name = "Seezone 27", Identifier = ERegion.SeeZone27, PositionX = 581, PositionY = 1063 };
        WaterRegion see28 = new WaterRegion()
            { Id = 28, Name = "Seezone 28", Identifier = ERegion.SeeZone28, PositionX = 741, PositionY = 1089 };
        WaterRegion see29 = new WaterRegion()
            { Id = 29, Name = "Seezone 29", Identifier = ERegion.SeeZone29, PositionX = 876, PositionY = 1075 };
        WaterRegion see30 = new WaterRegion()
            { Id = 30, Name = "Seezone 30", Identifier = ERegion.SeeZone30, PositionX = 1046, PositionY = 1086 };
        WaterRegion see31 = new WaterRegion()
            { Id = 31, Name = "Seezone 31", Identifier = ERegion.SeeZone31, PositionX = 1003, PositionY = 913 };
        WaterRegion see32 = new WaterRegion()
            { Id = 32, Name = "Seezone 32", Identifier = ERegion.SeeZone32, PositionX = 890, PositionY = 898 };
        WaterRegion see33 = new WaterRegion()
            { Id = 33, Name = "Seezone 33", Identifier = ERegion.SeeZone33, PositionX = 806, PositionY = 882 };
        WaterRegion see34 = new WaterRegion()
            { Id = 34, Name = "Seezone 34", Identifier = ERegion.SeeZone34, PositionX = 919, PositionY = 731 };
        WaterRegion see35 = new WaterRegion()
            { Id = 35, Name = "Seezone 35", Identifier = ERegion.SeeZone35, PositionX = 992, PositionY = 788 };
        WaterRegion see36 = new WaterRegion()
            { Id = 36, Name = "Seezone 36", Identifier = ERegion.SeeZone36, PositionX = 1201, PositionY = 821 };
        WaterRegion see37 = new WaterRegion()
            { Id = 37, Name = "Seezone 37", Identifier = ERegion.SeeZone37, PositionX = 1116, PositionY = 929 };
        WaterRegion see38 = new WaterRegion()
            { Id = 38, Name = "Seezone 38", Identifier = ERegion.SeeZone38, PositionX = 1262, PositionY = 1077 };
        WaterRegion see39 = new WaterRegion()
            { Id = 39, Name = "Seezone 39", Identifier = ERegion.SeeZone39, PositionX = 1477, PositionY = 1098 };
        WaterRegion see40 = new WaterRegion()
            { Id = 40, Name = "Seezone 40", Identifier = ERegion.SeeZone40, PositionX = 1590, PositionY = 996 };
        WaterRegion see41 = new WaterRegion()
            { Id = 41, Name = "Seezone 41", Identifier = ERegion.SeeZone41, PositionX = 1814, PositionY = 1019 };
        WaterRegion see42 = new WaterRegion()
            { Id = 42, Name = "Seezone 42", Identifier = ERegion.SeeZone42, PositionX = 1842, PositionY = 840 };
        WaterRegion see43 = new WaterRegion()
            { Id = 43, Name = "Seezone 43", Identifier = ERegion.SeeZone43, PositionX = 1724, PositionY = 840 };
        WaterRegion see44 = new WaterRegion()
            { Id = 44, Name = "Seezone 44", Identifier = ERegion.SeeZone44, PositionX = 1594, PositionY = 783 };
        WaterRegion see45 = new WaterRegion()
            { Id = 45, Name = "Seezone 45", Identifier = ERegion.SeeZone45, PositionX = 1515, PositionY = 912 };
        WaterRegion see46 = new WaterRegion()
            { Id = 46, Name = "Seezone 46", Identifier = ERegion.SeeZone46, PositionX = 1346, PositionY = 910 };
        WaterRegion see47 = new WaterRegion()
            { Id = 47, Name = "Seezone 47", Identifier = ERegion.SeeZone47, PositionX = 1332, PositionY = 825 };
        WaterRegion see48 = new WaterRegion()
            { Id = 48, Name = "Seezone 48", Identifier = ERegion.SeeZone48, PositionX = 1317, PositionY = 738 };
        WaterRegion see49 = new WaterRegion()
            { Id = 49, Name = "Seezone 49", Identifier = ERegion.SeeZone49, PositionX = 1469, PositionY = 782 };
        WaterRegion see50 = new WaterRegion()
            { Id = 50, Name = "Seezone 50", Identifier = ERegion.SeeZone50, PositionX = 1455, PositionY = 710 };
        WaterRegion see51 = new WaterRegion()
            { Id = 51, Name = "Seezone 51", Identifier = ERegion.SeeZone51, PositionX = 1465, PositionY = 574 };
        WaterRegion see52 = new WaterRegion()
            { Id = 52, Name = "Seezone 52", Identifier = ERegion.SeeZone52, PositionX = 1574, PositionY = 637 };
        WaterRegion see53 = new WaterRegion()
            { Id = 53, Name = "Seezone 53", Identifier = ERegion.SeeZone53, PositionX = 1677, PositionY = 643 };
        WaterRegion see54 = new WaterRegion()
            { Id = 54, Name = "Seezone 54", Identifier = ERegion.SeeZone54, PositionX = 1762, PositionY = 625 };
        WaterRegion see55 = new WaterRegion()
            { Id = 55, Name = "Seezone 55", Identifier = ERegion.SeeZone55, PositionX = 1868, PositionY = 606 };
        WaterRegion see56 = new WaterRegion()
            { Id = 56, Name = "Seezone 56", Identifier = ERegion.SeeZone56, PositionX = 1749, PositionY = 390 };
        WaterRegion see57 = new WaterRegion()
            { Id = 57, Name = "Seezone 57", Identifier = ERegion.SeeZone57, PositionX = 1658, PositionY = 367 };
        WaterRegion see58 = new WaterRegion()
            { Id = 58, Name = "Seezone 58", Identifier = ERegion.SeeZone58, PositionX = 1562, PositionY = 270 };
        WaterRegion see59 = new WaterRegion()
            { Id = 59, Name = "Seezone 59", Identifier = ERegion.SeeZone59, PositionX = 1569, PositionY = 417 };
        WaterRegion see60 = new WaterRegion()
            { Id = 60, Name = "Seezone 60", Identifier = ERegion.SeeZone60, PositionX = 1419, PositionY = 446 };
        WaterRegion see61 = new WaterRegion()
            { Id = 61, Name = "Seezone 61", Identifier = ERegion.SeeZone61, PositionX = 1326, PositionY = 490 };
        WaterRegion see62 = new WaterRegion()
            { Id = 62, Name = "Seezone 62", Identifier = ERegion.SeeZone62, PositionX = 1428, PositionY = 304 };
        WaterRegion see63 = new WaterRegion()
            { Id = 63, Name = "Seezone 63", Identifier = ERegion.SeeZone63, PositionX = 1573, PositionY = 132 };
        WaterRegion see64 = new WaterRegion()
            { Id = 64, Name = "Seezone 64", Identifier = ERegion.SeeZone64, PositionX = 1640, PositionY = 195 };
        WaterRegion see65 = new WaterRegion()
            { Id = 65, Name = "Seezone 65", Identifier = ERegion.SeeZone65, PositionX = 1736, PositionY = 221 };

        #endregion

        builder.Entity<WaterRegion>().HasData(new List<WaterRegion>(){
            see1,
            see2,
            see3,
            see4,
            see5,
            see6,
            see7,
            see8,
            see9,
            see10,
            see11,
            see12,
            see13,
            see14,
            see15,
            see16,
            see17,
            see18,
            see19,
            see20,
            see21,
            see22,
            see23,
            see24,
            see25,
            see26,
            see27,
            see28,
            see29,
            see30,
            see31,
            see32,
            see33,
            see34,
            see35,
            see36,
            see37,
            see38,
            see39,
            see40,
            see41,
            see42,
            see43,
            see44,
            see45,
            see46,
            see47,
            see48,
            see49,
            see50,
            see51,
            see52,
            see53,
            see54,
            see55,
            see56,
            see57,
            see58,
            see59,
            see60,
            see61,
            see62,
            see63,
            see64,
            see65
        });

        builder.Entity<Neighbours>().HasData(new List<Neighbours>(){
            #region WaterRegions

            new Neighbours(){
                RegionId = 1,
                NeighbourId = 2
            },
            new Neighbours(){
                RegionId = 1,
                NeighbourId = 10
            },
            new Neighbours(){
                RegionId = 1,
                NeighbourId = 112
            },
            new Neighbours(){
                RegionId = 2,
                NeighbourId = 3
            },
            new Neighbours(){
                RegionId = 2,
                NeighbourId = 123
            },
            new Neighbours(){
                RegionId = 2,
                NeighbourId = 7
            },
            new Neighbours(){
                RegionId = 2,
                NeighbourId = 9
            },
            new Neighbours(){
                RegionId = 2,
                NeighbourId = 1
            },
            new Neighbours(){
                RegionId = 2,
                NeighbourId = 10
            },
            new Neighbours(){
                RegionId = 3,
                NeighbourId = 2
            },
            new Neighbours(){
                RegionId = 3,
                NeighbourId = 111
            },
            new Neighbours(){
                RegionId = 3,
                NeighbourId = 72
            },
            new Neighbours(){
                RegionId = 3,
                NeighbourId = 73
            },
            new Neighbours(){
                RegionId = 3,
                NeighbourId = 7
            },
            new Neighbours(){
                RegionId = 3,
                NeighbourId = 6
            },
            new Neighbours(){
                RegionId = 3,
                NeighbourId = 4
            },
            new Neighbours(){
                RegionId = 4,
                NeighbourId = 3
            },
            new Neighbours(){
                RegionId = 4,
                NeighbourId = 84
            },
            new Neighbours(){
                RegionId = 4,
                NeighbourId = 85
            },
            new Neighbours(){
                RegionId = 5,
                NeighbourId = 6
            },
            new Neighbours(){
                RegionId = 5,
                NeighbourId = 84
            },
            new Neighbours(){
                RegionId = 5,
                NeighbourId = 68
            },
            new Neighbours(){
                RegionId = 5,
                NeighbourId = 66
            },
            new Neighbours(){
                RegionId = 5,
                NeighbourId = 72
            },
            new Neighbours(){
                RegionId = 5,
                NeighbourId = 73
            },
            new Neighbours(){
                RegionId = 5,
                NeighbourId = 79
            },
            new Neighbours(){
                RegionId = 6,
                NeighbourId = 5
            },
            new Neighbours(){
                RegionId = 6,
                NeighbourId = 79
            },
            new Neighbours(){
                RegionId = 6,
                NeighbourId = 73
            },
            new Neighbours(){
                RegionId = 6,
                NeighbourId = 110
            },
            new Neighbours(){
                RegionId = 6,
                NeighbourId = 7
            },
            new Neighbours(){
                RegionId = 6,
                NeighbourId = 3
            },
            new Neighbours(){
                RegionId = 6,
                NeighbourId = 8
            },
            new Neighbours(){
                RegionId = 7,
                NeighbourId = 6
            },
            new Neighbours(){
                RegionId = 7,
                NeighbourId = 110
            },
            new Neighbours(){
                RegionId = 7,
                NeighbourId = 3
            },
            new Neighbours(){
                RegionId = 7,
                NeighbourId = 2
            },
            new Neighbours(){
                RegionId = 7,
                NeighbourId = 8
            },
            new Neighbours(){
                RegionId = 7,
                NeighbourId = 9
            },
            new Neighbours(){
                RegionId = 8,
                NeighbourId = 6
            },
            new Neighbours(){
                RegionId = 8,
                NeighbourId = 110
            },
            new Neighbours(){
                RegionId = 8,
                NeighbourId = 78
            },
            new Neighbours(){
                RegionId = 8,
                NeighbourId = 7
            },
            new Neighbours(){
                RegionId = 8,
                NeighbourId = 9
            },
            new Neighbours(){
                RegionId = 8,
                NeighbourId = 13
            },
            new Neighbours(){
                RegionId = 9,
                NeighbourId = 2
            },
            new Neighbours(){
                RegionId = 9,
                NeighbourId = 7
            },
            new Neighbours(){
                RegionId = 9,
                NeighbourId = 8
            },
            new Neighbours(){
                RegionId = 9,
                NeighbourId = 13
            },
            new Neighbours(){
                RegionId = 9,
                NeighbourId = 12
            },
            new Neighbours(){
                RegionId = 9,
                NeighbourId = 10
            },
            new Neighbours(){
                RegionId = 10,
                NeighbourId = 2
            },
            new Neighbours(){
                RegionId = 10,
                NeighbourId = 112
            },
            new Neighbours(){
                RegionId = 10,
                NeighbourId = 1
            },
            new Neighbours(){
                RegionId = 10,
                NeighbourId = 9
            },
            new Neighbours(){
                RegionId = 10,
                NeighbourId = 11
            },
            new Neighbours(){
                RegionId = 10,
                NeighbourId = 12
            },
            new Neighbours(){
                RegionId = 11,
                NeighbourId = 10
            },
            new Neighbours(){
                RegionId = 11,
                NeighbourId = 116
            },
            new Neighbours(){
                RegionId = 11,
                NeighbourId = 117
            },
            new Neighbours(){
                RegionId = 11,
                NeighbourId = 119
            },
            new Neighbours(){
                RegionId = 11,
                NeighbourId = 12
            },
            new Neighbours(){
                RegionId = 11,
                NeighbourId = 18
            },
            new Neighbours(){
                RegionId = 12,
                NeighbourId = 9
            },
            new Neighbours(){
                RegionId = 12,
                NeighbourId = 10
            },
            new Neighbours(){
                RegionId = 12,
                NeighbourId = 11
            },
            new Neighbours(){
                RegionId = 12,
                NeighbourId = 18
            },
            new Neighbours(){
                RegionId = 12,
                NeighbourId = 13
            },
            new Neighbours(){
                RegionId = 12,
                NeighbourId = 22
            },
            new Neighbours(){
                RegionId = 12,
                NeighbourId = 23
            },
            new Neighbours(){
                RegionId = 13,
                NeighbourId = 9
            },
            new Neighbours(){
                RegionId = 13,
                NeighbourId = 109
            },
            new Neighbours(){
                RegionId = 13,
                NeighbourId = 80
            },
            new Neighbours(){
                RegionId = 13,
                NeighbourId = 8
            },
            new Neighbours(){
                RegionId = 13,
                NeighbourId = 14
            },
            new Neighbours(){
                RegionId = 13,
                NeighbourId = 12
            },
            new Neighbours(){
                RegionId = 13,
                NeighbourId = 23
            },
            new Neighbours(){
                RegionId = 14,
                NeighbourId = 13
            },
            new Neighbours(){
                RegionId = 14,
                NeighbourId = 109
            },
            new Neighbours(){
                RegionId = 14,
                NeighbourId = 80
            },
            new Neighbours(){
                RegionId = 14,
                NeighbourId = 81
            },
            new Neighbours(){
                RegionId = 14,
                NeighbourId = 78
            },
            new Neighbours(){
                RegionId = 14,
                NeighbourId = 15
            },
            new Neighbours(){
                RegionId = 15,
                NeighbourId = 14
            },
            new Neighbours(){
                RegionId = 15,
                NeighbourId = 16
            },
            new Neighbours(){
                RegionId = 15,
                NeighbourId = 77
            },
            new Neighbours(){
                RegionId = 15,
                NeighbourId = 76
            },
            new Neighbours(){
                RegionId = 15,
                NeighbourId = 82
            },
            new Neighbours(){
                RegionId = 15,
                NeighbourId = 17
            },
            new Neighbours(){
                RegionId = 16,
                NeighbourId = 15
            },
            new Neighbours(){
                RegionId = 16,
                NeighbourId = 75
            },
            new Neighbours(){
                RegionId = 16,
                NeighbourId = 70
            },
            new Neighbours(){
                RegionId = 16,
                NeighbourId = 74
            },
            new Neighbours(){
                RegionId = 17,
                NeighbourId = 15
            },
            new Neighbours(){
                RegionId = 17,
                NeighbourId = 34
            },
            new Neighbours(){
                RegionId = 17,
                NeighbourId = 83
            },
            new Neighbours(){
                RegionId = 17,
                NeighbourId = 94
            },
            new Neighbours(){
                RegionId = 18,
                NeighbourId = 11
            },
            new Neighbours(){
                RegionId = 18,
                NeighbourId = 12
            },
            new Neighbours(){
                RegionId = 18,
                NeighbourId = 119
            },
            new Neighbours(){
                RegionId = 18,
                NeighbourId = 118
            },
            new Neighbours(){
                RegionId = 18,
                NeighbourId = 120
            },
            new Neighbours(){
                RegionId = 18,
                NeighbourId = 22
            },
            new Neighbours(){
                RegionId = 18,
                NeighbourId = 19
            },
            new Neighbours(){
                RegionId = 19,
                NeighbourId = 18
            },
            new Neighbours(){
                RegionId = 19,
                NeighbourId = 119
            },
            new Neighbours(){
                RegionId = 19,
                NeighbourId = 120
            },
            new Neighbours(){
                RegionId = 19,
                NeighbourId = 55
            },
            new Neighbours(){
                RegionId = 19,
                NeighbourId = 20
            },
            new Neighbours(){
                RegionId = 20,
                NeighbourId = 19
            },
            new Neighbours(){
                RegionId = 20,
                NeighbourId = 42
            },
            new Neighbours(){
                RegionId = 20,
                NeighbourId = 21
            },
            new Neighbours(){
                RegionId = 21,
                NeighbourId = 20
            },
            new Neighbours(){
                RegionId = 21,
                NeighbourId = 41
            },
            new Neighbours(){
                RegionId = 21,
                NeighbourId = 26
            },
            new Neighbours(){
                RegionId = 21,
                NeighbourId = 25
            },
            new Neighbours(){
                RegionId = 21,
                NeighbourId = 22
            },
            new Neighbours(){
                RegionId = 22,
                NeighbourId = 21
            },
            new Neighbours(){
                RegionId = 22,
                NeighbourId = 25
            },
            new Neighbours(){
                RegionId = 22,
                NeighbourId = 122
            },
            new Neighbours(){
                RegionId = 22,
                NeighbourId = 23
            },
            new Neighbours(){
                RegionId = 22,
                NeighbourId = 12
            },
            new Neighbours(){
                RegionId = 22,
                NeighbourId = 18
            },
            new Neighbours(){
                RegionId = 23,
                NeighbourId = 22
            },
            new Neighbours(){
                RegionId = 23,
                NeighbourId = 108
            },
            new Neighbours(){
                RegionId = 23,
                NeighbourId = 25
            },
            new Neighbours(){
                RegionId = 23,
                NeighbourId = 24
            },
            new Neighbours(){
                RegionId = 23,
                NeighbourId = 13
            },
            new Neighbours(){
                RegionId = 23,
                NeighbourId = 12
            },
            new Neighbours(){
                RegionId = 24,
                NeighbourId = 23
            },
            new Neighbours(){
                RegionId = 24,
                NeighbourId = 107
            },
            new Neighbours(){
                RegionId = 24,
                NeighbourId = 104
            },
            new Neighbours(){
                RegionId = 24,
                NeighbourId = 25
            },
            new Neighbours(){
                RegionId = 24,
                NeighbourId = 27
            },
            new Neighbours(){
                RegionId = 25,
                NeighbourId = 24
            },
            new Neighbours(){
                RegionId = 25,
                NeighbourId = 23
            },
            new Neighbours(){
                RegionId = 25,
                NeighbourId = 22
            },
            new Neighbours(){
                RegionId = 25,
                NeighbourId = 21
            },
            new Neighbours(){
                RegionId = 25,
                NeighbourId = 26
            },
            new Neighbours(){
                RegionId = 25,
                NeighbourId = 27
            },
            new Neighbours(){
                RegionId = 26,
                NeighbourId = 21
            },
            new Neighbours(){
                RegionId = 26,
                NeighbourId = 25
            },
            new Neighbours(){
                RegionId = 26,
                NeighbourId = 27
            },
            new Neighbours(){
                RegionId = 27,
                NeighbourId = 26
            },
            new Neighbours(){
                RegionId = 27,
                NeighbourId = 24
            },
            new Neighbours(){
                RegionId = 27,
                NeighbourId = 28
            },
            new Neighbours(){
                RegionId = 27,
                NeighbourId = 102
            },
            new Neighbours(){
                RegionId = 27,
                NeighbourId = 25
            },
            new Neighbours(){
                RegionId = 28,
                NeighbourId = 27
            },
            new Neighbours(){
                RegionId = 28,
                NeighbourId = 102
            },
            new Neighbours(){
                RegionId = 28,
                NeighbourId = 101
            },
            new Neighbours(){
                RegionId = 28,
                NeighbourId = 29
            },
            new Neighbours(){
                RegionId = 28,
                NeighbourId = 33
            },
            new Neighbours(){
                RegionId = 29,
                NeighbourId = 28
            },
            new Neighbours(){
                RegionId = 29,
                NeighbourId = 101
            },
            new Neighbours(){
                RegionId = 29,
                NeighbourId = 30
            },
            new Neighbours(){
                RegionId = 29,
                NeighbourId = 32
            },
            new Neighbours(){
                RegionId = 29,
                NeighbourId = 31
            },
            new Neighbours(){
                RegionId = 30,
                NeighbourId = 29
            },
            new Neighbours(){
                RegionId = 30,
                NeighbourId = 31
            },
            new Neighbours(){
                RegionId = 30,
                NeighbourId = 37
            },
            new Neighbours(){
                RegionId = 30,
                NeighbourId = 38
            },
            new Neighbours(){
                RegionId = 31,
                NeighbourId = 29
            },
            new Neighbours(){
                RegionId = 31,
                NeighbourId = 30
            },
            new Neighbours(){
                RegionId = 31,
                NeighbourId = 37
            },
            new Neighbours(){
                RegionId = 31,
                NeighbourId = 35
            },
            new Neighbours(){
                RegionId = 31,
                NeighbourId = 32
            },
            new Neighbours(){
                RegionId = 32,
                NeighbourId = 29
            },
            new Neighbours(){
                RegionId = 32,
                NeighbourId = 101
            },
            new Neighbours(){
                RegionId = 32,
                NeighbourId = 31
            },
            new Neighbours(){
                RegionId = 32,
                NeighbourId = 33
            },
            new Neighbours(){
                RegionId = 32,
                NeighbourId = 34
            },
            new Neighbours(){
                RegionId = 32,
                NeighbourId = 35
            },
            new Neighbours(){
                RegionId = 33,
                NeighbourId = 28
            },
            new Neighbours(){
                RegionId = 33,
                NeighbourId = 101
            },
            new Neighbours(){
                RegionId = 33,
                NeighbourId = 103
            },
            new Neighbours(){
                RegionId = 33,
                NeighbourId = 106
            },
            new Neighbours(){
                RegionId = 33,
                NeighbourId = 32
            },
            new Neighbours(){
                RegionId = 33,
                NeighbourId = 34
            },
            new Neighbours(){
                RegionId = 34,
                NeighbourId = 17
            },
            new Neighbours(){
                RegionId = 34,
                NeighbourId = 106
            },
            new Neighbours(){
                RegionId = 34,
                NeighbourId = 105
            },
            new Neighbours(){
                RegionId = 34,
                NeighbourId = 83
            },
            new Neighbours(){
                RegionId = 34,
                NeighbourId = 94
            },
            new Neighbours(){
                RegionId = 34,
                NeighbourId = 95
            },
            new Neighbours(){
                RegionId = 34,
                NeighbourId = 33
            },
            new Neighbours(){
                RegionId = 34,
                NeighbourId = 32
            },
            new Neighbours(){
                RegionId = 34,
                NeighbourId = 35
            },
            new Neighbours(){
                RegionId = 35,
                NeighbourId = 34
            },
            new Neighbours(){
                RegionId = 35,
                NeighbourId = 96
            },
            new Neighbours(){
                RegionId = 35,
                NeighbourId = 36
            },
            new Neighbours(){
                RegionId = 35,
                NeighbourId = 31
            },
            new Neighbours(){
                RegionId = 35,
                NeighbourId = 37
            },
            new Neighbours(){
                RegionId = 35,
                NeighbourId = 32
            },
            new Neighbours(){
                RegionId = 36,
                NeighbourId = 35
            },
            new Neighbours(){
                RegionId = 36,
                NeighbourId = 97
            },
            new Neighbours(){
                RegionId = 36,
                NeighbourId = 134
            },
            new Neighbours(){
                RegionId = 36,
                NeighbourId = 135
            },
            new Neighbours(){
                RegionId = 36,
                NeighbourId = 37
            },
            new Neighbours(){
                RegionId = 36,
                NeighbourId = 47
            },
            new Neighbours(){
                RegionId = 36,
                NeighbourId = 48
            },
            new Neighbours(){
                RegionId = 36,
                NeighbourId = 61
            },
            new Neighbours(){
                RegionId = 37,
                NeighbourId = 35
            },
            new Neighbours(){
                RegionId = 37,
                NeighbourId = 137
            },
            new Neighbours(){
                RegionId = 37,
                NeighbourId = 31
            },
            new Neighbours(){
                RegionId = 37,
                NeighbourId = 30
            },
            new Neighbours(){
                RegionId = 37,
                NeighbourId = 38
            },
            new Neighbours(){
                RegionId = 37,
                NeighbourId = 46
            },
            new Neighbours(){
                RegionId = 37,
                NeighbourId = 47
            },
            new Neighbours(){
                RegionId = 37,
                NeighbourId = 36
            },
            new Neighbours(){
                RegionId = 38,
                NeighbourId = 30
            },
            new Neighbours(){
                RegionId = 38,
                NeighbourId = 98
            },
            new Neighbours(){
                RegionId = 38,
                NeighbourId = 37
            },
            new Neighbours(){
                RegionId = 38,
                NeighbourId = 46
            },
            new Neighbours(){
                RegionId = 38,
                NeighbourId = 39
            },
            new Neighbours(){
                RegionId = 39,
                NeighbourId = 38
            },
            new Neighbours(){
                RegionId = 39,
                NeighbourId = 99
            },
            new Neighbours(){
                RegionId = 39,
                NeighbourId = 40
            },
            new Neighbours(){
                RegionId = 39,
                NeighbourId = 45
            },
            new Neighbours(){
                RegionId = 40,
                NeighbourId = 39
            },
            new Neighbours(){
                RegionId = 40,
                NeighbourId = 100
            },
            new Neighbours(){
                RegionId = 40,
                NeighbourId = 44
            },
            new Neighbours(){
                RegionId = 40,
                NeighbourId = 41
            },
            new Neighbours(){
                RegionId = 40,
                NeighbourId = 43
            },
            new Neighbours(){
                RegionId = 40,
                NeighbourId = 45
            },
            new Neighbours(){
                RegionId = 41,
                NeighbourId = 40
            },
            new Neighbours(){
                RegionId = 41,
                NeighbourId = 43
            },
            new Neighbours(){
                RegionId = 41,
                NeighbourId = 42
            },
            new Neighbours(){
                RegionId = 41,
                NeighbourId = 21
            },
            new Neighbours(){
                RegionId = 42,
                NeighbourId = 41
            },
            new Neighbours(){
                RegionId = 42,
                NeighbourId = 20
            },
            new Neighbours(){
                RegionId = 42,
                NeighbourId = 43
            },
            new Neighbours(){
                RegionId = 42,
                NeighbourId = 55
            },
            new Neighbours(){
                RegionId = 42,
                NeighbourId = 54
            },
            new Neighbours(){
                RegionId = 43,
                NeighbourId = 42
            },
            new Neighbours(){
                RegionId = 43,
                NeighbourId = 41
            },
            new Neighbours(){
                RegionId = 43,
                NeighbourId = 40
            },
            new Neighbours(){
                RegionId = 43,
                NeighbourId = 44
            },
            new Neighbours(){
                RegionId = 43,
                NeighbourId = 53
            },
            new Neighbours(){
                RegionId = 43,
                NeighbourId = 54
            },
            new Neighbours(){
                RegionId = 44,
                NeighbourId = 43
            },
            new Neighbours(){
                RegionId = 44,
                NeighbourId = 40
            },
            new Neighbours(){
                RegionId = 44,
                NeighbourId = 45
            },
            new Neighbours(){
                RegionId = 44,
                NeighbourId = 49
            },
            new Neighbours(){
                RegionId = 44,
                NeighbourId = 50
            },
            new Neighbours(){
                RegionId = 44,
                NeighbourId = 52
            },
            new Neighbours(){
                RegionId = 44,
                NeighbourId = 53
            },
            new Neighbours(){
                RegionId = 44,
                NeighbourId = 138
            },
            new Neighbours(){
                RegionId = 45,
                NeighbourId = 99
            },
            new Neighbours(){
                RegionId = 45,
                NeighbourId = 39
            },
            new Neighbours(){
                RegionId = 45,
                NeighbourId = 40
            },
            new Neighbours(){
                RegionId = 45,
                NeighbourId = 44
            },
            new Neighbours(){
                RegionId = 45,
                NeighbourId = 49
            },
            new Neighbours(){
                RegionId = 45,
                NeighbourId = 46
            },
            new Neighbours(){
                RegionId = 46,
                NeighbourId = 98
            },
            new Neighbours(){
                RegionId = 46,
                NeighbourId = 45
            },
            new Neighbours(){
                RegionId = 46,
                NeighbourId = 49
            },
            new Neighbours(){
                RegionId = 46,
                NeighbourId = 47
            },
            new Neighbours(){
                RegionId = 46,
                NeighbourId = 37
            },
            new Neighbours(){
                RegionId = 46,
                NeighbourId = 38
            },
            new Neighbours(){
                RegionId = 47,
                NeighbourId = 136
            },
            new Neighbours(){
                RegionId = 47,
                NeighbourId = 46
            },
            new Neighbours(){
                RegionId = 47,
                NeighbourId = 37
            },
            new Neighbours(){
                RegionId = 47,
                NeighbourId = 36
            },
            new Neighbours(){
                RegionId = 47,
                NeighbourId = 48
            },
            new Neighbours(){
                RegionId = 47,
                NeighbourId = 49
            },
            new Neighbours(){
                RegionId = 48,
                NeighbourId = 140
            },
            new Neighbours(){
                RegionId = 48,
                NeighbourId = 47
            },
            new Neighbours(){
                RegionId = 48,
                NeighbourId = 36
            },
            new Neighbours(){
                RegionId = 48,
                NeighbourId = 61
            },
            new Neighbours(){
                RegionId = 48,
                NeighbourId = 60
            },
            new Neighbours(){
                RegionId = 48,
                NeighbourId = 51
            },
            new Neighbours(){
                RegionId = 48,
                NeighbourId = 50
            },
            new Neighbours(){
                RegionId = 48,
                NeighbourId = 49
            },
            new Neighbours(){
                RegionId = 49,
                NeighbourId = 48
            },
            new Neighbours(){
                RegionId = 49,
                NeighbourId = 139
            },
            new Neighbours(){
                RegionId = 49,
                NeighbourId = 47
            },
            new Neighbours(){
                RegionId = 49,
                NeighbourId = 46
            },
            new Neighbours(){
                RegionId = 49,
                NeighbourId = 45
            },
            new Neighbours(){
                RegionId = 49,
                NeighbourId = 44
            },
            new Neighbours(){
                RegionId = 49,
                NeighbourId = 50
            },
            new Neighbours(){
                RegionId = 50,
                NeighbourId = 49
            },
            new Neighbours(){
                RegionId = 50,
                NeighbourId = 145
            },
            new Neighbours(){
                RegionId = 50,
                NeighbourId = 48
            },
            new Neighbours(){
                RegionId = 50,
                NeighbourId = 44
            },
            new Neighbours(){
                RegionId = 50,
                NeighbourId = 52
            },
            new Neighbours(){
                RegionId = 50,
                NeighbourId = 51
            },
            new Neighbours(){
                RegionId = 51,
                NeighbourId = 50
            },
            new Neighbours(){
                RegionId = 51,
                NeighbourId = 142
            },
            new Neighbours(){
                RegionId = 51,
                NeighbourId = 48
            },
            new Neighbours(){
                RegionId = 51,
                NeighbourId = 60
            },
            new Neighbours(){
                RegionId = 51,
                NeighbourId = 59
            },
            new Neighbours(){
                RegionId = 51,
                NeighbourId = 52
            },
            new Neighbours(){
                RegionId = 52,
                NeighbourId = 51
            },
            new Neighbours(){
                RegionId = 52,
                NeighbourId = 144
            },
            new Neighbours(){
                RegionId = 52,
                NeighbourId = 44
            },
            new Neighbours(){
                RegionId = 52,
                NeighbourId = 50
            },
            new Neighbours(){
                RegionId = 52,
                NeighbourId = 59
            },
            new Neighbours(){
                RegionId = 52,
                NeighbourId = 57
            },
            new Neighbours(){
                RegionId = 52,
                NeighbourId = 53
            },
            new Neighbours(){
                RegionId = 53,
                NeighbourId = 52
            },
            new Neighbours(){
                RegionId = 53,
                NeighbourId = 125
            },
            new Neighbours(){
                RegionId = 53,
                NeighbourId = 44
            },
            new Neighbours(){
                RegionId = 53,
                NeighbourId = 43
            },
            new Neighbours(){
                RegionId = 53,
                NeighbourId = 57
            },
            new Neighbours(){
                RegionId = 53,
                NeighbourId = 56
            },
            new Neighbours(){
                RegionId = 53,
                NeighbourId = 54
            },
            new Neighbours(){
                RegionId = 54,
                NeighbourId = 53
            },
            new Neighbours(){
                RegionId = 54,
                NeighbourId = 43
            },
            new Neighbours(){
                RegionId = 54,
                NeighbourId = 42
            },
            new Neighbours(){
                RegionId = 54,
                NeighbourId = 56
            },
            new Neighbours(){
                RegionId = 54,
                NeighbourId = 55
            },
            new Neighbours(){
                RegionId = 55,
                NeighbourId = 54
            },
            new Neighbours(){
                RegionId = 55,
                NeighbourId = 42
            },
            new Neighbours(){
                RegionId = 55,
                NeighbourId = 19
            },
            new Neighbours(){
                RegionId = 55,
                NeighbourId = 121
            },
            new Neighbours(){
                RegionId = 55,
                NeighbourId = 56
            },
            new Neighbours(){
                RegionId = 56,
                NeighbourId = 55
            },
            new Neighbours(){
                RegionId = 56,
                NeighbourId = 115
            },
            new Neighbours(){
                RegionId = 56,
                NeighbourId = 54
            },
            new Neighbours(){
                RegionId = 56,
                NeighbourId = 65
            },
            new Neighbours(){
                RegionId = 56,
                NeighbourId = 53
            },
            new Neighbours(){
                RegionId = 56,
                NeighbourId = 57
            },
            new Neighbours(){
                RegionId = 57,
                NeighbourId = 56
            },
            new Neighbours(){
                RegionId = 57,
                NeighbourId = 124
            },
            new Neighbours(){
                RegionId = 57,
                NeighbourId = 53
            },
            new Neighbours(){
                RegionId = 57,
                NeighbourId = 59
            },
            new Neighbours(){
                RegionId = 57,
                NeighbourId = 52
            },
            new Neighbours(){
                RegionId = 57,
                NeighbourId = 65
            },
            new Neighbours(){
                RegionId = 57,
                NeighbourId = 64
            },
            new Neighbours(){
                RegionId = 57,
                NeighbourId = 58
            },
            new Neighbours(){
                RegionId = 58,
                NeighbourId = 57
            },
            new Neighbours(){
                RegionId = 58,
                NeighbourId = 60
            },
            new Neighbours(){
                RegionId = 58,
                NeighbourId = 63
            },
            new Neighbours(){
                RegionId = 58,
                NeighbourId = 64
            },
            new Neighbours(){
                RegionId = 58,
                NeighbourId = 59
            },
            new Neighbours(){
                RegionId = 59,
                NeighbourId = 58
            },
            new Neighbours(){
                RegionId = 59,
                NeighbourId = 143
            },
            new Neighbours(){
                RegionId = 59,
                NeighbourId = 52
            },
            new Neighbours(){
                RegionId = 59,
                NeighbourId = 51
            },
            new Neighbours(){
                RegionId = 59,
                NeighbourId = 57
            },
            new Neighbours(){
                RegionId = 59,
                NeighbourId = 60
            },
            new Neighbours(){
                RegionId = 60,
                NeighbourId = 59
            },
            new Neighbours(){
                RegionId = 60,
                NeighbourId = 130
            },
            new Neighbours(){
                RegionId = 60,
                NeighbourId = 51
            },
            new Neighbours(){
                RegionId = 60,
                NeighbourId = 48
            },
            new Neighbours(){
                RegionId = 60,
                NeighbourId = 62
            },
            new Neighbours(){
                RegionId = 60,
                NeighbourId = 58
            },
            new Neighbours(){
                RegionId = 60,
                NeighbourId = 63
            },
            new Neighbours(){
                RegionId = 60,
                NeighbourId = 61
            },
            new Neighbours(){
                RegionId = 61,
                NeighbourId = 60
            },
            new Neighbours(){
                RegionId = 61,
                NeighbourId = 48
            },
            new Neighbours(){
                RegionId = 61,
                NeighbourId = 36
            },
            new Neighbours(){
                RegionId = 61,
                NeighbourId = 132
            },
            new Neighbours(){
                RegionId = 61,
                NeighbourId = 133
            },
            new Neighbours(){
                RegionId = 61,
                NeighbourId = 141
            },
            new Neighbours(){
                RegionId = 61,
                NeighbourId = 129
            },
            new Neighbours(){
                RegionId = 61,
                NeighbourId = 62
            },
            new Neighbours(){
                RegionId = 62,
                NeighbourId = 61
            },
            new Neighbours(){
                RegionId = 62,
                NeighbourId = 130
            },
            new Neighbours(){
                RegionId = 62,
                NeighbourId = 131
            },
            new Neighbours(){
                RegionId = 62,
                NeighbourId = 60
            },
            new Neighbours(){
                RegionId = 62,
                NeighbourId = 92
            },
            new Neighbours(){
                RegionId = 62,
                NeighbourId = 63
            },
            new Neighbours(){
                RegionId = 63,
                NeighbourId = 62
            },
            new Neighbours(){
                RegionId = 63,
                NeighbourId = 92
            },
            new Neighbours(){
                RegionId = 63,
                NeighbourId = 93
            },
            new Neighbours(){
                RegionId = 63,
                NeighbourId = 60
            },
            new Neighbours(){
                RegionId = 63,
                NeighbourId = 58
            },
            new Neighbours(){
                RegionId = 63,
                NeighbourId = 64
            },
            new Neighbours(){
                RegionId = 64,
                NeighbourId = 63
            },
            new Neighbours(){
                RegionId = 64,
                NeighbourId = 58
            },
            new Neighbours(){
                RegionId = 64,
                NeighbourId = 57
            },
            new Neighbours(){
                RegionId = 64,
                NeighbourId = 114
            },
            new Neighbours(){
                RegionId = 64,
                NeighbourId = 65
            },
            new Neighbours(){
                RegionId = 65,
                NeighbourId = 64
            },
            new Neighbours(){
                RegionId = 65,
                NeighbourId = 114
            },
            new Neighbours(){
                RegionId = 65,
                NeighbourId = 56
            },
            new Neighbours(){
                RegionId = 65,
                NeighbourId = 57
            },
            new Neighbours(){
                RegionId = 65,
                NeighbourId = 113
            },

            #endregion

            #region Deutschland

            new Neighbours(){
                RegionId = 66,
                NeighbourId = 67
            },
            new Neighbours(){
                RegionId = 66,
                NeighbourId = 5
            },
            new Neighbours(){
                RegionId = 66,
                NeighbourId = 68
            },
            new Neighbours(){
                RegionId = 66,
                NeighbourId = 75
            },
            new Neighbours(){
                RegionId = 66,
                NeighbourId = 76
            },
            new Neighbours(){
                RegionId = 66,
                NeighbourId = 77
            },
            new Neighbours(){
                RegionId = 66,
                NeighbourId = 78
            },
            new Neighbours(){
                RegionId = 66,
                NeighbourId = 79
            },
            new Neighbours(){
                RegionId = 67,
                NeighbourId = 66
            },
            new Neighbours(){
                RegionId = 67,
                NeighbourId = 68
            },
            new Neighbours(){
                RegionId = 67,
                NeighbourId = 71
            },
            new Neighbours(){
                RegionId = 67,
                NeighbourId = 70
            },
            new Neighbours(){
                RegionId = 67,
                NeighbourId = 75
            },
            new Neighbours(){
                RegionId = 68,
                NeighbourId = 66
            },
            new Neighbours(){
                RegionId = 68,
                NeighbourId = 5
            },
            new Neighbours(){
                RegionId = 68,
                NeighbourId = 67
            },
            new Neighbours(){
                RegionId = 68,
                NeighbourId = 69
            },
            new Neighbours(){
                RegionId = 68,
                NeighbourId = 84
            },
            new Neighbours(){
                RegionId = 69,
                NeighbourId = 67
            },
            new Neighbours(){
                RegionId = 69,
                NeighbourId = 68
            },
            new Neighbours(){
                RegionId = 69,
                NeighbourId = 71
            },
            new Neighbours(){
                RegionId = 69,
                NeighbourId = 70
            },
            new Neighbours(){
                RegionId = 69,
                NeighbourId = 84
            },
            new Neighbours(){
                RegionId = 70,
                NeighbourId = 69
            },
            new Neighbours(){
                RegionId = 70,
                NeighbourId = 71
            },
            new Neighbours(){
                RegionId = 70,
                NeighbourId = 75
            },
            new Neighbours(){
                RegionId = 70,
                NeighbourId = 67
            },
            new Neighbours(){
                RegionId = 70,
                NeighbourId = 74
            },
            new Neighbours(){
                RegionId = 70,
                NeighbourId = 16
            },
            new Neighbours(){
                RegionId = 71,
                NeighbourId = 69
            },
            new Neighbours(){
                RegionId = 71,
                NeighbourId = 70
            },
            new Neighbours(){
                RegionId = 71,
                NeighbourId = 74
            },
            new Neighbours(){
                RegionId = 71,
                NeighbourId = 84
            },
            new Neighbours(){
                RegionId = 71,
                NeighbourId = 85
            },
            new Neighbours(){
                RegionId = 71,
                NeighbourId = 86
            },
            new Neighbours(){
                RegionId = 72,
                NeighbourId = 84
            },
            new Neighbours(){
                RegionId = 72,
                NeighbourId = 73
            },
            new Neighbours(){
                RegionId = 72,
                NeighbourId = 3
            },
            new Neighbours(){
                RegionId = 72,
                NeighbourId = 5
            },
            new Neighbours(){
                RegionId = 73,
                NeighbourId = 72
            },
            new Neighbours(){
                RegionId = 73,
                NeighbourId = 3
            },
            new Neighbours(){
                RegionId = 73,
                NeighbourId = 6
            },
            new Neighbours(){
                RegionId = 73,
                NeighbourId = 5
            },
            new Neighbours(){
                RegionId = 75,
                NeighbourId = 70
            },
            new Neighbours(){
                RegionId = 75,
                NeighbourId = 67
            },
            new Neighbours(){
                RegionId = 75,
                NeighbourId = 16
            },
            new Neighbours(){
                RegionId = 75,
                NeighbourId = 76
            },
            new Neighbours(){
                RegionId = 75,
                NeighbourId = 66
            },
            new Neighbours(){
                RegionId = 76,
                NeighbourId = 15
            },
            new Neighbours(){
                RegionId = 76,
                NeighbourId = 77
            },
            new Neighbours(){
                RegionId = 76,
                NeighbourId = 75
            },
            new Neighbours(){
                RegionId = 76,
                NeighbourId = 66
            },
            new Neighbours(){
                RegionId = 77,
                NeighbourId = 76
            },
            new Neighbours(){
                RegionId = 77,
                NeighbourId = 15
            },
            new Neighbours(){
                RegionId = 77,
                NeighbourId = 78
            },
            new Neighbours(){
                RegionId = 77,
                NeighbourId = 66
            },
            new Neighbours(){
                RegionId = 78,
                NeighbourId = 77
            },
            new Neighbours(){
                RegionId = 78,
                NeighbourId = 14
            },
            new Neighbours(){
                RegionId = 78,
                NeighbourId = 8
            },
            new Neighbours(){
                RegionId = 78,
                NeighbourId = 79
            },
            new Neighbours(){
                RegionId = 78,
                NeighbourId = 66
            },
            new Neighbours(){
                RegionId = 79,
                NeighbourId = 66
            },
            new Neighbours(){
                RegionId = 79,
                NeighbourId = 78
            },
            new Neighbours(){
                RegionId = 79,
                NeighbourId = 5
            },
            new Neighbours(){
                RegionId = 79,
                NeighbourId = 6
            },
            new Neighbours(){
                RegionId = 80,
                NeighbourId = 13
            },
            new Neighbours(){
                RegionId = 80,
                NeighbourId = 14
            },
            new Neighbours(){
                RegionId = 80,
                NeighbourId = 81
            },
            new Neighbours(){
                RegionId = 81,
                NeighbourId = 14
            },
            new Neighbours(){
                RegionId = 81,
                NeighbourId = 80
            },
            new Neighbours(){
                RegionId = 81,
                NeighbourId = 82
            },
            new Neighbours(){
                RegionId = 82,
                NeighbourId = 81
            },
            new Neighbours(){
                RegionId = 82,
                NeighbourId = 15
            },
            new Neighbours(){
                RegionId = 82,
                NeighbourId = 83
            },

            #endregion

            #region Russland

            new Neighbours(){
                RegionId = 74,
                NeighbourId = 71
            },
            new Neighbours(){
                RegionId = 74,
                NeighbourId = 70
            },
            new Neighbours(){
                RegionId = 74,
                NeighbourId = 16
            },
            new Neighbours(){
                RegionId = 74,
                NeighbourId = 86
            },
            new Neighbours(){
                RegionId = 74,
                NeighbourId = 90
            },
            new Neighbours(){
                RegionId = 84,
                NeighbourId = 72
            },
            new Neighbours(){
                RegionId = 84,
                NeighbourId = 71
            },
            new Neighbours(){
                RegionId = 84,
                NeighbourId = 69
            },
            new Neighbours(){
                RegionId = 84,
                NeighbourId = 68
            },
            new Neighbours(){
                RegionId = 84,
                NeighbourId = 5
            },
            new Neighbours(){
                RegionId = 84,
                NeighbourId = 4
            },
            new Neighbours(){
                RegionId = 84,
                NeighbourId = 85
            },
            new Neighbours(){
                RegionId = 85,
                NeighbourId = 84
            },
            new Neighbours(){
                RegionId = 85,
                NeighbourId = 4
            },
            new Neighbours(){
                RegionId = 85,
                NeighbourId = 87
            },
            new Neighbours(){
                RegionId = 85,
                NeighbourId = 88
            },
            new Neighbours(){
                RegionId = 85,
                NeighbourId = 71
            },
            new Neighbours(){
                RegionId = 85,
                NeighbourId = 86
            },
            new Neighbours(){
                RegionId = 86,
                NeighbourId = 85
            },
            new Neighbours(){
                RegionId = 86,
                NeighbourId = 71
            },
            new Neighbours(){
                RegionId = 86,
                NeighbourId = 88
            },
            new Neighbours(){
                RegionId = 86,
                NeighbourId = 89
            },
            new Neighbours(){
                RegionId = 86,
                NeighbourId = 90
            },
            new Neighbours(){
                RegionId = 86,
                NeighbourId = 74
            },
            new Neighbours(){
                RegionId = 87,
                NeighbourId = 85
            },
            new Neighbours(){
                RegionId = 87,
                NeighbourId = 89
            },
            new Neighbours(){
                RegionId = 87,
                NeighbourId = 91
            },
            new Neighbours(){
                RegionId = 87,
                NeighbourId = 126
            },
            new Neighbours(){
                RegionId = 87,
                NeighbourId = 88
            },
            new Neighbours(){
                RegionId = 88,
                NeighbourId = 87
            },
            new Neighbours(){
                RegionId = 88,
                NeighbourId = 86
            },
            new Neighbours(){
                RegionId = 88,
                NeighbourId = 85
            },
            new Neighbours(){
                RegionId = 88,
                NeighbourId = 89
            },
            new Neighbours(){
                RegionId = 89,
                NeighbourId = 88
            },
            new Neighbours(){
                RegionId = 89,
                NeighbourId = 126
            },
            new Neighbours(){
                RegionId = 89,
                NeighbourId = 86
            },
            new Neighbours(){
                RegionId = 89,
                NeighbourId = 87
            },
            new Neighbours(){
                RegionId = 89,
                NeighbourId = 90
            },
            new Neighbours(){
                RegionId = 90,
                NeighbourId = 89
            },
            new Neighbours(){
                RegionId = 90,
                NeighbourId = 86
            },
            new Neighbours(){
                RegionId = 90,
                NeighbourId = 74
            },
            new Neighbours(){
                RegionId = 90,
                NeighbourId = 126
            },
            new Neighbours(){
                RegionId = 90,
                NeighbourId = 128
            },
            new Neighbours(){
                RegionId = 90,
                NeighbourId = 95
            },
            new Neighbours(){
                RegionId = 91,
                NeighbourId = 87
            },
            new Neighbours(){
                RegionId = 91,
                NeighbourId = 93
            },
            new Neighbours(){
                RegionId = 91,
                NeighbourId = 92
            },
            new Neighbours(){
                RegionId = 92,
                NeighbourId = 91
            },
            new Neighbours(){
                RegionId = 92,
                NeighbourId = 63
            },
            new Neighbours(){
                RegionId = 92,
                NeighbourId = 62
            },
            new Neighbours(){
                RegionId = 92,
                NeighbourId = 131
            },
            new Neighbours(){
                RegionId = 92,
                NeighbourId = 93
            },
            new Neighbours(){
                RegionId = 93,
                NeighbourId = 92
            },
            new Neighbours(){
                RegionId = 93,
                NeighbourId = 91
            },
            new Neighbours(){
                RegionId = 93,
                NeighbourId = 63
            },

            #endregion

            #region Großbritannien

            new Neighbours(){
                RegionId = 83,
                NeighbourId = 82
            },
            new Neighbours(){
                RegionId = 83,
                NeighbourId = 17
            },
            new Neighbours(){
                RegionId = 83,
                NeighbourId = 34
            },
            new Neighbours(){
                RegionId = 83,
                NeighbourId = 105
            },
            new Neighbours(){
                RegionId = 83,
                NeighbourId = 94
            },
            new Neighbours(){
                RegionId = 94,
                NeighbourId = 83
            },
            new Neighbours(){
                RegionId = 94,
                NeighbourId = 17
            },
            new Neighbours(){
                RegionId = 94,
                NeighbourId = 34
            },
            new Neighbours(){
                RegionId = 94,
                NeighbourId = 95
            },
            new Neighbours(){
                RegionId = 95,
                NeighbourId = 94
            },
            new Neighbours(){
                RegionId = 95,
                NeighbourId = 74
            },
            new Neighbours(){
                RegionId = 95,
                NeighbourId = 34
            },
            new Neighbours(){
                RegionId = 95,
                NeighbourId = 90
            },
            new Neighbours(){
                RegionId = 95,
                NeighbourId = 96
            },
            new Neighbours(){
                RegionId = 96,
                NeighbourId = 95
            },
            new Neighbours(){
                RegionId = 96,
                NeighbourId = 35
            },
            new Neighbours(){
                RegionId = 96,
                NeighbourId = 97
            },
            new Neighbours(){
                RegionId = 97,
                NeighbourId = 96
            },
            new Neighbours(){
                RegionId = 97,
                NeighbourId = 36
            },
            new Neighbours(){
                RegionId = 97,
                NeighbourId = 129
            },
            new Neighbours(){
                RegionId = 97,
                NeighbourId = 134
            },
            new Neighbours(){
                RegionId = 98,
                NeighbourId = 46
            },
            new Neighbours(){
                RegionId = 98,
                NeighbourId = 38
            },
            new Neighbours(){
                RegionId = 98,
                NeighbourId = 99
            },
            new Neighbours(){
                RegionId = 99,
                NeighbourId = 98
            },
            new Neighbours(){
                RegionId = 99,
                NeighbourId = 45
            },
            new Neighbours(){
                RegionId = 99,
                NeighbourId = 39
            },
            new Neighbours(){
                RegionId = 100,
                NeighbourId = 40
            },
            new Neighbours(){
                RegionId = 101,
                NeighbourId = 29
            },
            new Neighbours(){
                RegionId = 101,
                NeighbourId = 28
            },
            new Neighbours(){
                RegionId = 101,
                NeighbourId = 33
            },
            new Neighbours(){
                RegionId = 101,
                NeighbourId = 32
            },
            new Neighbours(){
                RegionId = 102,
                NeighbourId = 27
            },
            new Neighbours(){
                RegionId = 102,
                NeighbourId = 28
            },
            new Neighbours(){
                RegionId = 102,
                NeighbourId = 104
            },
            new Neighbours(){
                RegionId = 102,
                NeighbourId = 103
            },
            new Neighbours(){
                RegionId = 103,
                NeighbourId = 102
            },
            new Neighbours(){
                RegionId = 103,
                NeighbourId = 33
            },
            new Neighbours(){
                RegionId = 103,
                NeighbourId = 106
            },
            new Neighbours(){
                RegionId = 103,
                NeighbourId = 105
            },
            new Neighbours(){
                RegionId = 103,
                NeighbourId = 104
            },
            new Neighbours(){
                RegionId = 104,
                NeighbourId = 103
            },
            new Neighbours(){
                RegionId = 104,
                NeighbourId = 102
            },
            new Neighbours(){
                RegionId = 104,
                NeighbourId = 107
            },
            new Neighbours(){
                RegionId = 104,
                NeighbourId = 24
            },
            new Neighbours(){
                RegionId = 104,
                NeighbourId = 105
            },
            new Neighbours(){
                RegionId = 105,
                NeighbourId = 104
            },
            new Neighbours(){
                RegionId = 105,
                NeighbourId = 103
            },
            new Neighbours(){
                RegionId = 105,
                NeighbourId = 34
            },
            new Neighbours(){
                RegionId = 105,
                NeighbourId = 83
            },
            new Neighbours(){
                RegionId = 105,
                NeighbourId = 108
            },
            new Neighbours(){
                RegionId = 105,
                NeighbourId = 106
            },
            new Neighbours(){
                RegionId = 106,
                NeighbourId = 105
            },
            new Neighbours(){
                RegionId = 106,
                NeighbourId = 103
            },
            new Neighbours(){
                RegionId = 106,
                NeighbourId = 34
            },
            new Neighbours(){
                RegionId = 106,
                NeighbourId = 33
            },
            new Neighbours(){
                RegionId = 107,
                NeighbourId = 104
            },
            new Neighbours(){
                RegionId = 107,
                NeighbourId = 105
            },
            new Neighbours(){
                RegionId = 107,
                NeighbourId = 24
            },
            new Neighbours(){
                RegionId = 107,
                NeighbourId = 108
            },
            new Neighbours(){
                RegionId = 108,
                NeighbourId = 107
            },
            new Neighbours(){
                RegionId = 108,
                NeighbourId = 23
            },
            new Neighbours(){
                RegionId = 109,
                NeighbourId = 13
            },
            new Neighbours(){
                RegionId = 109,
                NeighbourId = 14
            },
            new Neighbours(){
                RegionId = 110,
                NeighbourId = 8
            },
            new Neighbours(){
                RegionId = 110,
                NeighbourId = 6
            },
            new Neighbours(){
                RegionId = 110,
                NeighbourId = 7
            },
            new Neighbours(){
                RegionId = 111,
                NeighbourId = 3
            },
            new Neighbours(){
                RegionId = 112,
                NeighbourId = 1
            },
            new Neighbours(){
                RegionId = 112,
                NeighbourId = 10
            },
            new Neighbours(){
                RegionId = 112,
                NeighbourId = 116
            },
            new Neighbours(){
                RegionId = 112,
                NeighbourId = 117
            },
            new Neighbours(){
                RegionId = 112,
                NeighbourId = 113
            },
            new Neighbours(){
                RegionId = 113,
                NeighbourId = 112
            },
            new Neighbours(){
                RegionId = 113,
                NeighbourId = 115
            },
            new Neighbours(){
                RegionId = 113,
                NeighbourId = 65
            },
            new Neighbours(){
                RegionId = 113,
                NeighbourId = 114
            },

            #endregion

            #region USA

            new Neighbours(){
                RegionId = 114,
                NeighbourId = 113
            },
            new Neighbours(){
                RegionId = 114,
                NeighbourId = 65
            },
            new Neighbours(){
                RegionId = 114,
                NeighbourId = 64
            },
            new Neighbours(){
                RegionId = 115,
                NeighbourId = 113
            },
            new Neighbours(){
                RegionId = 115,
                NeighbourId = 56
            },
            new Neighbours(){
                RegionId = 115,
                NeighbourId = 121
            },
            new Neighbours(){
                RegionId = 115,
                NeighbourId = 116
            },
            new Neighbours(){
                RegionId = 116,
                NeighbourId = 115
            },
            new Neighbours(){
                RegionId = 116,
                NeighbourId = 112
            },
            new Neighbours(){
                RegionId = 116,
                NeighbourId = 11
            },
            new Neighbours(){
                RegionId = 116,
                NeighbourId = 119
            },
            new Neighbours(){
                RegionId = 116,
                NeighbourId = 117
            },
            new Neighbours(){
                RegionId = 117,
                NeighbourId = 116
            },
            new Neighbours(){
                RegionId = 117,
                NeighbourId = 112
            },
            new Neighbours(){
                RegionId = 117,
                NeighbourId = 11
            },
            new Neighbours(){
                RegionId = 118,
                NeighbourId = 18
            },
            new Neighbours(){
                RegionId = 119,
                NeighbourId = 116
            },
            new Neighbours(){
                RegionId = 119,
                NeighbourId = 11
            },
            new Neighbours(){
                RegionId = 119,
                NeighbourId = 18
            },
            new Neighbours(){
                RegionId = 119,
                NeighbourId = 19
            },
            new Neighbours(){
                RegionId = 119,
                NeighbourId = 121
            },
            new Neighbours(){
                RegionId = 119,
                NeighbourId = 120
            },
            new Neighbours(){
                RegionId = 120,
                NeighbourId = 119
            },
            new Neighbours(){
                RegionId = 120,
                NeighbourId = 19
            },
            new Neighbours(){
                RegionId = 120,
                NeighbourId = 18
            },
            new Neighbours(){
                RegionId = 121,
                NeighbourId = 119
            },
            new Neighbours(){
                RegionId = 121,
                NeighbourId = 115
            },
            new Neighbours(){
                RegionId = 121,
                NeighbourId = 55
            },
            new Neighbours(){
                RegionId = 122,
                NeighbourId = 22
            },
            new Neighbours(){
                RegionId = 123,
                NeighbourId = 2
            },
            new Neighbours(){
                RegionId = 124,
                NeighbourId = 57
            },
            new Neighbours(){
                RegionId = 125,
                NeighbourId = 53
            },
            new Neighbours(){
                RegionId = 126,
                NeighbourId = 89
            },
            new Neighbours(){
                RegionId = 126,
                NeighbourId = 90
            },
            new Neighbours(){
                RegionId = 126,
                NeighbourId = 128
            },
            new Neighbours(){
                RegionId = 126,
                NeighbourId = 127
            },
            new Neighbours(){
                RegionId = 127,
                NeighbourId = 126
            },
            new Neighbours(){
                RegionId = 127,
                NeighbourId = 131
            },
            new Neighbours(){
                RegionId = 127,
                NeighbourId = 132
            },
            new Neighbours(){
                RegionId = 127,
                NeighbourId = 133
            },
            new Neighbours(){
                RegionId = 127,
                NeighbourId = 128
            },
            new Neighbours(){
                RegionId = 128,
                NeighbourId = 127
            },
            new Neighbours(){
                RegionId = 128,
                NeighbourId = 126
            },
            new Neighbours(){
                RegionId = 128,
                NeighbourId = 90
            },
            new Neighbours(){
                RegionId = 128,
                NeighbourId = 133
            },
            new Neighbours(){
                RegionId = 128,
                NeighbourId = 129
            },
            new Neighbours(){
                RegionId = 129,
                NeighbourId = 128
            },
            new Neighbours(){
                RegionId = 129,
                NeighbourId = 133
            },
            new Neighbours(){
                RegionId = 129,
                NeighbourId = 61
            },
            new Neighbours(){
                RegionId = 129,
                NeighbourId = 134
            },
            new Neighbours(){
                RegionId = 129,
                NeighbourId = 97
            },

            #endregion

            #region Japan

            new Neighbours(){
                RegionId = 130,
                NeighbourId = 60
            },
            new Neighbours(){
                RegionId = 130,
                NeighbourId = 62
            },
            new Neighbours(){
                RegionId = 131,
                NeighbourId = 62
            },
            new Neighbours(){
                RegionId = 131,
                NeighbourId = 92
            },
            new Neighbours(){
                RegionId = 131,
                NeighbourId = 127
            },
            new Neighbours(){
                RegionId = 131,
                NeighbourId = 132
            },
            new Neighbours(){
                RegionId = 132,
                NeighbourId = 131
            },
            new Neighbours(){
                RegionId = 132,
                NeighbourId = 127
            },
            new Neighbours(){
                RegionId = 132,
                NeighbourId = 61
            },
            new Neighbours(){
                RegionId = 132,
                NeighbourId = 133
            },
            new Neighbours(){
                RegionId = 133,
                NeighbourId = 132
            },
            new Neighbours(){
                RegionId = 133,
                NeighbourId = 127
            },
            new Neighbours(){
                RegionId = 133,
                NeighbourId = 61
            },
            new Neighbours(){
                RegionId = 133,
                NeighbourId = 128
            },
            new Neighbours(){
                RegionId = 133,
                NeighbourId = 129
            },
            new Neighbours(){
                RegionId = 134,
                NeighbourId = 36
            },
            new Neighbours(){
                RegionId = 134,
                NeighbourId = 129
            },
            new Neighbours(){
                RegionId = 134,
                NeighbourId = 97
            },
            new Neighbours(){
                RegionId = 134,
                NeighbourId = 135
            },
            new Neighbours(){
                RegionId = 135,
                NeighbourId = 134
            },
            new Neighbours(){
                RegionId = 135,
                NeighbourId = 36
            },
            new Neighbours(){
                RegionId = 136,
                NeighbourId = 47
            },
            new Neighbours(){
                RegionId = 137,
                NeighbourId = 37
            },
            new Neighbours(){
                RegionId = 138,
                NeighbourId = 44
            },
            new Neighbours(){
                RegionId = 139,
                NeighbourId = 49
            },
            new Neighbours(){
                RegionId = 140,
                NeighbourId = 48
            },
            new Neighbours(){
                RegionId = 141,
                NeighbourId = 61
            },
            new Neighbours(){
                RegionId = 142,
                NeighbourId = 51
            },
            new Neighbours(){
                RegionId = 143,
                NeighbourId = 59
            },
            new Neighbours(){
                RegionId = 144,
                NeighbourId = 52
            },
            new Neighbours(){
                RegionId = 145,
                NeighbourId = 50
            },

            #endregion
        });

        builder.Entity<CanalOwners>().HasData(new List<CanalOwners>(){
            new CanalOwners(){
                NeighboursRegionId = 18,
                NeighboursNeighbourId = 19,
                CanalOwnerId = 120
            },
            new CanalOwners(){
                NeighboursRegionId = 19,
                NeighboursNeighbourId = 18,
                CanalOwnerId = 120
            },
            new CanalOwners(){
                NeighboursRegionId = 17,
                NeighboursNeighbourId = 34,
                CanalOwnerId = 83
            },
            new CanalOwners(){
                NeighboursRegionId = 17,
                NeighboursNeighbourId = 34,
                CanalOwnerId = 94
            },
            new CanalOwners(){
                NeighboursRegionId = 34,
                NeighboursNeighbourId = 17,
                CanalOwnerId = 83
            },
            new CanalOwners(){
                NeighboursRegionId = 34,
                NeighboursNeighbourId = 17,
                CanalOwnerId = 94
            }
        });

        #region GermanLand

        ALandUnit inf1 = LandUnitFactory.CreateInfantry(deutschland, Germany, true);
        ALandUnit inf2 = LandUnitFactory.CreateInfantry(deutschland, Germany, true);
        ALandUnit inf3 = LandUnitFactory.CreateInfantry(deutschland, Germany, true);
        ALandUnit inf4 = LandUnitFactory.CreateInfantry(polen, Germany, true);
        ALandUnit inf5 = LandUnitFactory.CreateInfantry(polen, Germany, true);
        ALandUnit inf6 = LandUnitFactory.CreateInfantry(ukrainischessr, Germany, true);
        ALandUnit inf7 = LandUnitFactory.CreateInfantry(ukrainischessr, Germany, true);
        ALandUnit inf8 = LandUnitFactory.CreateInfantry(ukrainischessr, Germany, true);
        ALandUnit inf9 = LandUnitFactory.CreateInfantry(westrussland, Germany, true);
        ALandUnit inf10 = LandUnitFactory.CreateInfantry(westrussland, Germany, true);
        ALandUnit inf11 = LandUnitFactory.CreateInfantry(westrussland, Germany, true);
        ALandUnit inf12 = LandUnitFactory.CreateInfantry(weissrussland, Germany, true);
        ALandUnit inf13 = LandUnitFactory.CreateInfantry(weissrussland, Germany, true);
        ALandUnit inf14 = LandUnitFactory.CreateInfantry(weissrussland, Germany, true);
        ALandUnit inf15 = LandUnitFactory.CreateInfantry(baltische_staaten, Germany, true);
        ALandUnit inf16 = LandUnitFactory.CreateInfantry(bulgarien_rumänien, Germany, true);
        ALandUnit inf17 = LandUnitFactory.CreateInfantry(bulgarien_rumänien, Germany, true);
        ALandUnit inf18 = LandUnitFactory.CreateInfantry(finnland, Germany, true);
        ALandUnit inf19 = LandUnitFactory.CreateInfantry(finnland, Germany, true);
        ALandUnit inf20 = LandUnitFactory.CreateInfantry(finnland, Germany, true);
        ALandUnit inf21 = LandUnitFactory.CreateInfantry(norwegen, Germany, true);
        ALandUnit inf22 = LandUnitFactory.CreateInfantry(norwegen, Germany, true);
        ALandUnit inf23 = LandUnitFactory.CreateInfantry(nordwesteuropa, Germany, true);
        ALandUnit inf24 = LandUnitFactory.CreateInfantry(frankreich, Germany, true);
        ALandUnit inf25 = LandUnitFactory.CreateInfantry(italien, Germany, true);
        ALandUnit inf26 = LandUnitFactory.CreateInfantry(südeuropa, Germany, true);
        ALandUnit inf27 = LandUnitFactory.CreateInfantry(marokko, Germany, true);
        ALandUnit inf28 = LandUnitFactory.CreateInfantry(algerien, Germany, true);
        ALandUnit inf29 = LandUnitFactory.CreateInfantry(lybien, Germany, true);

        inf1.Id = 1;
        inf2.Id = 2;
        inf3.Id = 3;
        inf4.Id = 4;
        inf5.Id = 5;
        inf6.Id = 6;
        inf7.Id = 7;
        inf8.Id = 8;
        inf9.Id = 9;
        inf10.Id = 10;
        inf11.Id = 11;
        inf12.Id = 12;
        inf13.Id = 13;
        inf14.Id = 14;
        inf15.Id = 15;
        inf16.Id = 16;
        inf17.Id = 17;
        inf18.Id = 18;
        inf19.Id = 19;
        inf20.Id = 20;
        inf21.Id = 21;
        inf22.Id = 22;
        inf23.Id = 23;
        inf24.Id = 24;
        inf25.Id = 25;
        inf26.Id = 26;
        inf27.Id = 27;
        inf28.Id = 28;
        inf29.Id = 29;

        ALandUnit transinf30 = LandUnitFactory.CreateInfantry(null, Germany, true);
        ALandUnit transinf31 = LandUnitFactory.CreateInfantry(null, Germany, true);

        transinf30.Id = 30;
        transinf31.Id = 31;

        ALandUnit pan32 = LandUnitFactory.CreateTank(deutschland, Germany, true);
        ALandUnit pan33 = LandUnitFactory.CreateTank(deutschland, Germany, true);
        ALandUnit pan34 = LandUnitFactory.CreateTank(polen, Germany, true);
        ALandUnit pan35 = LandUnitFactory.CreateTank(ukrainischessr, Germany, true);
        ALandUnit pan36 = LandUnitFactory.CreateTank(westrussland, Germany, true);
        ALandUnit pan37 = LandUnitFactory.CreateTank(baltische_staaten, Germany, true);
        ALandUnit pan38 = LandUnitFactory.CreateTank(bulgarien_rumänien, Germany, true);
        ALandUnit pan39 = LandUnitFactory.CreateTank(nordwesteuropa, Germany, true);
        ALandUnit pan40 = LandUnitFactory.CreateTank(frankreich, Germany, true);
        ALandUnit pan41 = LandUnitFactory.CreateTank(frankreich, Germany, true);
        ALandUnit pan42 = LandUnitFactory.CreateTank(italien, Germany, true);
        ALandUnit pan43 = LandUnitFactory.CreateTank(lybien, Germany, true);

        pan32.Id = 32;
        pan33.Id = 33;
        pan34.Id = 34;
        pan35.Id = 35;
        pan36.Id = 36;
        pan37.Id = 37;
        pan38.Id = 38;
        pan39.Id = 39;
        pan40.Id = 40;
        pan41.Id = 41;
        pan42.Id = 42;
        pan43.Id = 43;

        ALandUnit flak44 = LandUnitFactory.CreateAntiAir(deutschland, Germany, true);
        ALandUnit flak45 = LandUnitFactory.CreateAntiAir(frankreich, Germany, true);
        ALandUnit flak46 = LandUnitFactory.CreateAntiAir(italien, Germany, true);

        flak44.Id = 44;
        flak45.Id = 45;
        flak46.Id = 46;

        ALandUnit art47 = LandUnitFactory.CreateArtillery(algerien, Germany, true);
        ALandUnit art48 = LandUnitFactory.CreateArtillery(südeuropa, Germany, true);
        ALandUnit art49 = LandUnitFactory.CreateArtillery(ukrainischessr, Germany, true);
        ALandUnit art50 = LandUnitFactory.CreateArtillery(westrussland, Germany, true);

        art47.Id = 47;
        art48.Id = 48;
        art49.Id = 49;
        art50.Id = 50;

        #endregion

        #region RussianLand

        ALandUnit inf51 = LandUnitFactory.CreateInfantry(russland, Soviet_Union, true);
        ALandUnit inf52 = LandUnitFactory.CreateInfantry(russland, Soviet_Union, true);
        ALandUnit inf53 = LandUnitFactory.CreateInfantry(russland, Soviet_Union, true);
        ALandUnit inf54 = LandUnitFactory.CreateInfantry(russland, Soviet_Union, true);
        ALandUnit inf55 = LandUnitFactory.CreateInfantry(karelo_finnnischessr, Soviet_Union, true);
        ALandUnit inf56 = LandUnitFactory.CreateInfantry(karelo_finnnischessr, Soviet_Union, true);
        ALandUnit inf57 = LandUnitFactory.CreateInfantry(karelo_finnnischessr, Soviet_Union, true);
        ALandUnit inf58 = LandUnitFactory.CreateInfantry(karelo_finnnischessr, Soviet_Union, true);
        ALandUnit inf59 = LandUnitFactory.CreateInfantry(archangelsk, Soviet_Union, true);
        ALandUnit inf60 = LandUnitFactory.CreateInfantry(kaukasus, Soviet_Union, true);
        ALandUnit inf61 = LandUnitFactory.CreateInfantry(kaukasus, Soviet_Union, true);
        ALandUnit inf62 = LandUnitFactory.CreateInfantry(kaukasus, Soviet_Union, true);
        ALandUnit inf63 = LandUnitFactory.CreateInfantry(kasachischessr, Soviet_Union, true);
        ALandUnit inf64 = LandUnitFactory.CreateInfantry(nowosibirsk, Soviet_Union, true);
        ALandUnit inf65 = LandUnitFactory.CreateInfantry(autonomer_kreis_der_ewenken, Soviet_Union, true);
        ALandUnit inf66 = LandUnitFactory.CreateInfantry(autonomer_kreis_der_ewenken, Soviet_Union, true);
        ALandUnit inf67 = LandUnitFactory.CreateInfantry(jakutischessr, Soviet_Union, true);
        ALandUnit inf68 = LandUnitFactory.CreateInfantry(burjatischessr, Soviet_Union, true);
        ALandUnit inf69 = LandUnitFactory.CreateInfantry(burjatischessr, Soviet_Union, true);
        ALandUnit inf70 = LandUnitFactory.CreateInfantry(sowjetischer_ferner_osten, Soviet_Union, true);
        ALandUnit inf71 = LandUnitFactory.CreateInfantry(sowjetischer_ferner_osten, Soviet_Union, true);

        inf51.Id = 51;
        inf52.Id = 52;
        inf53.Id = 53;
        inf54.Id = 54;
        inf55.Id = 55;
        inf56.Id = 56;
        inf57.Id = 57;
        inf58.Id = 58;
        inf59.Id = 59;
        inf60.Id = 60;
        inf61.Id = 61;
        inf62.Id = 62;
        inf63.Id = 63;
        inf64.Id = 64;
        inf65.Id = 65;
        inf66.Id = 66;
        inf67.Id = 67;
        inf68.Id = 68;
        inf69.Id = 69;
        inf70.Id = 70;
        inf71.Id = 71;

        ALandUnit pan72 = LandUnitFactory.CreateTank(russland, Soviet_Union, true);
        ALandUnit pan73 = LandUnitFactory.CreateTank(russland, Soviet_Union, true);
        ALandUnit pan74 = LandUnitFactory.CreateTank(kaukasus, Soviet_Union, true);
        ALandUnit pan75 = LandUnitFactory.CreateTank(archangelsk, Soviet_Union, true);

        pan72.Id = 72;
        pan73.Id = 73;
        pan74.Id = 74;
        pan75.Id = 75;

        ALandUnit flak76 = LandUnitFactory.CreateAntiAir(russland, Soviet_Union, true);
        ALandUnit flak77 = LandUnitFactory.CreateAntiAir(kaukasus, Soviet_Union, true);

        flak76.Id = 76;
        flak77.Id = 77;

        ALandUnit art78 = LandUnitFactory.CreateArtillery(russland, Soviet_Union, true);
        ALandUnit art79 = LandUnitFactory.CreateArtillery(karelo_finnnischessr, Soviet_Union, true);
        ALandUnit art80 = LandUnitFactory.CreateArtillery(kaukasus, Soviet_Union, true);

        art78.Id = 78;
        art79.Id = 79;
        art80.Id = 80;

        #endregion

        #region BritishLand

        ALandUnit inf81 = LandUnitFactory.CreateInfantry(vereinigtes_königreich, United_Kingdom, true);
        ALandUnit inf82 = LandUnitFactory.CreateInfantry(vereinigtes_königreich, United_Kingdom, true);
        ALandUnit inf83 = LandUnitFactory.CreateInfantry(ägypten, United_Kingdom, true);
        ALandUnit inf84 = LandUnitFactory.CreateInfantry(transjordanien, United_Kingdom, true);
        ALandUnit inf85 = LandUnitFactory.CreateInfantry(persien, United_Kingdom, true);
        ALandUnit inf86 = LandUnitFactory.CreateInfantry(südafrikanische_union, United_Kingdom, true);
        ALandUnit inf87 = LandUnitFactory.CreateInfantry(indien, United_Kingdom, true);
        ALandUnit inf88 = LandUnitFactory.CreateInfantry(indien, United_Kingdom, true);
        ALandUnit inf89 = LandUnitFactory.CreateInfantry(indien, United_Kingdom, true);
        ALandUnit inf90 = LandUnitFactory.CreateInfantry(burma, United_Kingdom, true);
        ALandUnit inf91 = LandUnitFactory.CreateInfantry(ostaustralien, United_Kingdom, true);
        ALandUnit inf92 = LandUnitFactory.CreateInfantry(ostaustralien, United_Kingdom, true);
        ALandUnit inf93 = LandUnitFactory.CreateInfantry(westaustralien, United_Kingdom, true);
        ALandUnit inf94 = LandUnitFactory.CreateInfantry(neuseeland, United_Kingdom, true);
        ALandUnit inf95 = LandUnitFactory.CreateInfantry(westkanada, United_Kingdom, true);

        inf81.Id = 81;
        inf82.Id = 82;
        inf83.Id = 83;
        inf84.Id = 84;
        inf85.Id = 85;
        inf86.Id = 86;
        inf87.Id = 87;
        inf88.Id = 88;
        inf89.Id = 89;
        inf90.Id = 90;
        inf91.Id = 91;
        inf92.Id = 92;
        inf93.Id = 93;
        inf94.Id = 94;
        inf95.Id = 95;

        ALandUnit transinf96 = LandUnitFactory.CreateInfantry(null, United_Kingdom, true);
        ALandUnit transinf97 = LandUnitFactory.CreateInfantry(null, United_Kingdom, true);
        ALandUnit transinf98 = LandUnitFactory.CreateInfantry(null, United_Kingdom, true);
        ALandUnit transinf99 = LandUnitFactory.CreateInfantry(null, United_Kingdom, true);

        transinf96.Id = 96;
        transinf97.Id = 97;
        transinf98.Id = 98;
        transinf99.Id = 99;

        ALandUnit pan100 = LandUnitFactory.CreateTank(vereinigtes_königreich, United_Kingdom, true);
        ALandUnit pan101 = LandUnitFactory.CreateTank(ostkanada, United_Kingdom, true);
        ALandUnit pan102 = LandUnitFactory.CreateTank(ägypten, United_Kingdom, true);

        pan100.Id = 100;
        pan101.Id = 101;
        pan102.Id = 102;

        ALandUnit flak103 = LandUnitFactory.CreateAntiAir(vereinigtes_königreich, United_Kingdom, true);
        ALandUnit flak104 = LandUnitFactory.CreateAntiAir(indien, United_Kingdom, true);

        flak103.Id = 103;
        flak104.Id = 104;

        ALandUnit art105 = LandUnitFactory.CreateArtillery(vereinigtes_königreich, United_Kingdom, true);
        ALandUnit art106 = LandUnitFactory.CreateArtillery(ägypten, United_Kingdom, true);

        art105.Id = 105;
        art106.Id = 106;

        #endregion

        #region USALand

        ALandUnit inf107 = LandUnitFactory.CreateInfantry(östliche_vereinigte_staaten, United_States, true);
        ALandUnit inf108 = LandUnitFactory.CreateInfantry(östliche_vereinigte_staaten, United_States, true);
        ALandUnit inf109 = LandUnitFactory.CreateInfantry(zentrale_vereinigte_staaten, United_States, true);
        ALandUnit inf110 =
            LandUnitFactory.CreateInfantry(westliche_vereinigte_staaten, United_States, true);
        ALandUnit inf111 =
            LandUnitFactory.CreateInfantry(westliche_vereinigte_staaten, United_States, true);
        ALandUnit inf112 = LandUnitFactory.CreateInfantry(alaska, United_States, true);
        ALandUnit inf113 = LandUnitFactory.CreateInfantry(midway_atoll, United_States, true);
        ALandUnit inf114 = LandUnitFactory.CreateInfantry(hawaii_inseln, United_States, true);
        ALandUnit inf115 = LandUnitFactory.CreateInfantry(anhwei, United_States, true);
        ALandUnit inf116 = LandUnitFactory.CreateInfantry(anhwei, United_States, true);
        ALandUnit inf117 = LandUnitFactory.CreateInfantry(sezuan, United_States, true);
        ALandUnit inf118 = LandUnitFactory.CreateInfantry(sezuan, United_States, true);
        ALandUnit inf119 = LandUnitFactory.CreateInfantry(yunnan, United_States, true);
        ALandUnit inf120 = LandUnitFactory.CreateInfantry(yunnan, United_States, true);

        inf107.Id = 107;
        inf108.Id = 108;
        inf109.Id = 109;
        inf110.Id = 110;
        inf111.Id = 111;
        inf112.Id = 112;
        inf113.Id = 113;
        inf114.Id = 114;
        inf115.Id = 115;
        inf116.Id = 116;
        inf117.Id = 117;
        inf118.Id = 118;
        inf119.Id = 119;
        inf120.Id = 120;

        ALandUnit transinf121 = LandUnitFactory.CreateInfantry(null, United_States, true);
        ALandUnit transinf122 = LandUnitFactory.CreateInfantry(null, United_States, true);
        ALandUnit transinf123 = LandUnitFactory.CreateInfantry(null, United_States, true);

        transinf121.Id = 121;
        transinf122.Id = 122;
        transinf123.Id = 123;

        ALandUnit pan124 = LandUnitFactory.CreateTank(östliche_vereinigte_staaten, United_States, true);

        pan124.Id = 124;

        ALandUnit flak125 =
            LandUnitFactory.CreateAntiAir(östliche_vereinigte_staaten, United_States, true);
        ALandUnit flak126 =
            LandUnitFactory.CreateAntiAir(westliche_vereinigte_staaten, United_States, true);

        flak125.Id = 125;
        flak126.Id = 126;

        ALandUnit art127 =
            LandUnitFactory.CreateArtillery(östliche_vereinigte_staaten, United_States, true);

        art127.Id = 127;

        #endregion

        #region JapanLand

        ALandUnit inf128 = LandUnitFactory.CreateInfantry(japan, Japan, true);
        ALandUnit inf129 = LandUnitFactory.CreateInfantry(japan, Japan, true);
        ALandUnit inf130 = LandUnitFactory.CreateInfantry(japan, Japan, true);
        ALandUnit inf131 = LandUnitFactory.CreateInfantry(japan, Japan, true);
        ALandUnit inf132 = LandUnitFactory.CreateInfantry(wake, Japan, true);
        ALandUnit inf133 = LandUnitFactory.CreateInfantry(iwojima, Japan, true);
        ALandUnit inf134 = LandUnitFactory.CreateInfantry(okinawa, Japan, true);
        ALandUnit inf135 = LandUnitFactory.CreateInfantry(caroline_atoll, Japan, true);
        ALandUnit inf136 = LandUnitFactory.CreateInfantry(philippinische_inseln, Japan, true);
        ALandUnit inf137 = LandUnitFactory.CreateInfantry(jiangsu, Japan, true);
        ALandUnit inf138 = LandUnitFactory.CreateInfantry(jiangsu, Japan, true);
        ALandUnit inf139 = LandUnitFactory.CreateInfantry(jiangsu, Japan, true);
        ALandUnit inf140 = LandUnitFactory.CreateInfantry(jiangsu, Japan, true);
        ALandUnit inf141 = LandUnitFactory.CreateInfantry(mandschurei, Japan, true);
        ALandUnit inf142 = LandUnitFactory.CreateInfantry(mandschurei, Japan, true);
        ALandUnit inf143 = LandUnitFactory.CreateInfantry(mandschurei, Japan, true);
        ALandUnit inf144 = LandUnitFactory.CreateInfantry(guandong, Japan, true);
        ALandUnit inf145 = LandUnitFactory.CreateInfantry(französisch_indochina_thailand, Japan, true);
        ALandUnit inf146 = LandUnitFactory.CreateInfantry(französisch_indochina_thailand, Japan, true);
        ALandUnit inf147 = LandUnitFactory.CreateInfantry(malaysia, Japan, true);
        ALandUnit inf148 = LandUnitFactory.CreateInfantry(ostindien, Japan, true);
        ALandUnit inf149 = LandUnitFactory.CreateInfantry(ostindien, Japan, true);
        ALandUnit inf150 = LandUnitFactory.CreateInfantry(borneo, Japan, true);
        ALandUnit inf151 = LandUnitFactory.CreateInfantry(neuguniea, Japan, true);
        ALandUnit inf152 = LandUnitFactory.CreateInfantry(salomon_inseln, Japan, true);

        inf128.Id = 128;
        inf129.Id = 129;
        inf130.Id = 130;
        inf131.Id = 131;
        inf132.Id = 132;
        inf133.Id = 133;
        inf134.Id = 134;
        inf135.Id = 135;
        inf136.Id = 136;
        inf137.Id = 137;
        inf138.Id = 138;
        inf139.Id = 139;
        inf140.Id = 140;
        inf141.Id = 141;
        inf142.Id = 142;
        inf143.Id = 143;
        inf144.Id = 144;
        inf145.Id = 145;
        inf146.Id = 146;
        inf147.Id = 147;
        inf148.Id = 148;
        inf149.Id = 149;
        inf150.Id = 150;
        inf151.Id = 151;
        inf152.Id = 152;

        ALandUnit transinf153 = LandUnitFactory.CreateInfantry(null, Japan, true);
        ALandUnit transinf154 = LandUnitFactory.CreateInfantry(null, Japan, true);

        transinf153.Id = 153;
        transinf154.Id = 154;

        ALandUnit pan155 = LandUnitFactory.CreateTank(japan, Japan, true);

        pan155.Id = 155;

        ALandUnit flak156 = LandUnitFactory.CreateAntiAir(japan, Japan, true);

        flak156.Id = 156;

        ALandUnit art157 = LandUnitFactory.CreateArtillery(japan, Japan, true);
        ALandUnit art158 = LandUnitFactory.CreateArtillery(guandong, Japan, true);
        ALandUnit art159 = LandUnitFactory.CreateArtillery(französisch_indochina_thailand, Japan, true);
        ALandUnit art160 = LandUnitFactory.CreateArtillery(philippinische_inseln, Japan, true);

        art157.Id = 157;
        art158.Id = 158;
        art159.Id = 159;
        art160.Id = 160;

        #endregion

        #region GermanPlanes

        APlane figh161 = PlaneFactory.CreateFighter(deutschland, Germany, true);
        APlane figh162 = PlaneFactory.CreateFighter(nordwesteuropa, Germany, true);
        APlane figh163 = PlaneFactory.CreateFighter(polen, Germany, true);
        APlane figh164 = PlaneFactory.CreateFighter(ukrainischessr, Germany, true);
        APlane figh165 = PlaneFactory.CreateFighter(bulgarien_rumänien, Germany, true);
        APlane figh166 = PlaneFactory.CreateFighter(norwegen, Germany, true);

        figh161.Id = 161;
        figh162.Id = 162;
        figh163.Id = 163;
        figh164.Id = 164;
        figh165.Id = 165;
        figh166.Id = 166;

        APlane bomb167 = PlaneFactory.CreateBomber(deutschland, Germany, true);

        bomb167.Id = 167;

        #endregion

        #region RussianPlanes

        APlane figh168 = PlaneFactory.CreateFighter(russland, Soviet_Union, true);
        APlane figh169 = PlaneFactory.CreateFighter(karelo_finnnischessr, Soviet_Union, true);

        figh168.Id = 168;
        figh169.Id = 169;

        #endregion

        #region BritishPlanes

        APlane figh170 = PlaneFactory.CreateFighter(vereinigtes_königreich, United_Kingdom, true);
        APlane figh171 = PlaneFactory.CreateFighter(vereinigtes_königreich, United_Kingdom, true);
        APlane figh172 = PlaneFactory.CreateFighter(ägypten, United_Kingdom, true);
        APlane figh173 = PlaneFactory.CreateFighter(see35, United_Kingdom, true);

        figh170.Id = 170;
        figh171.Id = 171;
        figh172.Id = 172;
        figh173.Id = 173;

        APlane bomb174 = PlaneFactory.CreateBomber(vereinigtes_königreich, United_Kingdom, true);

        bomb174.Id = 174;

        #endregion

        #region USAPlanes

        APlane figh175 = PlaneFactory.CreateFighter(östliche_vereinigte_staaten, United_States, true);
        APlane figh176 = PlaneFactory.CreateFighter(westliche_vereinigte_staaten, United_States, true);
        APlane figh177 = PlaneFactory.CreateFighter(hawaii_inseln, United_States, true);
        APlane figh178 = PlaneFactory.CreateFighter(see53, United_States, true);
        APlane figh179 = PlaneFactory.CreateFighter(sezuan, United_States, true);

        figh175.Id = 175;
        figh176.Id = 176;
        figh177.Id = 177;
        figh178.Id = 178;
        figh179.Id = 179;

        APlane bomb180 = PlaneFactory.CreateBomber(östliche_vereinigte_staaten, United_States, true);

        bomb180.Id = 180;

        #endregion

        #region JapanPlanes

        APlane figh181 = PlaneFactory.CreateFighter(japan, Japan, true);
        APlane figh182 = PlaneFactory.CreateFighter(mandschurei, Japan, true);
        APlane figh183 = PlaneFactory.CreateFighter(französisch_indochina_thailand, Japan, true);
        APlane figh184 = PlaneFactory.CreateFighter(see37, Japan, true);
        APlane figh185 = PlaneFactory.CreateFighter(see37, Japan, true);
        APlane figh186 = PlaneFactory.CreateFighter(see50, Japan, true);

        figh181.Id = 181;
        figh182.Id = 182;
        figh183.Id = 183;
        figh184.Id = 184;
        figh185.Id = 185;
        figh186.Id = 186;

        APlane bomb187 = PlaneFactory.CreateBomber(japan, Japan, true);

        bomb187.Id = 187;

        #endregion

        #region GermanShips

        AShip sub188 = ShipFactory.CreateSubmarine(see9, Germany, true);
        AShip sub189 = ShipFactory.CreateSubmarine(see9, Germany, true);
        AShip sub190 = ShipFactory.CreateSubmarine(see5, Germany, true);
        AShip sub191 = ShipFactory.CreateSubmarine(see5, Germany, true);

        sub188.Id = 188;
        sub189.Id = 189;
        sub190.Id = 190;
        sub191.Id = 191;

        AShip crs192 = ShipFactory.CreateCruiser(see5, Germany, true);

        crs192.Id = 192;

        AShip bat193 = ShipFactory.CreateBattleship(see15, Germany, true);

        bat193.Id = 193;

        AShip t1 = ShipFactory.CreateTransport(see5, Germany, true);
        AShip t2 = ShipFactory.CreateTransport(see15, Germany, true);

        Transport tra194 = (Transport)t1;
        Transport tra195 = (Transport)t2;

        tra194.Id = 194;
        tra195.Id = 195;
        transinf30.TransportId = 194;
        transinf31.TransportId = 195;

        #endregion

        #region RussianShips

        AShip sub196 = ShipFactory.CreateSubmarine(see4, Soviet_Union, true);

        sub196.Id = 196;

        #endregion

        #region BritishShips

        AShip crs197 = ShipFactory.CreateCruiser(see14, United_Kingdom, true);
        AShip crs198 = ShipFactory.CreateCruiser(see35, United_Kingdom, true);
        AShip crs199 = ShipFactory.CreateCruiser(see39, United_Kingdom, true);

        crs197.Id = 197;
        crs198.Id = 198;
        crs199.Id = 199;

        AShip des200 = ShipFactory.CreateDestroyer(see10, United_Kingdom, true);
        AShip des201 = ShipFactory.CreateDestroyer(see17, United_Kingdom, true);

        des200.Id = 200;
        des201.Id = 201;

        AShip sub202 = ShipFactory.CreateSubmarine(see39, United_Kingdom, true);

        sub202.Id = 202;

        AShip bat203 = ShipFactory.CreateBattleship(see7, United_Kingdom, true);

        bat203.Id = 203;

        AShip t3 = ShipFactory.CreateTransport(see10, United_Kingdom, true);
        AShip t4 = ShipFactory.CreateTransport(see7, United_Kingdom, true);
        AShip t5 = ShipFactory.CreateTransport(see35, United_Kingdom, true);
        AShip t6 = ShipFactory.CreateTransport(see39, United_Kingdom, true);

        Transport tra204 = (Transport)t3;
        Transport tra205 = (Transport)t4;
        Transport tra206 = (Transport)t5;
        Transport tra207 = (Transport)t6;

        tra204.Id = 204;
        tra205.Id = 205;
        tra206.Id = 206;
        tra207.Id = 207;

        transinf96.TransportId = 204;
        transinf97.TransportId = 205;
        transinf98.TransportId = 206;
        transinf99.TransportId = 207;

        AShip a1 = ShipFactory.CreateAircraftCarrier(see35, United_Kingdom, true);

        AircraftCarrier air208 = (AircraftCarrier)a1;

        air208.Id = 208;

        figh173.AircraftCarrierId = 208;

        #endregion

        #region USAShips

        AShip sub209 = ShipFactory.CreateSubmarine(see53, United_States, true);

        sub209.Id = 209;

        AShip des210 = ShipFactory.CreateDestroyer(see53, United_States, true);
        AShip des211 = ShipFactory.CreateDestroyer(see56, United_States, true);
        AShip des212 = ShipFactory.CreateDestroyer(see11, United_States, true);

        des210.Id = 210;
        des211.Id = 211;
        des212.Id = 212;

        AShip crs213 = ShipFactory.CreateCruiser(see19, United_States, true);

        crs213.Id = 213;

        AShip bat214 = ShipFactory.CreateBattleship(see56, United_States, true);

        bat214.Id = 214;

        AShip t7 = ShipFactory.CreateTransport(see56, United_States, true);
        AShip t8 = ShipFactory.CreateTransport(see11, United_States, true);
        AShip t9 = ShipFactory.CreateTransport(see11, United_States, true);

        Transport tra215 = (Transport)t7;
        Transport tra216 = (Transport)t8;
        Transport tra217 = (Transport)t9;

        tra215.Id = 215;
        tra216.Id = 216;
        tra217.Id = 217;

        transinf121.TransportId = 215;
        transinf122.TransportId = 216;
        transinf123.TransportId = 217;

        AShip a2 = ShipFactory.CreateAircraftCarrier(see53, United_States, true);

        AircraftCarrier air218 = (AircraftCarrier)a2;

        air218.Id = 218;

        figh178.AircraftCarrierId = 218;

        #endregion

        #region JapanShips

        AShip sub219 = ShipFactory.CreateSubmarine(see44, Japan, true);

        sub219.Id = 219;

        AShip crs220 = ShipFactory.CreateCruiser(see50, Japan, true);

        crs220.Id = 220;

        AShip des221 = ShipFactory.CreateDestroyer(see60, Japan, true);
        AShip des222 = ShipFactory.CreateDestroyer(see61, Japan, true);

        des221.Id = 221;
        des222.Id = 222;

        AShip bat223 = ShipFactory.CreateBattleship(see60, Japan, true);
        AShip bat224 = ShipFactory.CreateBattleship(see37, Japan, true);

        bat223.Id = 223;
        bat224.Id = 224;

        AShip t10 = ShipFactory.CreateTransport(see60, Japan, true);
        AShip t11 = ShipFactory.CreateTransport(see61, Japan, true);

        Transport tra225 = (Transport)t10;
        Transport tra226 = (Transport)t11;

        tra225.Id = 225;
        tra226.Id = 226;

        transinf153.TransportId = 225;
        transinf154.TransportId = 226;

        AShip a3 = ShipFactory.CreateAircraftCarrier(see50, Japan, true);
        AShip a4 = ShipFactory.CreateAircraftCarrier(see37, Japan, true);

        AircraftCarrier air227 = (AircraftCarrier)a3;
        AircraftCarrier air228 = (AircraftCarrier)a4;

        air227.Id = 227;
        air228.Id = 228;

        figh186.AircraftCarrierId = 227;
        figh184.AircraftCarrierId = 228;
        figh185.AircraftCarrierId = 228;

        #endregion

        builder.Entity<Battleship>().HasData(new List<Battleship>(){
            (Battleship)bat193,
            (Battleship)bat203,
            (Battleship)bat214,
            (Battleship)bat223,
            (Battleship)bat224
        });

        builder.Entity<Cruiser>().HasData(new List<Cruiser>(){
            (Cruiser)crs192,
            (Cruiser)crs197,
            (Cruiser)crs198,
            (Cruiser)crs199,
            (Cruiser)crs213,
            (Cruiser)crs220
        });

        builder.Entity<Destroyer>().HasData(new List<Destroyer>(){
            (Destroyer)des200,
            (Destroyer)des201,
            (Destroyer)des210,
            (Destroyer)des211,
            (Destroyer)des212,
            (Destroyer)des221,
            (Destroyer)des222
        });

        builder.Entity<Submarine>().HasData(new List<Submarine>(){
            (Submarine)sub188,
            (Submarine)sub189,
            (Submarine)sub190,
            (Submarine)sub191,
            (Submarine)sub196,
            (Submarine)sub202,
            (Submarine)sub209,
            (Submarine)sub219
        });

        builder.Entity<Transport>().HasData(new List<Transport>(){
            tra194,
            tra195,
            tra204,
            tra205,
            tra206,
            tra207,
            tra215,
            tra216,
            tra217,
            tra225,
            tra226
        });

        builder.Entity<AircraftCarrier>().HasData(new List<AircraftCarrier>(){
            air208,
            air218,
            air227,
            air228
        });

        builder.Entity<Infantry>().HasData(new List<Infantry>(){
            (Infantry)inf1,
            (Infantry)inf2,
            (Infantry)inf3,
            (Infantry)inf4,
            (Infantry)inf5,
            (Infantry)inf6,
            (Infantry)inf7,
            (Infantry)inf8,
            (Infantry)inf9,
            (Infantry)inf10,
            (Infantry)inf11,
            (Infantry)inf12,
            (Infantry)inf13,
            (Infantry)inf14,
            (Infantry)inf15,
            (Infantry)inf16,
            (Infantry)inf17,
            (Infantry)inf18,
            (Infantry)inf19,
            (Infantry)inf20,
            (Infantry)inf21,
            (Infantry)inf22,
            (Infantry)inf23,
            (Infantry)inf24,
            (Infantry)inf25,
            (Infantry)inf26,
            (Infantry)inf27,
            (Infantry)inf28,
            (Infantry)inf29,
            (Infantry)transinf30,
            (Infantry)transinf31,
            (Infantry)inf51,
            (Infantry)inf52,
            (Infantry)inf53,
            (Infantry)inf54,
            (Infantry)inf55,
            (Infantry)inf56,
            (Infantry)inf57,
            (Infantry)inf58,
            (Infantry)inf59,
            (Infantry)inf60,
            (Infantry)inf61,
            (Infantry)inf62,
            (Infantry)inf63,
            (Infantry)inf64,
            (Infantry)inf65,
            (Infantry)inf66,
            (Infantry)inf67,
            (Infantry)inf68,
            (Infantry)inf69,
            (Infantry)inf70,
            (Infantry)inf71,
            (Infantry)inf81,
            (Infantry)inf82,
            (Infantry)inf83,
            (Infantry)inf84,
            (Infantry)inf85,
            (Infantry)inf86,
            (Infantry)inf87,
            (Infantry)inf88,
            (Infantry)inf89,
            (Infantry)inf90,
            (Infantry)inf91,
            (Infantry)inf92,
            (Infantry)inf93,
            (Infantry)inf94,
            (Infantry)inf95,
            (Infantry)transinf96,
            (Infantry)transinf97,
            (Infantry)transinf98,
            (Infantry)transinf99,
            (Infantry)inf107,
            (Infantry)inf108,
            (Infantry)inf109,
            (Infantry)inf110,
            (Infantry)inf111,
            (Infantry)inf112,
            (Infantry)inf113,
            (Infantry)inf114,
            (Infantry)inf115,
            (Infantry)inf116,
            (Infantry)inf117,
            (Infantry)inf118,
            (Infantry)inf119,
            (Infantry)inf120,
            (Infantry)transinf121,
            (Infantry)transinf122,
            (Infantry)transinf123,
            (Infantry)inf128,
            (Infantry)inf129,
            (Infantry)inf130,
            (Infantry)inf131,
            (Infantry)inf132,
            (Infantry)inf133,
            (Infantry)inf134,
            (Infantry)inf135,
            (Infantry)inf136,
            (Infantry)inf137,
            (Infantry)inf138,
            (Infantry)inf139,
            (Infantry)inf140,
            (Infantry)inf141,
            (Infantry)inf142,
            (Infantry)inf143,
            (Infantry)inf144,
            (Infantry)inf145,
            (Infantry)inf146,
            (Infantry)inf147,
            (Infantry)inf148,
            (Infantry)inf149,
            (Infantry)inf150,
            (Infantry)inf151,
            (Infantry)inf152,
            (Infantry)transinf153,
            (Infantry)transinf154
        });

        builder.Entity<Tank>().HasData(new List<Tank>(){
            (Tank)pan32,
            (Tank)pan33,
            (Tank)pan34,
            (Tank)pan35,
            (Tank)pan36,
            (Tank)pan37,
            (Tank)pan38,
            (Tank)pan39,
            (Tank)pan40,
            (Tank)pan41,
            (Tank)pan42,
            (Tank)pan43,
            (Tank)pan72,
            (Tank)pan73,
            (Tank)pan74,
            (Tank)pan75,
            (Tank)pan100,
            (Tank)pan101,
            (Tank)pan102,
            (Tank)pan124,
            (Tank)pan155
        });

        builder.Entity<Artillery>().HasData(new List<Artillery>(){
            (Artillery)art47,
            (Artillery)art48,
            (Artillery)art49,
            (Artillery)art50,
            (Artillery)art78,
            (Artillery)art79,
            (Artillery)art80,
            (Artillery)art105,
            (Artillery)art106,
            (Artillery)art127,
            (Artillery)art157,
            (Artillery)art158,
            (Artillery)art159,
            (Artillery)art160
        });

        builder.Entity<AntiAir>().HasData(new List<AntiAir>(){
            (AntiAir)flak44,
            (AntiAir)flak45,
            (AntiAir)flak46,
            (AntiAir)flak76,
            (AntiAir)flak77,
            (AntiAir)flak103,
            (AntiAir)flak104,
            (AntiAir)flak125,
            (AntiAir)flak126,
            (AntiAir)flak156
        });

        builder.Entity<Fighter>().HasData(new List<Fighter>(){
            (Fighter)figh161,
            (Fighter)figh162,
            (Fighter)figh163,
            (Fighter)figh164,
            (Fighter)figh165,
            (Fighter)figh166,
            (Fighter)figh168,
            (Fighter)figh169,
            (Fighter)figh170,
            (Fighter)figh171,
            (Fighter)figh172,
            (Fighter)figh173,
            (Fighter)figh175,
            (Fighter)figh176,
            (Fighter)figh177,
            (Fighter)figh178,
            (Fighter)figh179,
            (Fighter)figh181,
            (Fighter)figh182,
            (Fighter)figh183,
            (Fighter)figh184,
            (Fighter)figh185,
            (Fighter)figh186
        });

        builder.Entity<Bomber>().HasData(new List<Bomber>(){
            (Bomber)bomb167,
            (Bomber)bomb174,
            (Bomber)bomb180,
            (Bomber)bomb187
        });

        builder.Entity<SessionInfo>().HasData(new SessionInfo(){
            Id = 1,
            CurrentNationId = 3,
            StandardVictory = true,
            TotalVictory = false,
            Phase = EPhase.PurchaseUnits,
            Round = 1,
            DiceMode = EDiceMode.STANDARD,
            AxisCapitals = 6,
            AlliesCapitals = 6,
        });

        base.OnModelCreating(builder);
    }
}