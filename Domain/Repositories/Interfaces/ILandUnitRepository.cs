using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Domain.Repositories.Interfaces;

public interface ILandUnitRepository : ICreatableRepository<ALandUnit>{
    Task<ALandUnit?> ReadGraphAsync(int Id);
}