using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class AircraftCarrierRepository : ARepository<AircraftCarrier>, IAircraftCarrierRepository{
    public AircraftCarrierRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<AircraftCarrier?> ReadGraphAsync(int Id){
        return await _set
            .Include(a => a.Planes)
            .FirstOrDefaultAsync(a => a.Id == Id);
    }
}