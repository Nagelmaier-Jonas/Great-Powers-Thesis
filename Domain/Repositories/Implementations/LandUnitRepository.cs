using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories;

public class LandUnitRepository : ARepository<LandUnit>, ILandUnitRepository{
    public LandUnitRepository(GreatPowersDbContext context) : base(context){
    }
}