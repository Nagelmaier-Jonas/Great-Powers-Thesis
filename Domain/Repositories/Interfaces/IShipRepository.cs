using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Domain.Repositories.Interfaces;

public interface IShipRepository : ICreatableRepository<AShip>{
    Task<AShip?> ReadGraphAsync(int Id);
}