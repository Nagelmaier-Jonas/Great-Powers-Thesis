using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class PlaneRepository : ACreatableRepository<APlane>, IPlaneRepository{
    public PlaneRepository(GreatPowersDbContext context) : base(context){
    }
}