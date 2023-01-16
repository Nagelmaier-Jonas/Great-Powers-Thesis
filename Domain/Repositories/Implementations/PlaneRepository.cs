using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Domain.Repositories.Implementations;

public class PlaneRepository : ACreatableRepository<APlane>, IPlaneRepository{
    public PlaneRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<APlane?> ReadGraphAsync(int Id){
        return await _set
            .Include(n => n.Target)
            .Include(n => n.Region)
            .AsSplitQuery()
            .FirstOrDefaultAsync(n => n.Id == Id);
    }
}