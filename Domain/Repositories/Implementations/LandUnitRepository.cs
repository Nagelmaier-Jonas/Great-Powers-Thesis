using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Domain.Repositories.Implementations;

public class LandUnitRepository : ACreatableRepository<ALandUnit>, ILandUnitRepository{
    public LandUnitRepository(GreatPowersDbContext context) : base(context){
    }
    public async Task<ALandUnit?> ReadGraphAsync(int Id){
        return await _set
            .Include(n => n.Target)
            .ThenInclude(t => t.Neighbours)
            .ThenInclude(n => n.Neighbour)
            .Include(n => n.Region)
            .ThenInclude(r => r.Neighbours)
            .ThenInclude(n => n.Neighbour)
            .Include(n => n.Region)
            .ThenInclude(r => r.Nation)
            .Include(u => u.Nation)
            .ThenInclude(n => n.Allies)
            .AsSplitQuery()
            .FirstOrDefaultAsync(n => n.Id == Id);
    }
}