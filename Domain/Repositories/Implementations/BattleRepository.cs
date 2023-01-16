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
            .Include(l => l.Attackers)
            .Include(l => l.Defenders)
            .AsSplitQuery()
            .FirstOrDefaultAsync(n => n.LocationId == region.Id);
    }
}