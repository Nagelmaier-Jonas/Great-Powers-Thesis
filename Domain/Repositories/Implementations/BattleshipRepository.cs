using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class BattleshipRepository : ACreatableRepository<Battleship>, IBattleshipRepository{
    public BattleshipRepository(GreatPowersDbContext context) : base(context){
    }
}