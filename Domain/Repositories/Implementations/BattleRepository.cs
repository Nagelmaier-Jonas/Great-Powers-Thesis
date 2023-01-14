using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class BattleRepository : ACreatableRepository<Battle>, IBattleRepository{
    public BattleRepository(GreatPowersDbContext context) : base(context){
    }
}