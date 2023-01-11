using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class ArtilleryRepository : ACreatableRepository<Artillery>, IArtilleryRepository{
    public ArtilleryRepository(GreatPowersDbContext context) : base(context){
    }
}