using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class TransportRepository : ACreatableRepository<Transport>, ITransportRepository{
    public TransportRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<Transport?> ReadGraphAsync(int Id){
        return await _set
            .Include(t => t.Units)
            .FirstOrDefaultAsync(t => t.Id == Id);
    }
}