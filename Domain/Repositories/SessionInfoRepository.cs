using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories;

public class SessionInfoRepository{
    private readonly GreatPowersDbContext _context;
    protected readonly DbSet<SessionInfo> _set;

    public SessionInfoRepository(GreatPowersDbContext context)
    {
        _context = context;
        _set = _context.Set<SessionInfo>();
    }

    public async Task<SessionInfo?> ReadAsync() => await _set.FirstOrDefaultAsync();
    
    public async Task UpdateAsync(SessionInfo entity)
    {
        _context.ChangeTracker.Clear();
        _set.Update(entity);
        await _context.SaveChangesAsync();
    }
    
}