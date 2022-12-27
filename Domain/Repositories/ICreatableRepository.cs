namespace Domain.Repositories;

public interface ICreatableRepository<TEntity> where TEntity : class{
    
    Task DeleteAsync(TEntity entity);
    
    Task<TEntity> CreateAsync(TEntity entity);
}