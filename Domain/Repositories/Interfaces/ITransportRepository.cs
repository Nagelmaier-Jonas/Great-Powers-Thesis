using Model.Entities.Units;

namespace Domain.Repositories.Interfaces;

public interface ITransportRepository : ICreatableRepository<Transport>{
    Task<Transport?> ReadGraphAsync(int Id);
}