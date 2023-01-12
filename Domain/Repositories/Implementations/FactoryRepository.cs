using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Regions;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class FactoryRepository : ACreatableRepository<Factory>, IFactoryRepository{
    public FactoryRepository(GreatPowersDbContext context) : base(context){
    }
}