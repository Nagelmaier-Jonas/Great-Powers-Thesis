using System.Linq.Expressions;

namespace Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> ReadAsync(int id);
    Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter);
    Task<List<TEntity>> ReadAllAsync();
    Task<List<TEntity>> ReadAsync(int start, int count);
    Task UpdateAsync(TEntity entity);
}