using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;

namespace Domain.Repositories;

public abstract class ACreatableRepository<TEntity> : ARepository<TEntity>,ICreatableRepository<TEntity> where TEntity : class
{
    public ACreatableRepository(GreatPowersDbContext context) : base(context){
    }

    public async Task<TEntity> CreateAsync(TEntity entity){
        _context.ChangeTracker.Clear();
        _set.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TEntity entity){
        _context.ChangeTracker.Clear();
        _set.Remove(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(List<TEntity> entities){
        _set.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
}