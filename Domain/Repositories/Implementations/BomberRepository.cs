using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class BomberRepository : ACreatableRepository<Bomber>, IBomberRepository{
    public BomberRepository(GreatPowersDbContext context) : base(context){
    }
}