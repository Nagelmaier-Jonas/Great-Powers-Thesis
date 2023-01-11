using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class InfantryRepository : ACreatableRepository<Infantry>, IInfantryRepository{
    public InfantryRepository(GreatPowersDbContext context) : base(context){
    }
}