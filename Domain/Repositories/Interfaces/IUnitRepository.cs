using System.Linq.Expressions;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Domain.Repositories.Interfaces;

public interface IUnitRepository : ICreatableRepository<AUnit>{
    Task<AUnit?> ReadAsync(int id);

    Task<List<AUnit>> ReadAsync(Expression<Func<AUnit, bool>> filter);

    Task<List<AUnit>> ReadAllAsync();

    Task<List<AUnit>> ReadAsync(int start, int count);
}