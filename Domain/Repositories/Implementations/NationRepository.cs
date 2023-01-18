using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class NationRepository : ARepository<Nation>, INationRepository{
    public NationRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<Nation?> ReadGraphAsync(int Id){
        return await _set
            .Include(n => n.Regions)
            .ThenInclude(t => t.StationedUnits)
            .Include(n => n.Regions)
            .ThenInclude(t => t.StationedPlanes)
            .Include(n => n.Regions)
            .ThenInclude(t => t.Capital)
            .Include(n => n.Regions)
            .ThenInclude(t => t.Factory)
            .Include(n => n.Regions)
            .ThenInclude(u => u.Neighbours)
            .Include(u => u.Battles)
            .Include(n => n.Allies)
            .AsSplitQuery()
            .FirstOrDefaultAsync(n => n.Id == Id);
    }
    
    public async Task<List<Nation>> ReadAllGraphAsync(){
        return await _set
            .Include(n => n.Regions)
            .ThenInclude(t => t.StationedUnits)
            .Include(n => n.Regions)
            .ThenInclude(t => t.StationedPlanes)
            .Include(n => n.Regions)
            .ThenInclude(t => t.Capital)
            .Include(n => n.Regions)
            .ThenInclude(t => t.Factory)
            .Include(n => n.Regions)
            .ThenInclude(u => u.Neighbours)
            .Include(u => u.Battles)
            .Include(n => n.Allies)
            .AsSplitQuery()
            .ToListAsync();
    }
    
    public async Task<int> GetFactoryPower(Nation nation){
        return await _set
            .Include(n => n.Regions)
            .ThenInclude(t => t.Factory)
            .Where(n => n.Id == nation.Id)
            .SelectMany(n => n.Regions)
            .Where(r => r.Factory != null)
            .Select(r => r.Income)
            .SumAsync();
    }
}