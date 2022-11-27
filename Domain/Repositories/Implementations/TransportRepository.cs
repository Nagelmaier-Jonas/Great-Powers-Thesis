using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories;

public class TransportRepository : ARepository<Transport>, ITransportRepository{
    public TransportRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<Transport?> ReadGraphAsync(int Id){
        return await _set
            .Include(t => t.Units)
            .FirstOrDefaultAsync(t => t.Id == Id);
    }
}