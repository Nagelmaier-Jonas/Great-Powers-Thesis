using System.Linq.Expressions;
using Model.Entities.Regions;

namespace Domain.Repositories.Interfaces;

public interface IRegionRepository : IRepository<ARegion>{
    Task<ARegion?> ReadAsync(int id);

    Task<List<ARegion>> ReadAsync(Expression<Func<ARegion, bool>> filter);

    Task<List<ARegion>> ReadAllAsync();

    Task<List<ARegion>> ReadAsync(int start, int count);
}