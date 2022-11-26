using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories;

public class UserRepository : ARepository<User>{
    public UserRepository(GreatPowersDbContext context) : base(context){
    }
    
    public async Task<User?> GetByUsername(string username){
        return await _set.Where(u => u.UserName == username)
            .FirstOrDefaultAsync();
    }
}