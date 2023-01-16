using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Domain.Repositories.Implementations;

public class ShipRepository : ACreatableRepository<AShip>, IShipRepository{
    public ShipRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<AShip?> ReadGraphAsync(int Id){
        return await _set
            .Include(n => n.Target)
            .Include(n => n.Region)
            .AsSplitQuery()
            .FirstOrDefaultAsync(n => n.Id == Id);
    }
}