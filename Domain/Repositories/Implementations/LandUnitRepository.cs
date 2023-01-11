using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class LandUnitRepository : ACreatableRepository<ALandUnit>, ILandUnitRepository{
    public LandUnitRepository(GreatPowersDbContext context) : base(context){
    }
}