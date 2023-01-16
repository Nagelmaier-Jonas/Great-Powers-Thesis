using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;

namespace Domain.Repositories;

public abstract class ARepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly GreatPowersDbContext _context;
    protected readonly DbSet<TEntity> _set;

    public ARepository(GreatPowersDbContext context){
        _context = context;
        _set = _context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> ReadAsync(int id) => await _set.FindAsync(id);
    

    public virtual async Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter)=> await _set.Where(filter).ToListAsync();
    

    public virtual async Task<List<TEntity>> ReadAllAsync()=> await _set.ToListAsync();
    

    public virtual async Task<List<TEntity>> ReadAsync(int start, int count)=> await _set.Skip(start).Take(count).ToListAsync();
    

    public async Task UpdateAsync(TEntity entity)
    {
        _context.ChangeTracker.Clear();
        _set.Update(entity);
        await _context.SaveChangesAsync();
    }
}