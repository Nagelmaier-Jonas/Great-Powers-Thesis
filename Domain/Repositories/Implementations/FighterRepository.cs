using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class FighterRepository : ACreatableRepository<Fighter>, IFighterRepository{
    public FighterRepository(GreatPowersDbContext context) : base(context){
    }
}