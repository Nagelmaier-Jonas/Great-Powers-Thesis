using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class UserRepository : ACreatableRepository<User>{
    public UserRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<User?> GetByUsername(string username){
        return await _set.Where(u => u.UserName == username)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> ReadGraphAsync(string Id){
        return await _set
            .Include(u => u.Nations)
            .FirstOrDefaultAsync(u => u.Id == Id);
    }
}