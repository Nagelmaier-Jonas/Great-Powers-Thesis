using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;
using Model.Entities.Regions;

namespace Domain.Repositories.Implementations;

public class BattleRepository : ACreatableRepository<Battle>, IBattleRepository{
    public BattleRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<Battle?> GetBattleFromLocation(ARegion region){
        return await _set
            .Include(l => l.Location)
            .ThenInclude(t => t.StationedPlanes)
            .Include(l => l.Location)
            .ThenInclude(t => t.Neighbours)
            .Include(l => l.Location)
            .ThenInclude(t => t.IncomingUnits)
            .Include(l => l.Attackers)
            .ThenInclude(n => n.Nation)
            .Include(l => l.Defenders)
            .ThenInclude(n => n.Nation)
            .AsSplitQuery()
            .FirstOrDefaultAsync(n => n.LocationId == region.Id);
    }
}