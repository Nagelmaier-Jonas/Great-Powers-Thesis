using Model.Configuration;
using Model.Entities.Regions;

namespace Domain.Repositories;

public class FactoryRepository : ARepository<Factory>, IFactoryRepository{
    public FactoryRepository(GreatPowersDbContext context) : base(context){
    }
}