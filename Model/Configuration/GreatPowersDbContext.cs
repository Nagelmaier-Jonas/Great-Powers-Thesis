using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Configuration;

public sealed class GreatPowersDbContext : IdentityDbContext<User>{
    
    public GreatPowersDbContext(DbContextOptions options) : base(options){
        Database.SetConnectionString($"server={AppSettings.IpAddress}; port={AppSettings.Port}; database=greatpowers; user=greatpowers; password=greatpowers; Persist Security Info=False; Connect Timeout=300");
    }

    public DbSet<User> User{ get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder){
        builder.Entity<User>().HasIndex(u => new{ u.UserName, u.Email }).IsUnique();
        base.OnModelCreating(builder);
    }
}