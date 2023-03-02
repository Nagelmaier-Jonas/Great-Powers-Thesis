using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface INationRepository : IRepository<Nation>{
    Task<Nation?> ReadGraphAsync(int Id);
    
    Task<List<Nation>> ReadAllGraphAsync();
    
    Task<List<Nation>> ReadAllCleanGraphAsync();
    
    Task<int> GetFactoryPower(Nation nation);
}