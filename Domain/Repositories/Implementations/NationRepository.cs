using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

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
            .AsSplitQuery()
            .FirstOrDefaultAsync(n => n.Id == Id);
    }
    
    public async Task<List<Nation>> ReadAllGraphAsync(){
        return await _set
            .Include(n => n.Regions)
            .ThenInclude(t => t.StationedUnits)
            .Include(n => n.Regions)
            .ThenInclude(t => t.StationedPlanes)
            .AsSplitQuery()
            .ToListAsync();
    }
}