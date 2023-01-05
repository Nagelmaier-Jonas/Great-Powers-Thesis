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
    public DbSet<LandUnit> LandUnits{ get; set; }
    public DbSet<Ship> Ships{ get; set; }
    public DbSet<AircraftCarrier> AircraftCarrier{ get; set; }
    public DbSet<Transport> Transporter{ get; set; }
    public DbSet<Plane> Planes{ get; set; }
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

        builder.Entity<AUnit>().Property(a => a.Type).HasConversion<string>();
        builder.Entity<AUnit>().HasOne(u => u.Nation).WithMany(n => n.Units);

        builder.Entity<LandUnit>().HasOne(n => n.Transport)
            .WithMany(u => u.Units);
        builder.Entity<LandUnit>().HasOne(n => n.Region)
            .WithMany(u => u.StationedUnits);
        builder.Entity<LandUnit>().HasOne(n => n.Target)
            .WithMany(u => u.IncomingUnits);

        builder.Entity<Plane>().HasOne(n => n.AircraftCarrier)
            .WithMany(u => u.Planes);
        builder.Entity<Plane>().HasOne(n => n.Region)
            .WithMany(u => u.StationedPlanes);
        builder.Entity<Plane>().HasOne(n => n.Target)
            .WithMany(u => u.IncomingPlanes);

        builder.Entity<Ship>().HasOne(s => s.Region)
            .WithMany(w => w.StationedShips);
        builder.Entity<Ship>().HasOne(s => s.Target)
            .WithMany(w => w.IncomingShips);

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
                Id = 1,
                Damage = 0,
                RegionId = 66
            },
            new Factory(){
                Id = 2,
                Damage = 0,
                RegionId = 74
            },
            new Factory(){
                Id = 3,
                Damage = 0,
                RegionId = 77
            },
            new Factory(){
                Id = 4,
                Damage = 0,
                RegionId = 84
            },
            new Factory(){
                Id = 5,
                Damage = 0,
                RegionId = 110
            },
            new Factory(){
                Id = 6,
                Damage = 0,
                RegionId = 115
            },
            new Factory(){
                Id = 7,
                Damage = 0,
                RegionId = 117
            },
            new Factory(){
                Id = 8,
                Damage = 0,
                RegionId = 86
            },
            new Factory(){
                Id = 9,
                Damage = 0,
                RegionId = 96
            },
            new Factory(){
                Id = 10,
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
            
        };
        LandRegion polen = new LandRegion(){
            Id = 67,
            Income = 2,
            Name = "Polen",
            NationId = 1,
            Identifier = ERegion.Poland,
            
        };
        LandRegion baltische_staaten = new LandRegion(){
            Id = 68,
            Income = 2,
            Name = "Baltische Staaten",
            NationId = 1,
            Identifier = ERegion.BalticStates,
            
        };
        LandRegion weissrussland = new LandRegion(){
            Id = 69,
            Income = 2,
            Name = "Weissrussland",
            NationId = 1,
            Identifier = ERegion.WhiteRussia,
            
        };
        LandRegion ukrainischessr = new LandRegion(){
            Id = 70,
            Income = 2,
            Name = "Ukrainische SSR",
            NationId = 1,
            Identifier = ERegion.Ukraine,
            
        };
        LandRegion westrussland = new LandRegion(){
            Id = 71,
            Income = 2,
            Name = "West Russland",
            NationId = 1,
            Identifier = ERegion.WestRussia,
            
        };
        LandRegion finnland = new LandRegion(){
            Id = 72,
            Income = 1,
            Name = "Finnland",
            NationId = 1,
            Identifier = ERegion.Finland,
            
        };
        LandRegion norwegen = new LandRegion(){
            Id = 73,
            Income = 2,
            Name = "Norwegen",
            NationId = 1,
            Identifier = ERegion.Norway,
            
        };
        LandRegion bulgarien_rumänien = new LandRegion(){
            Id = 75,
            Income = 2,
            Name = "Bulgarien Rumänien",
            NationId = 1,
            Identifier = ERegion.BulgariaRomania,
            
        };
        LandRegion südeuropa = new LandRegion(){
            Id = 76,
            Income = 2,
            Name = "Südeuropa",
            NationId = 1,
            Identifier = ERegion.SouthEurope,
            
        };
        LandRegion italien = new LandRegion(){
            Id = 77,
            Income = 3,
            Name = "Italien",
            NationId = 1,
            Identifier = ERegion.Italy,
            
        };
        LandRegion frankreich = new LandRegion(){
            Id = 78,
            Income = 6,
            Name = "Frankreich",
            NationId = 1,
            Identifier = ERegion.France,
            
        };
        LandRegion nordwesteuropa = new LandRegion(){
            Id = 79,
            Income = 2,
            Name = "Nordwesteuropa",
            NationId = 1,
            Identifier = ERegion.NorthWestEurope,
            
        };
        LandRegion marokko = new LandRegion(){
            Id = 80,
            Income = 1,
            Name = "Marokko",
            NationId = 1,
            Identifier = ERegion.Morocco,
            
        };
        LandRegion algerien = new LandRegion(){
            Id = 81,
            Income = 1,
            Name = "Algerien",
            NationId = 1,
            Identifier = ERegion.Algeria,
            
        };
        LandRegion lybien = new LandRegion(){
            Id = 82,
            Income = 1,
            Name = "Libyen",
            NationId = 1,
            Identifier = ERegion.Libya,
            
        };

        #endregion

        #region Russland

        LandRegion kaukasus = new LandRegion(){
            Id = 74,
            Income = 4,
            Name = "Kaukasus",
            NationId = 3,
            Identifier = ERegion.Caucasus,
            
        };
        LandRegion karelo_finnnischessr = new LandRegion(){
            Id = 84,
            Income = 2,
            Name = "Karelo-Finnische SSR",
            NationId = 3,
            Identifier = ERegion.Karelia,
            
        };
        LandRegion archangelsk = new LandRegion(){
            Id = 85,
            Income = 1,
            Name = "Archangelsk",
            NationId = 3,
            Identifier = ERegion.Archangelsk,
            
        };
        LandRegion russland = new LandRegion(){
            Id = 86,
            Income = 8,
            Name = "Russland",
            NationId = 3,
            Identifier = ERegion.Russia,
            
        };
        LandRegion autonomer_kreis_der_ewenken = new LandRegion(){
            Id = 87,
            Income = 1,
            Name = "Autonomer Kreis der Ewenken",
            NationId = 3,
            Identifier = ERegion.EwenkiAutonomousDistrict,
            
        };
        LandRegion wologda = new LandRegion(){
            Id = 88,
            Income = 2,
            Name = "Wologda",
            NationId = 3,
            Identifier = ERegion.Vologda,
            
        };
        LandRegion nowosibirsk = new LandRegion(){
            Id = 89,
            Income = 1,
            Name = "Nowosibirsk",
            NationId = 3,
            Identifier = ERegion.Novosibirsk,
            
        };
        LandRegion kasachischessr = new LandRegion(){
            Id = 90,
            Income = 2,
            Name = "Kasachische SSR",
            NationId = 3,
            Identifier = ERegion.Kazakhstan,
            
        };
        LandRegion jakutischessr = new LandRegion(){
            Id = 91,
            Income = 1,
            Name = "Jakutische SSR",
            NationId = 3,
            Identifier = ERegion.Yakutia,
            
        };
        LandRegion burjatischessr = new LandRegion(){
            Id = 92,
            Income = 1,
            Name = "Burjatische SSR",
            NationId = 3,
            Identifier = ERegion.Buryatia,
            
        };
        LandRegion sowjetischer_ferner_osten = new LandRegion(){
            Id = 93,
            Income = 1,
            Name = "Sowjetischer Ferner Osten",
            NationId = 3,
            Identifier = ERegion.SovietFarEast,
            
        };

        #endregion

        #region Großbritannien

        LandRegion ägypten = new LandRegion(){
            Id = 83,
            Income = 2,
            Name = "Ägypten",
            NationId = 5,
            Identifier = ERegion.Egypt,
            
        };
        LandRegion transjordanien = new LandRegion(){
            Id = 94,
            Income = 1,
            Name = "Transjordanien",
            NationId = 5,
            Identifier = ERegion.Transjordan,
            
        };
        LandRegion persien = new LandRegion(){
            Id = 95,
            Income = 1,
            Name = "Persien",
            NationId = 5,
            Identifier = ERegion.Persia,
            
        };
        LandRegion indien = new LandRegion(){
            Id = 96,
            Income = 3,
            Name = "Indien",
            NationId = 5,
            Identifier = ERegion.India,
            
        };
        LandRegion burma = new LandRegion(){
            Id = 97,
            Income = 1,
            Name = "Burma",
            NationId = 5,
            Identifier = ERegion.Burma,
            
        };
        LandRegion westaustralien = new LandRegion(){
            Id = 98,
            Income = 1,
            Name = "Westaustralien",
            NationId = 5,
            Identifier = ERegion.WestAustralia,
            
        };
        LandRegion ostaustralien = new LandRegion(){
            Id = 99,
            Income = 1,
            Name = "Ostaustralien",
            NationId = 5,
            Identifier = ERegion.EastAustralia,
            
        };
        LandRegion neuseeland = new LandRegion(){
            Id = 100,
            Income = 1,
            Name = "Neuseeland",
            NationId = 5,
            Identifier = ERegion.NewZealand,
            
        };
        LandRegion französisch_madagaskar = new LandRegion(){
            Id = 101,
            Income = 1,
            Name = "Französisch Madagaskar",
            NationId = 5,
            Identifier = ERegion.FrenchMadagascar,
            
        };
        LandRegion südafrikanische_union = new LandRegion(){
            Id = 102,
            Income = 2,
            Name = "Südafrikanische Union",
            NationId = 5,
            Identifier = ERegion.SouthAfricanUnion,
            
        };
        LandRegion rhodesien = new LandRegion(){
            Id = 103,
            Income = 1,
            Name = "Rhodesien",
            NationId = 5,
            Identifier = ERegion.Rhodesia,
            
        };
        LandRegion belgisch_kongo = new LandRegion(){
            Id = 104,
            Income = 1,
            Name = "Belgisch-Kongo",
            NationId = 5,
            Identifier = ERegion.BelgianCongo,
            
        };
        LandRegion anglo_ägyptischer_sudan = new LandRegion(){
            Id = 105,
            Income = 0,
            Name = "Anglo-Ägyptischer Sudan",
            NationId = 5,
            Identifier = ERegion.AngloEgyptianSudan,
            
        };
        LandRegion italienisch_ostafrika = new LandRegion(){
            Id = 106,
            Income = 1,
            Name = "Italienisch-Ostafrika",
            NationId = 5,
            Identifier = ERegion.ItalianEastAfrica,
            
        };
        LandRegion französisch_äquatorialafrika = new LandRegion(){
            Id = 107,
            Income = 1,
            Name = "Französisch-Äquatorialafrika",
            NationId = 5,
            Identifier = ERegion.FrenchEquatorialAfrica,
            
        };
        LandRegion französisch_westafrika = new LandRegion(){
            Id = 108,
            Income = 1,
            Name = "Französisch-Westafrika",
            NationId = 5,
            Identifier = ERegion.FrenchWestAfrica,
            
        };
        LandRegion gibraltar = new LandRegion(){
            Id = 109,
            Income = 0,
            Name = "Gibraltar",
            NationId = 5,
            Identifier = ERegion.Gibraltar,
            
        };
        LandRegion vereinigtes_königreich = new LandRegion(){
            Id = 110,
            Income = 8,
            Name = "Vereinigtes Königreich",
            NationId = 5,
            Identifier = ERegion.UnitedKingdom,
            
        };
        LandRegion island = new LandRegion(){
            Id = 111,
            Income = 0,
            Name = "Island",
            NationId = 5,
            Identifier = ERegion.Iceland,
            
        };
        LandRegion ostkanada = new LandRegion(){
            Id = 112,
            Income = 3,
            Name = "Ostkanada",
            NationId = 5,
            Identifier = ERegion.EastCanada,
            
        };
        LandRegion westkanada = new LandRegion(){
            Id = 113,
            Income = 1,
            Name = "Westkanada",
            NationId = 5,
            Identifier = ERegion.WestCanada,
            
        };

        #endregion

        #region USA

        LandRegion alaska = new LandRegion(){
            Id = 114,
            Income = 2,
            Name = "Alaska",
            NationId = 4,
            Identifier = ERegion.Alaska,
            
        };
        LandRegion westliche_vereinigte_staaten = new LandRegion(){
            Id = 115,
            Income = 10,
            Name = "Westliche Vereinigte Staaten",
            NationId = 4,
            Identifier = ERegion.WesternUnitedStates,
            
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
            
        };
        LandRegion midway_atoll = new LandRegion(){
            Id = 124,
            Income = 0,
            Name = "Midway-Atoll",
            NationId = 4,
            Identifier = ERegion.MidwayAtoll,
            
        };
        LandRegion hawaii_inseln = new LandRegion(){
            Id = 125,
            Income = 1,
            Name = "Hawaii-Inseln",
            NationId = 4,
            Identifier = ERegion.HawaiiIslands,
            
        };
        LandRegion sinkiang = new LandRegion(){
            Id = 126,
            Income = 1,
            Name = "Sinkiang",
            NationId = 4,
            Identifier = ERegion.Sinkiang,
            
        };
        LandRegion anhwei = new LandRegion(){
            Id = 127,
            Income = 1,
            Name = "Anhwei",
            NationId = 4,
            Identifier = ERegion.Anhwei,
            
        };
        LandRegion sezuan = new LandRegion(){
            Id = 128,
            Income = 1,
            Name = "Sezuan",
            NationId = 4,
            Identifier = ERegion.Sezuan,
            
        };
        LandRegion yunnan = new LandRegion(){
            Id = 129,
            Income = 1,
            Name = "Yunnan",
            NationId = 4,
            Identifier = ERegion.Yunnan,
            
        };

        #endregion

        #region Japan

        LandRegion japan = new LandRegion(){
            Id = 130,
            Income = 8,
            Name = "Japan",
            NationId = 2,
            Identifier = ERegion.Japan,
            
        };
        LandRegion mandschurei = new LandRegion(){
            Id = 131,
            Income = 3,
            Name = "Mandschurei",
            NationId = 2,
            Identifier = ERegion.Mandschurei,
            
        };
        LandRegion jiangsu = new LandRegion(){
            Id = 132,
            Income = 2,
            Name = "Jiangsu",
            NationId = 2,
            Identifier = ERegion.Jiangsu,
            
        };
        LandRegion guandong = new LandRegion(){
            Id = 133,
            Income = 2,
            Name = "Guandong",
            NationId = 2,
            Identifier = ERegion.Guandong,
            
        };
        LandRegion französisch_indochina_thailand = new LandRegion(){
            Id = 134,
            Income = 2,
            Name = "Französisch-Indochina-Thailand",
            NationId = 2,
            Identifier = ERegion.FrenchIndochinaThailand,
            
        };
        LandRegion malaysia = new LandRegion(){
            Id = 135,
            Income = 1,
            Name = "Malaysia",
            NationId = 2,
            Identifier = ERegion.Malaysia,
            
        };
        LandRegion borneo = new LandRegion(){
            Id = 136,
            Income = 4,
            Name = "Borneo",
            NationId = 2,
            Identifier = ERegion.Borneo,
            
        };
        LandRegion ostindien = new LandRegion(){
            Id = 137,
            Income = 4,
            Name = "Ostindien",
            NationId = 2,
            Identifier = ERegion.EastIndia,
            
        };
        LandRegion salomon_inseln = new LandRegion(){
            Id = 138,
            Income = 0,
            Name = "Salomon-Inseln",
            NationId = 2,
            Identifier = ERegion.SolomonIslands,
            
        };
        LandRegion neuguniea = new LandRegion(){
            Id = 139,
            Income = 1,
            Name = "Neuguniea",
            NationId = 2,
            Identifier = ERegion.NewGuinea,
            
        };
        LandRegion philippinische_inseln = new LandRegion(){
            Id = 140,
            Income = 3,
            Name = "Philippinische Inseln",
            NationId = 2,
            Identifier = ERegion.PhilippineIslands,
            
        };
        LandRegion formosa = new LandRegion(){
            Id = 141,
            Income = 0,
            Name = "Formosa",
            NationId = 2,
            Identifier = ERegion.Formosa,
            
        };
        LandRegion okinawa = new LandRegion(){
            Id = 142,
            Income = 0,
            Name = "Okinawa",
            NationId = 2,
            Identifier = ERegion.Okinawa
        };
        LandRegion iwojima = new LandRegion(){
            Id = 143,
            Income = 0,
            Name = "Iwojima",
            NationId = 2,
            Identifier = ERegion.Iwojima,
            
        };
        LandRegion wake = new LandRegion(){
            Id = 144,
            Income = 0,
            Name = "Wake",
            NationId = 2,
            Identifier = ERegion.Wake,
            
        };
        LandRegion caroline_atoll = new LandRegion(){
            Id = 145,
            Income = 0,
            Name = "Caroline-Atoll",
            NationId = 2,
            Identifier = ERegion.CarolineAtoll,
            
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
                
            },
            new LandRegion(){
                Id = 147,
                Income = 0,
                Name = "Himalaya",
                NationId = 6,
                Identifier = ERegion.Himalaya,
                
            },
            new LandRegion(){
                Id = 148,
                Income = 0,
                Name = "Afghanistan",
                NationId = 6,
                Identifier = ERegion.Afghanistan,
                
            },
            new LandRegion(){
                Id = 149,
                Income = 0,
                Name = "Saudi Arabien",
                NationId = 6,
                Identifier = ERegion.SaudiArabia,
                
            },
            new LandRegion(){
                Id = 150,
                Income = 0,
                Name = "Türkei",
                NationId = 6,
                Identifier = ERegion.Turkey,
                
            },
            new LandRegion(){
                Id = 151,
                Income = 0,
                Name = "Schweden",
                NationId = 6,
                Identifier = ERegion.Sweden,
                
            },
            new LandRegion(){
                Id = 152,
                Income = 0,
                Name = "Irland",
                NationId = 6,
                Identifier = ERegion.Ireland,
                
            },
            new LandRegion(){
                Id = 153,
                Income = 0,
                Name = "Spanien Portugal",
                NationId = 6,
                Identifier = ERegion.SpainPortugal,
                
            },
            new LandRegion(){
                Id = 154,
                Income = 0,
                Name = "Sahara",
                NationId = 6,
                Identifier = ERegion.Sahara,
                
            },
            new LandRegion(){
                Id = 155,
                Income = 0,
                Name = "Angola",
                NationId = 6,
                Identifier = ERegion.Angola,
                
            },
            new LandRegion(){
                Id = 156,
                Income = 0,
                Name = "Mosambik",
                NationId = 6,
                Identifier = ERegion.Mozambique,
                
            },
            new LandRegion(){
                Id = 157,
                Income = 0,
                Name = "Schweiz",
                NationId = 6,
                Identifier = ERegion.Switzerland,
                
            },
            new LandRegion(){
                Id = 158,
                Income = 0,
                Name = "Venezuela",
                NationId = 6,
                Identifier = ERegion.Venezuela,
               
                PositionX = 165,
                PositionY = 670
            },
            new LandRegion(){
                Id = 159,
                Income = 0,
                Name = "Kolumbien Ecuador",
                NationId = 6,
                Identifier = ERegion.ColombiaEcuador,
               
                PositionX = 105,
                PositionY = 675
            },
            new LandRegion(){
                Id = 160,
                Income = 0,
                Name = "Peru Argentinien",
                NationId = 6,
                Identifier = ERegion.PeruArgentina,
                
            },
            new LandRegion(){
                Id = 161,
                Income = 0,
                Name = "Chile",
                NationId = 6,
                Identifier = ERegion.Chile,
                
            }

            #endregion
        });

        #region WaterRegions

        WaterRegion see1 = new WaterRegion(){ Id = 1, Name = "Seezone 1", Identifier = ERegion.SeeZone1};
        WaterRegion see2 = new WaterRegion(){ Id = 2, Name = "Seezone 2", Identifier = ERegion.SeeZone2 };
        WaterRegion see3 = new WaterRegion(){ Id = 3, Name = "Seezone 3", Identifier = ERegion.SeeZone3 };
        WaterRegion see4 = new WaterRegion(){ Id = 4, Name = "Seezone 4", Identifier = ERegion.SeeZone4 };
        WaterRegion see5 = new WaterRegion(){ Id = 5, Name = "Seezone 5", Identifier = ERegion.SeeZone5 };
        WaterRegion see6 = new WaterRegion(){ Id = 6, Name = "Seezone 6", Identifier = ERegion.SeeZone6 };
        WaterRegion see7 = new WaterRegion(){ Id = 7, Name = "Seezone 7", Identifier = ERegion.SeeZone7 };
        WaterRegion see8 = new WaterRegion(){ Id = 8, Name = "Seezone 8", Identifier = ERegion.SeeZone8 };
        WaterRegion see9 = new WaterRegion(){ Id = 9, Name = "Seezone 9", Identifier = ERegion.SeeZone9 };
        WaterRegion see10 = new WaterRegion(){ Id = 10, Name = "Seezone 10", Identifier = ERegion.SeeZone10 };
        WaterRegion see11 = new WaterRegion(){ Id = 11, Name = "Seezone 11", Identifier = ERegion.SeeZone11 };
        WaterRegion see12 = new WaterRegion(){ Id = 12, Name = "Seezone 12", Identifier = ERegion.SeeZone12 };
        WaterRegion see13 = new WaterRegion(){ Id = 13, Name = "Seezone 13", Identifier = ERegion.SeeZone13 };
        WaterRegion see14 = new WaterRegion(){ Id = 14, Name = "Seezone 14", Identifier = ERegion.SeeZone14 };
        WaterRegion see15 = new WaterRegion(){ Id = 15, Name = "Seezone 15", Identifier = ERegion.SeeZone15 };
        WaterRegion see16 = new WaterRegion(){ Id = 16, Name = "Seezone 16", Identifier = ERegion.SeeZone16 };
        WaterRegion see17 = new WaterRegion(){ Id = 17, Name = "Seezone 17", Identifier = ERegion.SeeZone17 };
        WaterRegion see18 = new WaterRegion(){ Id = 18, Name = "Seezone 18", Identifier = ERegion.SeeZone18 };
        WaterRegion see19 = new WaterRegion(){ Id = 19, Name = "Seezone 19", Identifier = ERegion.SeeZone19 };
        WaterRegion see20 = new WaterRegion(){ Id = 20, Name = "Seezone 20", Identifier = ERegion.SeeZone20 };
        WaterRegion see21 = new WaterRegion(){ Id = 21, Name = "Seezone 21", Identifier = ERegion.SeeZone21, PositionX = 110, PositionY = 1050};
        WaterRegion see22 = new WaterRegion(){ Id = 22, Name = "Seezone 22", Identifier = ERegion.SeeZone22 };
        WaterRegion see23 = new WaterRegion(){ Id = 23, Name = "Seezone 23", Identifier = ERegion.SeeZone23 };
        WaterRegion see24 = new WaterRegion(){ Id = 24, Name = "Seezone 24", Identifier = ERegion.SeeZone24 };
        WaterRegion see25 = new WaterRegion(){ Id = 25, Name = "Seezone 25", Identifier = ERegion.SeeZone25 };
        WaterRegion see26 = new WaterRegion(){ Id = 26, Name = "Seezone 26", Identifier = ERegion.SeeZone26 };
        WaterRegion see27 = new WaterRegion(){ Id = 27, Name = "Seezone 27", Identifier = ERegion.SeeZone27 };
        WaterRegion see28 = new WaterRegion(){ Id = 28, Name = "Seezone 28", Identifier = ERegion.SeeZone28 };
        WaterRegion see29 = new WaterRegion(){ Id = 29, Name = "Seezone 29", Identifier = ERegion.SeeZone29 };
        WaterRegion see30 = new WaterRegion(){ Id = 30, Name = "Seezone 30", Identifier = ERegion.SeeZone30 };
        WaterRegion see31 = new WaterRegion(){ Id = 31, Name = "Seezone 31", Identifier = ERegion.SeeZone31 };
        WaterRegion see32 = new WaterRegion(){ Id = 32, Name = "Seezone 32", Identifier = ERegion.SeeZone32 };
        WaterRegion see33 = new WaterRegion(){ Id = 33, Name = "Seezone 33", Identifier = ERegion.SeeZone33 };
        WaterRegion see34 = new WaterRegion(){ Id = 34, Name = "Seezone 34", Identifier = ERegion.SeeZone34 };
        WaterRegion see35 = new WaterRegion(){ Id = 35, Name = "Seezone 35", Identifier = ERegion.SeeZone35 };
        WaterRegion see36 = new WaterRegion(){ Id = 36, Name = "Seezone 36", Identifier = ERegion.SeeZone36 };
        WaterRegion see37 = new WaterRegion(){ Id = 37, Name = "Seezone 37", Identifier = ERegion.SeeZone37 };
        WaterRegion see38 = new WaterRegion(){ Id = 38, Name = "Seezone 38", Identifier = ERegion.SeeZone38 };
        WaterRegion see39 = new WaterRegion(){ Id = 39, Name = "Seezone 39", Identifier = ERegion.SeeZone39 };
        WaterRegion see40 = new WaterRegion(){ Id = 40, Name = "Seezone 40", Identifier = ERegion.SeeZone40 };
        WaterRegion see41 = new WaterRegion(){ Id = 41, Name = "Seezone 41", Identifier = ERegion.SeeZone41 };
        WaterRegion see42 = new WaterRegion(){ Id = 42, Name = "Seezone 42", Identifier = ERegion.SeeZone42 };
        WaterRegion see43 = new WaterRegion(){ Id = 43, Name = "Seezone 43", Identifier = ERegion.SeeZone43 };
        WaterRegion see44 = new WaterRegion(){ Id = 44, Name = "Seezone 44", Identifier = ERegion.SeeZone44 };
        WaterRegion see45 = new WaterRegion(){ Id = 45, Name = "Seezone 45", Identifier = ERegion.SeeZone45 };
        WaterRegion see46 = new WaterRegion(){ Id = 46, Name = "Seezone 46", Identifier = ERegion.SeeZone46 };
        WaterRegion see47 = new WaterRegion(){ Id = 47, Name = "Seezone 47", Identifier = ERegion.SeeZone47 };
        WaterRegion see48 = new WaterRegion(){ Id = 48, Name = "Seezone 48", Identifier = ERegion.SeeZone48 };
        WaterRegion see49 = new WaterRegion(){ Id = 49, Name = "Seezone 49", Identifier = ERegion.SeeZone49 };
        WaterRegion see50 = new WaterRegion(){ Id = 50, Name = "Seezone 50", Identifier = ERegion.SeeZone50 };
        WaterRegion see51 = new WaterRegion(){ Id = 51, Name = "Seezone 51", Identifier = ERegion.SeeZone51 };
        WaterRegion see52 = new WaterRegion(){ Id = 52, Name = "Seezone 52", Identifier = ERegion.SeeZone52 };
        WaterRegion see53 = new WaterRegion(){ Id = 53, Name = "Seezone 53", Identifier = ERegion.SeeZone53 };
        WaterRegion see54 = new WaterRegion(){ Id = 54, Name = "Seezone 54", Identifier = ERegion.SeeZone54 };
        WaterRegion see55 = new WaterRegion(){ Id = 55, Name = "Seezone 55", Identifier = ERegion.SeeZone55 };
        WaterRegion see56 = new WaterRegion(){ Id = 56, Name = "Seezone 56", Identifier = ERegion.SeeZone56 };
        WaterRegion see57 = new WaterRegion(){ Id = 57, Name = "Seezone 57", Identifier = ERegion.SeeZone57 };
        WaterRegion see58 = new WaterRegion(){ Id = 58, Name = "Seezone 58", Identifier = ERegion.SeeZone58 };
        WaterRegion see59 = new WaterRegion(){ Id = 59, Name = "Seezone 59", Identifier = ERegion.SeeZone59 };
        WaterRegion see60 = new WaterRegion(){ Id = 60, Name = "Seezone 60", Identifier = ERegion.SeeZone60 };
        WaterRegion see61 = new WaterRegion(){ Id = 61, Name = "Seezone 61", Identifier = ERegion.SeeZone61 };
        WaterRegion see62 = new WaterRegion(){ Id = 62, Name = "Seezone 62", Identifier = ERegion.SeeZone62 };
        WaterRegion see63 = new WaterRegion(){ Id = 63, Name = "Seezone 63", Identifier = ERegion.SeeZone63 };
        WaterRegion see64 = new WaterRegion(){ Id = 64, Name = "Seezone 64", Identifier = ERegion.SeeZone64 };
        WaterRegion see65 = new WaterRegion(){ Id = 65, Name = "Seezone 65", Identifier = ERegion.SeeZone65 };
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

        LandUnit inf1 = LandUnitFactory.Create(EUnitType.INFANTRY, deutschland, Germany, true);
        LandUnit inf2 = LandUnitFactory.Create(EUnitType.INFANTRY, deutschland, Germany, true);
        LandUnit inf3 = LandUnitFactory.Create(EUnitType.INFANTRY, deutschland, Germany, true);
        LandUnit inf4 = LandUnitFactory.Create(EUnitType.INFANTRY, polen, Germany, true);
        LandUnit inf5 = LandUnitFactory.Create(EUnitType.INFANTRY, polen, Germany, true);
        LandUnit inf6 = LandUnitFactory.Create(EUnitType.INFANTRY, ukrainischessr, Germany, true);
        LandUnit inf7 = LandUnitFactory.Create(EUnitType.INFANTRY, ukrainischessr, Germany, true);
        LandUnit inf8 = LandUnitFactory.Create(EUnitType.INFANTRY, ukrainischessr, Germany, true);
        LandUnit inf9 = LandUnitFactory.Create(EUnitType.INFANTRY, westrussland, Germany, true);
        LandUnit inf10 = LandUnitFactory.Create(EUnitType.INFANTRY, westrussland, Germany, true);
        LandUnit inf11 = LandUnitFactory.Create(EUnitType.INFANTRY, westrussland, Germany, true);
        LandUnit inf12 = LandUnitFactory.Create(EUnitType.INFANTRY, weissrussland, Germany, true);
        LandUnit inf13 = LandUnitFactory.Create(EUnitType.INFANTRY, weissrussland, Germany, true);
        LandUnit inf14 = LandUnitFactory.Create(EUnitType.INFANTRY, weissrussland, Germany, true);
        LandUnit inf15 = LandUnitFactory.Create(EUnitType.INFANTRY, baltische_staaten, Germany, true);
        LandUnit inf16 = LandUnitFactory.Create(EUnitType.INFANTRY, bulgarien_rumänien, Germany, true);
        LandUnit inf17 = LandUnitFactory.Create(EUnitType.INFANTRY, bulgarien_rumänien, Germany, true);
        LandUnit inf18 = LandUnitFactory.Create(EUnitType.INFANTRY, finnland, Germany, true);
        LandUnit inf19 = LandUnitFactory.Create(EUnitType.INFANTRY, finnland, Germany, true);
        LandUnit inf20 = LandUnitFactory.Create(EUnitType.INFANTRY, finnland, Germany, true);
        LandUnit inf21 = LandUnitFactory.Create(EUnitType.INFANTRY, norwegen, Germany, true);
        LandUnit inf22 = LandUnitFactory.Create(EUnitType.INFANTRY, norwegen, Germany, true);
        LandUnit inf23 = LandUnitFactory.Create(EUnitType.INFANTRY, nordwesteuropa, Germany, true);
        LandUnit inf24 = LandUnitFactory.Create(EUnitType.INFANTRY, frankreich, Germany, true);
        LandUnit inf25 = LandUnitFactory.Create(EUnitType.INFANTRY, italien, Germany, true);
        LandUnit inf26 = LandUnitFactory.Create(EUnitType.INFANTRY, südeuropa, Germany, true);
        LandUnit inf27 = LandUnitFactory.Create(EUnitType.INFANTRY, marokko, Germany, true);
        LandUnit inf28 = LandUnitFactory.Create(EUnitType.INFANTRY, algerien, Germany, true);
        LandUnit inf29 = LandUnitFactory.Create(EUnitType.INFANTRY, lybien, Germany, true);

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

        LandUnit transinf30 = LandUnitFactory.Create(EUnitType.INFANTRY, null, Germany, true);
        LandUnit transinf31 = LandUnitFactory.Create(EUnitType.INFANTRY, null, Germany, true);

        transinf30.Id = 30;
        transinf31.Id = 31;

        LandUnit pan32 = LandUnitFactory.Create(EUnitType.TANK, deutschland, Germany, true);
        LandUnit pan33 = LandUnitFactory.Create(EUnitType.TANK, deutschland, Germany, true);
        LandUnit pan34 = LandUnitFactory.Create(EUnitType.TANK, polen, Germany, true);
        LandUnit pan35 = LandUnitFactory.Create(EUnitType.TANK, ukrainischessr, Germany, true);
        LandUnit pan36 = LandUnitFactory.Create(EUnitType.TANK, westrussland, Germany, true);
        LandUnit pan37 = LandUnitFactory.Create(EUnitType.TANK, baltische_staaten, Germany, true);
        LandUnit pan38 = LandUnitFactory.Create(EUnitType.TANK, bulgarien_rumänien, Germany, true);
        LandUnit pan39 = LandUnitFactory.Create(EUnitType.TANK, nordwesteuropa, Germany, true);
        LandUnit pan40 = LandUnitFactory.Create(EUnitType.TANK, frankreich, Germany, true);
        LandUnit pan41 = LandUnitFactory.Create(EUnitType.TANK, frankreich, Germany, true);
        LandUnit pan42 = LandUnitFactory.Create(EUnitType.TANK, italien, Germany, true);
        LandUnit pan43 = LandUnitFactory.Create(EUnitType.TANK, lybien, Germany, true);

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

        LandUnit flak44 = LandUnitFactory.Create(EUnitType.ANTI_AIR, deutschland, Germany, true);
        LandUnit flak45 = LandUnitFactory.Create(EUnitType.ANTI_AIR, frankreich, Germany, true);
        LandUnit flak46 = LandUnitFactory.Create(EUnitType.ANTI_AIR, italien, Germany, true);

        flak44.Id = 44;
        flak45.Id = 45;
        flak46.Id = 46;

        LandUnit art47 = LandUnitFactory.Create(EUnitType.ARTILLERY, algerien, Germany, true);
        LandUnit art48 = LandUnitFactory.Create(EUnitType.ARTILLERY, südeuropa, Germany, true);
        LandUnit art49 = LandUnitFactory.Create(EUnitType.ARTILLERY, ukrainischessr, Germany, true);
        LandUnit art50 = LandUnitFactory.Create(EUnitType.ARTILLERY, westrussland, Germany, true);

        art47.Id = 47;
        art48.Id = 48;
        art49.Id = 49;
        art50.Id = 50;

        #endregion

        #region RussianLand

        LandUnit inf51 = LandUnitFactory.Create(EUnitType.INFANTRY, russland, Soviet_Union, true);
        LandUnit inf52 = LandUnitFactory.Create(EUnitType.INFANTRY, russland, Soviet_Union, true);
        LandUnit inf53 = LandUnitFactory.Create(EUnitType.INFANTRY, russland, Soviet_Union, true);
        LandUnit inf54 = LandUnitFactory.Create(EUnitType.INFANTRY, russland, Soviet_Union, true);
        LandUnit inf55 = LandUnitFactory.Create(EUnitType.INFANTRY, karelo_finnnischessr, Soviet_Union, true);
        LandUnit inf56 = LandUnitFactory.Create(EUnitType.INFANTRY, karelo_finnnischessr, Soviet_Union, true);
        LandUnit inf57 = LandUnitFactory.Create(EUnitType.INFANTRY, karelo_finnnischessr, Soviet_Union, true);
        LandUnit inf58 = LandUnitFactory.Create(EUnitType.INFANTRY, karelo_finnnischessr, Soviet_Union, true);
        LandUnit inf59 = LandUnitFactory.Create(EUnitType.INFANTRY, archangelsk, Soviet_Union, true);
        LandUnit inf60 = LandUnitFactory.Create(EUnitType.INFANTRY, kaukasus, Soviet_Union, true);
        LandUnit inf61 = LandUnitFactory.Create(EUnitType.INFANTRY, kaukasus, Soviet_Union, true);
        LandUnit inf62 = LandUnitFactory.Create(EUnitType.INFANTRY, kaukasus, Soviet_Union, true);
        LandUnit inf63 = LandUnitFactory.Create(EUnitType.INFANTRY, kasachischessr, Soviet_Union, true);
        LandUnit inf64 = LandUnitFactory.Create(EUnitType.INFANTRY, nowosibirsk, Soviet_Union, true);
        LandUnit inf65 = LandUnitFactory.Create(EUnitType.INFANTRY, autonomer_kreis_der_ewenken, Soviet_Union, true);
        LandUnit inf66 = LandUnitFactory.Create(EUnitType.INFANTRY, autonomer_kreis_der_ewenken, Soviet_Union, true);
        LandUnit inf67 = LandUnitFactory.Create(EUnitType.INFANTRY, jakutischessr, Soviet_Union, true);
        LandUnit inf68 = LandUnitFactory.Create(EUnitType.INFANTRY, burjatischessr, Soviet_Union, true);
        LandUnit inf69 = LandUnitFactory.Create(EUnitType.INFANTRY, burjatischessr, Soviet_Union, true);
        LandUnit inf70 = LandUnitFactory.Create(EUnitType.INFANTRY, sowjetischer_ferner_osten, Soviet_Union, true);
        LandUnit inf71 = LandUnitFactory.Create(EUnitType.INFANTRY, sowjetischer_ferner_osten, Soviet_Union, true);

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

        LandUnit pan72 = LandUnitFactory.Create(EUnitType.TANK, russland, Soviet_Union, true);
        LandUnit pan73 = LandUnitFactory.Create(EUnitType.TANK, russland, Soviet_Union, true);
        LandUnit pan74 = LandUnitFactory.Create(EUnitType.TANK, kaukasus, Soviet_Union, true);
        LandUnit pan75 = LandUnitFactory.Create(EUnitType.TANK, archangelsk, Soviet_Union, true);

        pan72.Id = 72;
        pan73.Id = 73;
        pan74.Id = 74;
        pan75.Id = 75;

        LandUnit flak76 = LandUnitFactory.Create(EUnitType.ANTI_AIR, russland, Soviet_Union, true);
        LandUnit flak77 = LandUnitFactory.Create(EUnitType.ANTI_AIR, kaukasus, Soviet_Union, true);

        flak76.Id = 76;
        flak77.Id = 77;

        LandUnit art78 = LandUnitFactory.Create(EUnitType.ARTILLERY, russland, Soviet_Union, true);
        LandUnit art79 = LandUnitFactory.Create(EUnitType.ARTILLERY, karelo_finnnischessr, Soviet_Union, true);
        LandUnit art80 = LandUnitFactory.Create(EUnitType.ARTILLERY, kaukasus, Soviet_Union, true);

        art78.Id = 78;
        art79.Id = 79;
        art80.Id = 80;

        #endregion

        #region BritishLand

        LandUnit inf81 = LandUnitFactory.Create(EUnitType.INFANTRY, vereinigtes_königreich, United_Kingdom, true);
        LandUnit inf82 = LandUnitFactory.Create(EUnitType.INFANTRY, vereinigtes_königreich, United_Kingdom, true);
        LandUnit inf83 = LandUnitFactory.Create(EUnitType.INFANTRY, ägypten, United_Kingdom, true);
        LandUnit inf84 = LandUnitFactory.Create(EUnitType.INFANTRY, transjordanien, United_Kingdom, true);
        LandUnit inf85 = LandUnitFactory.Create(EUnitType.INFANTRY, persien, United_Kingdom, true);
        LandUnit inf86 = LandUnitFactory.Create(EUnitType.INFANTRY, südafrikanische_union, United_Kingdom, true);
        LandUnit inf87 = LandUnitFactory.Create(EUnitType.INFANTRY, indien, United_Kingdom, true);
        LandUnit inf88 = LandUnitFactory.Create(EUnitType.INFANTRY, indien, United_Kingdom, true);
        LandUnit inf89 = LandUnitFactory.Create(EUnitType.INFANTRY, indien, United_Kingdom, true);
        LandUnit inf90 = LandUnitFactory.Create(EUnitType.INFANTRY, burma, United_Kingdom, true);
        LandUnit inf91 = LandUnitFactory.Create(EUnitType.INFANTRY, ostaustralien, United_Kingdom, true);
        LandUnit inf92 = LandUnitFactory.Create(EUnitType.INFANTRY, ostaustralien, United_Kingdom, true);
        LandUnit inf93 = LandUnitFactory.Create(EUnitType.INFANTRY, westaustralien, United_Kingdom, true);
        LandUnit inf94 = LandUnitFactory.Create(EUnitType.INFANTRY, neuseeland, United_Kingdom, true);
        LandUnit inf95 = LandUnitFactory.Create(EUnitType.INFANTRY, westkanada, United_Kingdom, true);

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

        LandUnit transinf96 = LandUnitFactory.Create(EUnitType.INFANTRY, null, United_Kingdom, true);
        LandUnit transinf97 = LandUnitFactory.Create(EUnitType.INFANTRY, null, United_Kingdom, true);
        LandUnit transinf98 = LandUnitFactory.Create(EUnitType.INFANTRY, null, United_Kingdom, true);
        LandUnit transinf99 = LandUnitFactory.Create(EUnitType.INFANTRY, null, United_Kingdom, true);

        transinf96.Id = 96;
        transinf97.Id = 97;
        transinf98.Id = 98;
        transinf99.Id = 99;

        LandUnit pan100 = LandUnitFactory.Create(EUnitType.TANK, vereinigtes_königreich, United_Kingdom, true);
        LandUnit pan101 = LandUnitFactory.Create(EUnitType.TANK, ostkanada, United_Kingdom, true);
        LandUnit pan102 = LandUnitFactory.Create(EUnitType.TANK, ägypten, United_Kingdom, true);

        pan100.Id = 100;
        pan101.Id = 101;
        pan102.Id = 102;

        LandUnit flak103 = LandUnitFactory.Create(EUnitType.ANTI_AIR, vereinigtes_königreich, United_Kingdom, true);
        LandUnit flak104 = LandUnitFactory.Create(EUnitType.ANTI_AIR, indien, United_Kingdom, true);

        flak103.Id = 103;
        flak104.Id = 104;

        LandUnit art105 = LandUnitFactory.Create(EUnitType.ARTILLERY, vereinigtes_königreich, United_Kingdom, true);
        LandUnit art106 = LandUnitFactory.Create(EUnitType.ARTILLERY, ägypten, United_Kingdom, true);

        art105.Id = 105;
        art106.Id = 106;

        #endregion

        #region USALand

        LandUnit inf107 = LandUnitFactory.Create(EUnitType.INFANTRY, östliche_vereinigte_staaten, United_States, true);
        LandUnit inf108 = LandUnitFactory.Create(EUnitType.INFANTRY, östliche_vereinigte_staaten, United_States, true);
        LandUnit inf109 = LandUnitFactory.Create(EUnitType.INFANTRY, zentrale_vereinigte_staaten, United_States, true);
        LandUnit inf110 = LandUnitFactory.Create(EUnitType.INFANTRY, westliche_vereinigte_staaten, United_States, true);
        LandUnit inf111 = LandUnitFactory.Create(EUnitType.INFANTRY, westliche_vereinigte_staaten, United_States, true);
        LandUnit inf112 = LandUnitFactory.Create(EUnitType.INFANTRY, alaska, United_States, true);
        LandUnit inf113 = LandUnitFactory.Create(EUnitType.INFANTRY, midway_atoll, United_States, true);
        LandUnit inf114 = LandUnitFactory.Create(EUnitType.INFANTRY, hawaii_inseln, United_States, true);
        LandUnit inf115 = LandUnitFactory.Create(EUnitType.INFANTRY, anhwei, United_States, true);
        LandUnit inf116 = LandUnitFactory.Create(EUnitType.INFANTRY, anhwei, United_States, true);
        LandUnit inf117 = LandUnitFactory.Create(EUnitType.INFANTRY, sezuan, United_States, true);
        LandUnit inf118 = LandUnitFactory.Create(EUnitType.INFANTRY, sezuan, United_States, true);
        LandUnit inf119 = LandUnitFactory.Create(EUnitType.INFANTRY, yunnan, United_States, true);
        LandUnit inf120 = LandUnitFactory.Create(EUnitType.INFANTRY, yunnan, United_States, true);

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

        LandUnit transinf121 = LandUnitFactory.Create(EUnitType.INFANTRY, null, United_States, true);
        LandUnit transinf122 = LandUnitFactory.Create(EUnitType.INFANTRY, null, United_States, true);
        LandUnit transinf123 = LandUnitFactory.Create(EUnitType.INFANTRY, null, United_States, true);
        
        transinf121.Id = 121;
        transinf122.Id = 122;
        transinf123.Id = 123;
        
        LandUnit pan124 = LandUnitFactory.Create(EUnitType.TANK,östliche_vereinigte_staaten,United_States, true);

        pan124.Id = 124;
        
        LandUnit flak125 = LandUnitFactory.Create(EUnitType.ANTI_AIR, östliche_vereinigte_staaten, United_States, true);
        LandUnit flak126 = LandUnitFactory.Create(EUnitType.ANTI_AIR, westliche_vereinigte_staaten, United_States, true);

        flak125.Id = 125;
        flak126.Id = 126;
        
        LandUnit art127 = LandUnitFactory.Create(EUnitType.ARTILLERY,östliche_vereinigte_staaten,United_States, true);

        art127.Id = 127;
        
        #endregion

        #region JapanLand
        
        LandUnit inf128 = LandUnitFactory.Create(EUnitType.INFANTRY,japan,Japan, true);
        LandUnit inf129 = LandUnitFactory.Create(EUnitType.INFANTRY,japan,Japan, true);
        LandUnit inf130 = LandUnitFactory.Create(EUnitType.INFANTRY,japan,Japan, true);
        LandUnit inf131 = LandUnitFactory.Create(EUnitType.INFANTRY,japan,Japan, true);
        LandUnit inf132 = LandUnitFactory.Create(EUnitType.INFANTRY,wake,Japan, true);
        LandUnit inf133 = LandUnitFactory.Create(EUnitType.INFANTRY,iwojima,Japan, true);
        LandUnit inf134 = LandUnitFactory.Create(EUnitType.INFANTRY,okinawa,Japan, true);
        LandUnit inf135 = LandUnitFactory.Create(EUnitType.INFANTRY,caroline_atoll,Japan, true);
        LandUnit inf136 = LandUnitFactory.Create(EUnitType.INFANTRY,philippinische_inseln,Japan, true);
        LandUnit inf137 = LandUnitFactory.Create(EUnitType.INFANTRY,jiangsu,Japan, true);
        LandUnit inf138 = LandUnitFactory.Create(EUnitType.INFANTRY,jiangsu,Japan, true);
        LandUnit inf139 = LandUnitFactory.Create(EUnitType.INFANTRY,jiangsu,Japan, true);
        LandUnit inf140 = LandUnitFactory.Create(EUnitType.INFANTRY,jiangsu,Japan, true);
        LandUnit inf141 = LandUnitFactory.Create(EUnitType.INFANTRY,mandschurei,Japan, true);
        LandUnit inf142 = LandUnitFactory.Create(EUnitType.INFANTRY,mandschurei,Japan, true);
        LandUnit inf143 = LandUnitFactory.Create(EUnitType.INFANTRY,mandschurei,Japan, true);
        LandUnit inf144 = LandUnitFactory.Create(EUnitType.INFANTRY,guandong,Japan, true);
        LandUnit inf145 = LandUnitFactory.Create(EUnitType.INFANTRY,französisch_indochina_thailand,Japan, true);
        LandUnit inf146 = LandUnitFactory.Create(EUnitType.INFANTRY,französisch_indochina_thailand,Japan, true);
        LandUnit inf147 = LandUnitFactory.Create(EUnitType.INFANTRY,malaysia,Japan, true);
        LandUnit inf148 = LandUnitFactory.Create(EUnitType.INFANTRY,ostindien,Japan, true);
        LandUnit inf149 = LandUnitFactory.Create(EUnitType.INFANTRY,ostindien,Japan, true);
        LandUnit inf150 = LandUnitFactory.Create(EUnitType.INFANTRY,borneo,Japan, true);
        LandUnit inf151 = LandUnitFactory.Create(EUnitType.INFANTRY,neuguniea,Japan, true);
        LandUnit inf152 = LandUnitFactory.Create(EUnitType.INFANTRY,salomon_inseln,Japan, true);
        
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
        
        LandUnit transinf153 = LandUnitFactory.Create(EUnitType.INFANTRY,null,Japan, true);
        LandUnit transinf154 = LandUnitFactory.Create(EUnitType.INFANTRY,null,Japan, true);

        transinf153.Id = 153;
        transinf154.Id = 154;
        
        LandUnit pan155 = LandUnitFactory.Create(EUnitType.TANK, japan, Japan, true);

        pan155.Id = 155;

        LandUnit flak156 = LandUnitFactory.Create(EUnitType.ANTI_AIR, japan, Japan, true);

        flak156.Id = 156;
        
        LandUnit art157 = LandUnitFactory.Create(EUnitType.ARTILLERY,japan, Japan, true);
        LandUnit art158 = LandUnitFactory.Create(EUnitType.ARTILLERY,guandong, Japan, true);
        LandUnit art159 = LandUnitFactory.Create(EUnitType.ARTILLERY,französisch_indochina_thailand, Japan, true);
        LandUnit art160 = LandUnitFactory.Create(EUnitType.ARTILLERY,philippinische_inseln, Japan, true);
        
        art157.Id = 157;
        art158.Id = 158;
        art159.Id = 159;
        art160.Id = 160;
        
        #endregion

        #region GermanPlanes

        Plane figh161 = PlaneFactory.Create(EUnitType.FIGHTER, deutschland, Germany, true);
        Plane figh162 = PlaneFactory.Create(EUnitType.FIGHTER, nordwesteuropa, Germany, true);
        Plane figh163 = PlaneFactory.Create(EUnitType.FIGHTER, polen, Germany, true);
        Plane figh164 = PlaneFactory.Create(EUnitType.FIGHTER, ukrainischessr, Germany, true);
        Plane figh165 = PlaneFactory.Create(EUnitType.FIGHTER, bulgarien_rumänien, Germany, true);
        Plane figh166 = PlaneFactory.Create(EUnitType.FIGHTER, norwegen, Germany, true);
        
        figh161.Id = 161;
        figh162.Id = 162;
        figh163.Id = 163;
        figh164.Id = 164;
        figh165.Id = 165;
        figh166.Id = 166;
        
        Plane bomb167 = PlaneFactory.Create(EUnitType.BOMBER, deutschland, Germany, true);

        bomb167.Id = 167;
        
        #endregion
        
        #region RussianPlanes

        Plane figh168 = PlaneFactory.Create(EUnitType.FIGHTER, russland, Soviet_Union, true);
        Plane figh169 = PlaneFactory.Create(EUnitType.FIGHTER, karelo_finnnischessr, Soviet_Union, true);

        figh168.Id = 168;
        figh169.Id = 169;
        
        #endregion
        
        #region BritishPlanes
        
        Plane figh170 = PlaneFactory.Create(EUnitType.FIGHTER, vereinigtes_königreich, United_Kingdom, true);
        Plane figh171 = PlaneFactory.Create(EUnitType.FIGHTER, vereinigtes_königreich, United_Kingdom, true);
        Plane figh172 = PlaneFactory.Create(EUnitType.FIGHTER, ägypten, United_Kingdom, true);
        Plane figh173 = PlaneFactory.Create(EUnitType.FIGHTER, see35, United_Kingdom, true);
        
        figh170.Id = 170;
        figh171.Id = 171;
        figh172.Id = 172;
        figh173.Id = 173;
        
        Plane bomb174 = PlaneFactory.Create(EUnitType.BOMBER,vereinigtes_königreich,United_Kingdom, true);

        bomb174.Id = 174;
        
        #endregion
        
        #region USAPlanes
        
        Plane figh175 = PlaneFactory.Create(EUnitType.FIGHTER, östliche_vereinigte_staaten, United_States, true);
        Plane figh176 = PlaneFactory.Create(EUnitType.FIGHTER, westliche_vereinigte_staaten, United_States, true);
        Plane figh177 = PlaneFactory.Create(EUnitType.FIGHTER, hawaii_inseln, United_States, true);
        Plane figh178 = PlaneFactory.Create(EUnitType.FIGHTER, see53, United_States, true);
        Plane figh179 = PlaneFactory.Create(EUnitType.FIGHTER, sezuan, United_States, true);
        
        figh175.Id = 175;
        figh176.Id = 176;
        figh177.Id = 177;
        figh178.Id = 178;
        figh179.Id = 179;
        
        Plane bomb180 = PlaneFactory.Create(EUnitType.BOMBER, östliche_vereinigte_staaten, United_States, true);

        bomb180.Id = 180;
        
        #endregion
        
        #region JapanPlanes
        
        Plane figh181 = PlaneFactory.Create(EUnitType.FIGHTER, japan, Japan, true);
        Plane figh182 = PlaneFactory.Create(EUnitType.FIGHTER, mandschurei, Japan, true);
        Plane figh183 = PlaneFactory.Create(EUnitType.FIGHTER, französisch_indochina_thailand, Japan, true);
        Plane figh184 = PlaneFactory.Create(EUnitType.FIGHTER, see37, Japan, true);
        Plane figh185 = PlaneFactory.Create(EUnitType.FIGHTER, see37, Japan, true);
        Plane figh186 = PlaneFactory.Create(EUnitType.FIGHTER, see50, Japan, true);
        
        figh181.Id = 181;
        figh182.Id = 182;
        figh183.Id = 183;
        figh184.Id = 184;
        figh185.Id = 185;
        figh186.Id = 186;
        
        Plane bomb187 = PlaneFactory.Create(EUnitType.BOMBER, japan, Japan, true);

        bomb187.Id = 187;
        
        #endregion

        #region GermanShips
        
        Ship sub188 = ShipFactory.Create(EUnitType.SUBMARINE, see9, Germany, true);
        Ship sub189 = ShipFactory.Create(EUnitType.SUBMARINE, see9, Germany, true);
        Ship sub190 = ShipFactory.Create(EUnitType.SUBMARINE, see5, Germany, true);
        Ship sub191 = ShipFactory.Create(EUnitType.SUBMARINE, see5, Germany, true);
        
        sub188.Id = 188;
        sub189.Id = 189;
        sub190.Id = 190;
        sub191.Id = 191;

        Ship crs192 = ShipFactory.Create(EUnitType.CRUISER, see5, Germany, true);

        crs192.Id = 192;

        Ship bat193 = ShipFactory.Create(EUnitType.BATTLESHIP, see15, Germany, true);

        bat193.Id = 193;

        Ship t1 = ShipFactory.Create(EUnitType.TRANSPORT, see5, Germany, true);
        Ship t2 = ShipFactory.Create(EUnitType.TRANSPORT, see15, Germany, true);

        Transport tra194 = (Transport)t1;
        Transport tra195 = (Transport)t2;
        
        tra194.Id = 194;
        tra195.Id = 195;
        transinf30.TransportId = 194;
        transinf31.TransportId = 195;

        #endregion
        
        #region RussianShips
        
        Ship sub196 = ShipFactory.Create(EUnitType.SUBMARINE, see4,Soviet_Union, true);

        sub196.Id = 196;
        
        #endregion

        #region BritishShips

        Ship crs197 = ShipFactory.Create(EUnitType.CRUISER, see14, United_Kingdom, true);
        Ship crs198 = ShipFactory.Create(EUnitType.CRUISER, see35, United_Kingdom, true);
        Ship crs199 = ShipFactory.Create(EUnitType.CRUISER, see39, United_Kingdom, true);
        
        crs197.Id = 197;
        crs198.Id = 198;
        crs199.Id = 199;
        
        Ship des200 = ShipFactory.Create(EUnitType.DESTROYER, see10, United_Kingdom, true);
        Ship des201 = ShipFactory.Create(EUnitType.DESTROYER, see17, United_Kingdom, true);

        des200.Id = 200;
        des201.Id = 201;

        Ship sub202 = ShipFactory.Create(EUnitType.SUBMARINE, see39, United_Kingdom, true);

        sub202.Id = 202;
        
        Ship bat203 = ShipFactory.Create(EUnitType.BATTLESHIP, see7, United_Kingdom, true);

        bat203.Id = 203;
        
        Ship t3 = ShipFactory.Create(EUnitType.TRANSPORT, see10, United_Kingdom, true);
        Ship t4 = ShipFactory.Create(EUnitType.TRANSPORT, see7, Germany, true);
        Ship t5 = ShipFactory.Create(EUnitType.TRANSPORT, see35, Germany, true);
        Ship t6 = ShipFactory.Create(EUnitType.TRANSPORT, see39, Germany, true);

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
        
        Ship a1 = ShipFactory.Create(EUnitType.AIRCRAFT_CARRIER, see35,United_Kingdom, true);

        AircraftCarrier air208 = (AircraftCarrier)a1;

        air208.Id = 208;
        
        figh173.AircraftCarrierId = 208;
        
        #endregion
        
        #region USAShips

        Ship sub209 = ShipFactory.Create(EUnitType.SUBMARINE, see53, United_States, true);

        sub209.Id = 209;

        Ship des210 = ShipFactory.Create(EUnitType.DESTROYER, see53, United_States, true);
        Ship des211 = ShipFactory.Create(EUnitType.DESTROYER, see56, United_States, true);
        Ship des212 = ShipFactory.Create(EUnitType.DESTROYER, see11, United_States, true);
        
        des210.Id = 210;
        des211.Id = 211;
        des212.Id = 212;
        
        Ship crs213 = ShipFactory.Create(EUnitType.CRUISER, see19, United_States, true);

        crs213.Id = 213;
        
        Ship bat214 = ShipFactory.Create(EUnitType.BATTLESHIP, see56, United_States, true);

        bat214.Id = 214;
        
        Ship t7 = ShipFactory.Create(EUnitType.TRANSPORT, see56, United_States, true);
        Ship t8 = ShipFactory.Create(EUnitType.TRANSPORT, see11, United_States, true);
        Ship t9 = ShipFactory.Create(EUnitType.TRANSPORT, see11, United_States, true);

        Transport tra215 = (Transport)t7;
        Transport tra216 = (Transport)t8;
        Transport tra217 = (Transport)t9;
        
        tra215.Id = 215;
        tra216.Id = 216;
        tra217.Id = 217;

        transinf121.TransportId = 215;
        transinf122.TransportId = 216;
        transinf123.TransportId = 217;
        
        Ship a2 = ShipFactory.Create(EUnitType.AIRCRAFT_CARRIER, see53, United_States, true);

        AircraftCarrier air218 = (AircraftCarrier)a2;

        air218.Id = 218;

        figh178.AircraftCarrierId = 218;
        
        #endregion

        #region JapanShips

        Ship sub219 = ShipFactory.Create(EUnitType.SUBMARINE, see44, Japan, true);

        sub219.Id = 219;
        
        Ship crs220 = ShipFactory.Create(EUnitType.CRUISER, see50, Japan, true);

        crs220.Id = 220;

        Ship des221 = ShipFactory.Create(EUnitType.DESTROYER, see60, Japan, true);
        Ship des222 = ShipFactory.Create(EUnitType.DESTROYER, see61, Japan, true);

        des221.Id = 221;
        des222.Id = 222;
        
        Ship bat223 = ShipFactory.Create(EUnitType.BATTLESHIP, see60, Japan, true);
        Ship bat224 = ShipFactory.Create(EUnitType.BATTLESHIP, see37, Japan, true);

        bat223.Id = 223;
        bat224.Id = 224;
        
        Ship t10 = ShipFactory.Create(EUnitType.TRANSPORT, see60, Japan, true);
        Ship t11 = ShipFactory.Create(EUnitType.TRANSPORT, see61, Japan, true);

        Transport tra225 = (Transport)t10;
        Transport tra226 = (Transport)t11;

        tra225.Id = 225;
        tra226.Id = 226;

        transinf153.TransportId = 225;
        transinf154.TransportId = 226;
        
        Ship a3 = ShipFactory.Create(EUnitType.AIRCRAFT_CARRIER, see50, Japan, true);
        Ship a4 = ShipFactory.Create(EUnitType.AIRCRAFT_CARRIER, see37, Japan, true);

        AircraftCarrier air227 = (AircraftCarrier)a3;
        AircraftCarrier air228 = (AircraftCarrier)a4;

        air227.Id = 227;
        air228.Id = 228;

        figh186.AircraftCarrierId = 227;
        figh184.AircraftCarrierId = 228;
        figh185.AircraftCarrierId = 228;
        
        #endregion

        builder.Entity<Ship>().HasData(new List<Ship>(){
            sub188,
            sub189,
            sub190,
            sub191,
            crs192,
            bat193,
            sub196,
            crs197,
            crs198,
            crs199,
            des200,
            des201,
            sub202,
            bat203,
            sub209,
            des210,
            des211,
            des212,
            crs213,
            bat214,
            sub219,
            crs220,
            des221,
            des222,
            bat223,
            bat224
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
        
        builder.Entity<LandUnit>().HasData(new List<LandUnit>(){
            inf1,
            inf2,
            inf3,
            inf4,
            inf5,
            inf6,
            inf7,
            inf8,
            inf9,
            inf10,
            inf11,
            inf12,
            inf13,
            inf14,
            inf15,
            inf16,
            inf17,
            inf18,
            inf19,
            inf20,
            inf21,
            inf22,
            inf23,
            inf24,
            inf25,
            inf26,
            inf27,
            inf28,
            inf29,
            transinf30,
            transinf31,
            pan32,
            pan33,
            pan34,
            pan35,
            pan36,
            pan37,
            pan38,
            pan39,
            pan40,
            pan41,
            pan42,
            pan43,
            flak44,
            flak45,
            flak46,
            art47,
            art48,
            art49,
            art50,
            inf51,
            inf52,
            inf53,
            inf54,
            inf55,
            inf56,
            inf57,
            inf58,
            inf59,
            inf60,
            inf61,
            inf62,
            inf63,
            inf64,
            inf65,
            inf66,
            inf67,
            inf68,
            inf69,
            inf70,
            inf71,
            pan72,
            pan73,
            pan74,
            pan75,
            flak76,
            flak77,
            art78,
            art79,
            art80,
            inf81,
            inf82,
            inf83,
            inf84,
            inf85,
            inf86,
            inf87,
            inf88,
            inf89,
            inf90,
            inf91,
            inf92,
            inf93,
            inf94,
            inf95,
            transinf96,
            transinf97,
            transinf98,
            transinf99,
            pan100,
            pan101,
            pan102,
            flak103,
            flak104,
            art105,
            art106,
            inf107,
            inf108,
            inf109,
            inf110,
            inf111,
            inf112,
            inf113,
            inf114,
            inf115,
            inf116,
            inf117,
            inf118,
            inf119,
            inf120,
            transinf121,
            transinf122,
            transinf123,
            pan124,
            flak125,
            flak126,
            art127,
            inf128,
            inf129,
            inf130, 
            inf131,
            inf132,
            inf133,
            inf134,
            inf135,
            inf136,
            inf137,
            inf138,
            inf139,
            inf140,
            inf141,
            inf142,
            inf143,
            inf144,
            inf145,
            inf146,
            inf147,
            inf148,
            inf149,
            inf150,
            inf151,
            inf152,
            transinf153,
            transinf154,
            pan155,
            flak156,
            art157,
            art158,
            art159,
            art160
        });
        
        builder.Entity<Plane>().HasData(new List<Plane>(){
            figh161,
            figh162,
            figh163,
            figh164,
            figh165,
            figh166,
            bomb167,
            figh168,
            figh169,
            figh170,
            figh171,
            figh172,
            figh173,
            bomb174,
            figh175,
            figh176,
            figh177,
            figh178,
            figh179,
            bomb180,
            figh181,
            figh182,
            figh183,
            figh184,
            figh185,
            figh186,
            bomb187
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