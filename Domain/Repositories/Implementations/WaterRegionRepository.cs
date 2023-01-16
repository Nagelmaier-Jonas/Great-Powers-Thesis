using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class WaterRegionRepository : ARepository<WaterRegion>, IWaterRegionRepository{
    public WaterRegionRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<WaterRegion?> ReadGraphAsync(int Id){
        return await _set
            .Include(w => w.Neighbours)
            .Include(t => t.StationedShips)
            .ThenInclude(s => ((Transport)s).Units)
            .Include(t => t.StationedShips)
            .ThenInclude(s => ((AircraftCarrier)s).Planes)
            .Include(t => t.StationedPlanes)
            .Include(i => i.IncomingUnits)
            .FirstOrDefaultAsync(w => w.Id == Id);
    }

    public async Task<List<WaterRegion>> ReadAllGraphAsync(){
        return await _set
            .Include(w => w.Neighbours)
            .Include(t => t.StationedShips)
            .ThenInclude(s => ((Transport)s).Units)
            .Include(t => t.StationedShips)
            .ThenInclude(s => ((AircraftCarrier)s).Planes)
            .Include(t => t.StationedPlanes)
            .Include(i => i.IncomingUnits)
            .AsSplitQuery()
            .ToListAsync();
    }
}