using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;
using Model.Entities.Regions;

namespace Domain.Repositories.Implementations;

public class BattleRepository : ACreatableRepository<Battle>, IBattleRepository{
    public BattleRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<Battle?> GetBattleFromLocation(ARegion region){
        return await _set
            .Include(l => l.Location)
            .ThenInclude(t => t.StationedPlanes)
            .Include(l => l.Location)
            .ThenInclude(t => t.Neighbours)
            .Include(l => l.Location)
            .ThenInclude(t => t.IncomingUnits)
            .Include(l => l.Attackers)
            .ThenInclude(n => n.Nation)
            .Include(l => l.Defenders)
            .ThenInclude(n => n.Nation)
            .Include(n => n.CurrentNation)
            .AsSplitQuery()
            .FirstOrDefaultAsync(n => n.LocationId == region.Id);
    }
    
    public async Task<Battle?> ReadBattleGraphAsync(int id){
        return await _set
            .Include(l => l.Location)
            .ThenInclude(t => t.StationedPlanes)
            .Include(l => l.Location)
            .ThenInclude(t => t.Neighbours)
            .Include(l => l.Location)
            .ThenInclude(t => t.IncomingUnits)
            .Include(l => l.Attackers)
            .ThenInclude(n => n.Nation)
            .Include(l => l.Defenders)
            .ThenInclude(n => n.Nation)
            .Include(n => n.CurrentNation)
            .AsSplitQuery()
            .FirstOrDefaultAsync(n => n.Id == id);
    }
    
    public async Task DeleteBattle(int id){
        _context.ChangeTracker.Clear();
        var battle = await ReadAsync(id);
        if (battle is null) return;
        _context.Battles.Remove(battle);
        _set.Remove(battle);
        _context.Entry(battle).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task ChangeBattleNation(int battleId, int nationId){
        _context.ChangeTracker.Clear();
        var battle = await ReadAsync(battleId);
        if (battle is null) return;
        battle.CurrentNationId = nationId;
        battle.CurrentNation = await _context.Nations.FindAsync(nationId);
        _context.Entry(battle).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}