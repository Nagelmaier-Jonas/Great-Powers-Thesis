using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories;

public class PlaneRepository : ARepository<Plane>, IPlaneRepository{
    public PlaneRepository(GreatPowersDbContext context) : base(context){
    }
}