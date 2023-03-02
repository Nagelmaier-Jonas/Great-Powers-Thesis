using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Domain.Repositories.Interfaces;

public interface IPlaneRepository : ICreatableRepository<APlane>{
    Task<APlane?> ReadGraphAsync(int Id);
}