using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

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
        builder.Entity<User>().HasIndex(u => new{ u.UserName, u.Email }).IsUnique();
        
        builder.Entity<Nation>().HasOne(n => n.User)
            .WithMany(u => u.Nations);
        
        builder.Entity<Allies>().HasOne(n => n.Nation)
            .WithMany(r => r.Allies);
        builder.Entity<Allies>().HasOne(n => n.Ally)
            .WithMany();
        builder.Entity<Allies>().HasKey(n => new{ n.NationId, n.AllyId });
        
        builder.Entity<Capital>().HasIndex(u => u.Name).IsUnique();
        
        builder.Entity<ARegion>().Property(a => a.Type).HasConversion<string>();
        
        builder.Entity<LandRegion>().HasOne(n => n.Nation)
            .WithMany(u => u.Regions);
        builder.Entity<LandRegion>().HasOne(l => l.Capital).WithOne();
        
        builder.Entity<Factory>().HasOne(f => f.Region)
            .WithMany(r => r.Factories);

        builder.Entity<Neighbours>().HasOne(n => n.Region)
            .WithMany(r => r.Neighbours);
        builder.Entity<Neighbours>().HasOne(n => n.Neighbour)
            .WithMany();
        builder.Entity<Neighbours>().HasKey(n => new{ n.NeighbourId, n.RegionId });
        
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

        builder.Entity<SessionInfo>().HasOne(s => s.Nation).WithOne().HasForeignKey<SessionInfo>(s => s.CurrentNationId);

        builder.Entity<Nation>().HasData(new List<Nation>(){
            new Nation(){
                Id = 1,
                Name = "Germany",
                Color = "#8ea39e"
            },
            new Nation(){
                Id = 2,
                Name = "Japan",
                Color = "#d29151"
            },
            new Nation(){
                Id = 3,
                Name = "Soviet_Union",
                Color = "#ba8772"
            },
            new Nation(){
                Id = 4,
                Name = "United_States",
                Color = "#97a95f"
            },
            new Nation(){
                Id = 5,
                Name = "United_Kingdom",
                Color = "#b59b68"
            }
        });

        builder.Entity<Allies>().HasData(new List<Allies>(){
            new Allies(){
                NationId =1,
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
            #region Deutschland

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
                RegionId = 78
            },
            new Factory(){
                Id = 5,
                Damage = 0,
                RegionId = 84
            }

            #endregion
        });

        builder.Entity<Capital>().HasData(new List<Capital>(){
            new Capital(){
                Id = 1,
                Name = "Berlin"
            },
            new Capital(){
                Id = 2,
                Name = "Paris"
            },
            new Capital(){
                Id = 3,
                Name = "Rom"
            },
            new Capital(){
                Id = 4,
                Name = "Leningrad"
            },
            new Capital(){
                Id = 5,
                Name = "Moskau"
            },
            new Capital(){
                Id = 6,
                Name = "London"
            },
            new Capital(){
                Id = 7,
                Name = "Kalkutta"
            },
            new Capital(){
                Id = 8,
                Name = "Shanghai"
            },
            new Capital(){
                Id = 9,
                Name = "Tokyo"
            },
            new Capital(){
                Id = 10,
                Name = "Manila"
            },
            new Capital(){
                Id = 11,
                Name = "San Francisco"
            },
            new Capital(){
                Id = 12,
                Name = "Washington"
            },
            new Capital(){
                Id = 13,
                Name = "Honululu"
            }
        });

        builder.Entity<LandRegion>().HasData(new List<LandRegion>(){
            #region Deutschland

            new LandRegion(){
                Id = 66,
                Income = 10,
                Name = "Deutschland",
                CapitalId = 1,
                NationId = 1
            },
            new LandRegion(){
                Id = 67,
                Income = 2,
                Name = "Polen",
                NationId = 1
            },
            new LandRegion(){
                Id = 68,
                Income = 2,
                Name = "Baltische Staaten",
                NationId = 1
            },
            new LandRegion(){
                Id = 69,
                Income = 2,
                Name = "Weissrussland",
                NationId = 1
            },
            new LandRegion(){
                Id = 70,
                Income = 2,
                Name = "Ukrainische SSR",
                NationId = 1
            },
            new LandRegion(){
                Id = 71,
                Income = 2,
                Name = "West Russland",
                NationId = 1
            },
            new LandRegion(){
                Id = 72,
                Income = 1,
                Name = "Finnland",
                NationId = 1
            },
            new LandRegion(){
                Id = 73,
                Income = 2,
                Name = "Norwegen",
                NationId = 1
            },
            new LandRegion(){
                Id = 74,
                Income = 4,
                Name = "Kaukasus",
                NationId = 1
            },
            new LandRegion(){
                Id = 75,
                Income = 2,
                Name = "Bulgarien Rumänien",
                NationId = 1
            },
            new LandRegion(){
                Id = 76,
                Income = 2,
                Name = "Südeuropa",
                NationId = 1
            },
            new LandRegion(){
                Id = 77,
                Income = 3,
                Name = "Italien",
                CapitalId = 3,
                NationId = 1
            },
            new LandRegion(){
                Id = 78,
                Income = 6,
                Name = "Frankreich",
                CapitalId = 2,
                NationId = 1
            },
            new LandRegion(){
                Id = 79,
                Income = 2,
                Name = "Nordwesteuropa",
                NationId = 1
            },
            new LandRegion(){
                Id = 80,
                Income = 1,
                Name = "Marokko",
                NationId = 1
            },
            new LandRegion(){
                Id = 81,
                Income = 1,
                Name = "Algerien",
                NationId = 1
            },
            new LandRegion(){
                Id = 82,
                Income = 1,
                Name = "Libyen",
                NationId = 1
            },
            new LandRegion(){
                Id = 83,
                Income = 2,
                Name = "Ägypten",
                NationId = 1
            },
            new LandRegion(){
                Id = 84,
                Income = 2,
                Name = "Karelo-Finnische SSR",
                CapitalId = 4,
                NationId = 1
            },

            #endregion
        });

        builder.Entity<WaterRegion>().HasData(new List<WaterRegion>(){
            new WaterRegion(){ Id = 1, Name = "Seezone 1" },
            new WaterRegion(){ Id = 2, Name = "Seezone 2" },
            new WaterRegion(){ Id = 3, Name = "Seezone 3" },
            new WaterRegion(){ Id = 4, Name = "Seezone 4" },
            new WaterRegion(){ Id = 5, Name = "Seezone 5" },
            new WaterRegion(){ Id = 6, Name = "Seezone 6" },
            new WaterRegion(){ Id = 7, Name = "Seezone 7" },
            new WaterRegion(){ Id = 8, Name = "Seezone 8" },
            new WaterRegion(){ Id = 9, Name = "Seezone 9" },
            new WaterRegion(){ Id = 10, Name = "Seezone 10" },
            new WaterRegion(){ Id = 11, Name = "Seezone 11" },
            new WaterRegion(){ Id = 12, Name = "Seezone 12" },
            new WaterRegion(){ Id = 13, Name = "Seezone 13" },
            new WaterRegion(){ Id = 14, Name = "Seezone 14" },
            new WaterRegion(){ Id = 15, Name = "Seezone 15" },
            new WaterRegion(){ Id = 16, Name = "Seezone 16" },
            new WaterRegion(){ Id = 17, Name = "Seezone 17" },
            new WaterRegion(){ Id = 18, Name = "Seezone 18" },
            new WaterRegion(){ Id = 19, Name = "Seezone 19" },
            new WaterRegion(){ Id = 20, Name = "Seezone 20" },
            new WaterRegion(){ Id = 21, Name = "Seezone 21" },
            new WaterRegion(){ Id = 22, Name = "Seezone 22" },
            new WaterRegion(){ Id = 23, Name = "Seezone 23" },
            new WaterRegion(){ Id = 24, Name = "Seezone 24" },
            new WaterRegion(){ Id = 25, Name = "Seezone 25" },
            new WaterRegion(){ Id = 26, Name = "Seezone 26" },
            new WaterRegion(){ Id = 27, Name = "Seezone 27" },
            new WaterRegion(){ Id = 28, Name = "Seezone 28" },
            new WaterRegion(){ Id = 29, Name = "Seezone 29" },
            new WaterRegion(){ Id = 30, Name = "Seezone 30" },
            new WaterRegion(){ Id = 31, Name = "Seezone 31" },
            new WaterRegion(){ Id = 32, Name = "Seezone 32" },
            new WaterRegion(){ Id = 33, Name = "Seezone 33" },
            new WaterRegion(){ Id = 34, Name = "Seezone 34" },
            new WaterRegion(){ Id = 35, Name = "Seezone 35" },
            new WaterRegion(){ Id = 36, Name = "Seezone 36" },
            new WaterRegion(){ Id = 37, Name = "Seezone 37" },
            new WaterRegion(){ Id = 38, Name = "Seezone 38" },
            new WaterRegion(){ Id = 39, Name = "Seezone 39" },
            new WaterRegion(){ Id = 40, Name = "Seezone 40" },
            new WaterRegion(){ Id = 41, Name = "Seezone 41" },
            new WaterRegion(){ Id = 42, Name = "Seezone 42" },
            new WaterRegion(){ Id = 43, Name = "Seezone 43" },
            new WaterRegion(){ Id = 44, Name = "Seezone 44" },
            new WaterRegion(){ Id = 45, Name = "Seezone 45" },
            new WaterRegion(){ Id = 46, Name = "Seezone 46" },
            new WaterRegion(){ Id = 47, Name = "Seezone 47" },
            new WaterRegion(){ Id = 48, Name = "Seezone 48" },
            new WaterRegion(){ Id = 49, Name = "Seezone 49" },
            new WaterRegion(){ Id = 50, Name = "Seezone 50" },
            new WaterRegion(){ Id = 51, Name = "Seezone 51" },
            new WaterRegion(){ Id = 52, Name = "Seezone 52" },
            new WaterRegion(){ Id = 53, Name = "Seezone 53" },
            new WaterRegion(){ Id = 54, Name = "Seezone 54" },
            new WaterRegion(){ Id = 55, Name = "Seezone 55" },
            new WaterRegion(){ Id = 56, Name = "Seezone 56" },
            new WaterRegion(){ Id = 57, Name = "Seezone 57" },
            new WaterRegion(){ Id = 58, Name = "Seezone 58" },
            new WaterRegion(){ Id = 59, Name = "Seezone 59" },
            new WaterRegion(){ Id = 60, Name = "Seezone 60" },
            new WaterRegion(){ Id = 61, Name = "Seezone 61" },
            new WaterRegion(){ Id = 62, Name = "Seezone 62" },
            new WaterRegion(){ Id = 63, Name = "Seezone 63" },
            new WaterRegion(){ Id = 64, Name = "Seezone 64" },
            new WaterRegion(){ Id = 65, Name = "Seezone 65" },
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
                RegionId = 2,
                NeighbourId = 3
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
                RegionId = 5,
                NeighbourId = 6
            },
            new Neighbours(){
                RegionId = 6,
                NeighbourId = 5
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
                NeighbourId = 17
            },
            new Neighbours(){
                RegionId = 16,
                NeighbourId = 15
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
                RegionId = 18,
                NeighbourId = 11
            },
            new Neighbours(){
                RegionId = 18,
                NeighbourId = 12
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
                NeighbourId = 25
            },
            new Neighbours(){
                RegionId = 28,
                NeighbourId = 27
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

            #endregion

            #region LandRegions

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
            }

            #endregion
        });

        base.OnModelCreating(builder);
    }
}