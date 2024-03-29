﻿using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Regions;

namespace Domain.Repositories.Implementations;

public class LandRegionRepository : ARepository<LandRegion>, ILandRegionRepository{
    public LandRegionRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<LandRegion?> ReadGraphAsync(int Id){
        _context.ChangeTracker.Clear();
        return await _set
            .Include(l => l.Neighbours)
            .Include(i => i.StationedPlanes)
            .Include(i => i.Canals)
            .Include(i => i.StationedUnits)
            .Include(i => i.IncomingUnits)
            .Include(l => l.Nation)
            .ThenInclude(n => n.Allies)
            .Include(f => f.Factory)
            .FirstOrDefaultAsync(l => l.Id == Id);
    }
}