using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Model.Configuration;

public sealed class GreatPowersDbContext : IdentityDbContext<User>{
    
    public GreatPowersDbContext(DbContextOptions options) : base(options){
        Database.SetConnectionString($"server={AppSettings.IpAddress}; port={AppSettings.Port}; database=greatpowers; user=greatpowers; password=greatpowers; Persist Security Info=False; Connect Timeout=300");
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
    public DbSet<Settings> Settings{ get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder){
        builder.Entity<User>().HasIndex(u => new{ u.UserName, u.Email }).IsUnique();

        builder.Entity<Nation>().HasOne(n => n.User)
            .WithMany(u => u.Nations);
        
        builder.Entity<ARegion>().HasOne(n => n.Nation)
            .WithMany(u => u.Regions);
        
        builder.Entity<LandUnit>().HasOne(n => n.Transport)
            .WithMany(u => u.Units);
        builder.Entity<Plane>().HasOne(n => n.AircraftCarrier)
            .WithMany(u => u.Planes);

        builder.Entity<AUnit>().Property(a => a.Type).HasConversion<string>();
        
        builder.Entity<Neighbours>().HasOne(n => n.Region)
            .WithMany(r => r.Neighbours);
        builder.Entity<Neighbours>().HasOne(n => n.Neighbour)
            .WithMany();
        builder.Entity<Neighbours>().HasKey(n => new{ n.NeighbourId, n.RegionId });

        builder.Entity<Factory>().HasOne(f => f.Region)
            .WithMany(r => r.Factories);

        builder.Entity<Settings>().HasNoKey();

        base.OnModelCreating(builder);
    }
}