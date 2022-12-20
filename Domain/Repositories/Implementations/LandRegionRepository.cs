using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Regions;

namespace Domain.Repositories.Implementations;

public class LandRegionRepository : ARepository<LandRegion>, ILandRegionRepository{
    public LandRegionRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<LandRegion?> ReadGraphAsync(int Id){
        return await _set
            .Include(l => l.Neighbours)
            .FirstOrDefaultAsync(l => l.Id == Id);
    }
}