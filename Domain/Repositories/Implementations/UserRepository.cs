using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class UserRepository : ACreatableRepository<User>{
    public UserRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<User?> GetByUsername(string username){
        return await _set
            .Include(u => u.Nations)
            .ThenInclude(n => n.Regions)
            .ThenInclude(t => t.StationedUnits)
            .Include(u => u.Nations)
            .ThenInclude(n => n.Regions)
            .ThenInclude(t => t.StationedPlanes)
            .Include(u => u.Nations)
            .ThenInclude(n => n.Regions)
            .ThenInclude(t => t.Capital)
            .Include(u => u.Nations)
            .ThenInclude(n => n.Regions)
            .ThenInclude(t => t.Factory)
            .Include(u => u.Nations)
            .ThenInclude(n => n.Regions)
            .ThenInclude(u => u.Neighbours)
            .Include(u => u.Nations)
            .ThenInclude(u => u.Battles)
            .Include(u => u.Nations)
            .ThenInclude(n => n.Allies)
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<User?> ReadGraphAsync(string Id){
        return await _set
            .Include(u => u.Nations)
            .ThenInclude(n => n.Regions)
            .ThenInclude(t => t.StationedUnits)
            .Include(u => u.Nations)
            .ThenInclude(n => n.Regions)
            .ThenInclude(t => t.StationedPlanes)
            .Include(u => u.Nations)
            .ThenInclude(n => n.Regions)
            .ThenInclude(t => t.Capital)
            .Include(u => u.Nations)
            .ThenInclude(n => n.Regions)
            .ThenInclude(t => t.Factory)
            .Include(u => u.Nations)
            .ThenInclude(n => n.Regions)
            .ThenInclude(u => u.Neighbours)
            .Include(u => u.Nations)
            .ThenInclude(u => u.Battles)
            .Include(u => u.Nations)
            .ThenInclude(n => n.Allies)
            .AsSplitQuery()
            .FirstOrDefaultAsync(n => n.Id == Id);
    }
}