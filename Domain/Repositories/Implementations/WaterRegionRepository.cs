using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Regions;

namespace Domain.Repositories;

public class WaterRegionRepository : ARepository<WaterRegion>, IWaterRegionRepository{
    public WaterRegionRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<WaterRegion?> ReadGraphAsync(int Id){
        return await _set
            .Include(w => w.Neighbours)
            .FirstOrDefaultAsync(w => w.Id == Id);
    }
}