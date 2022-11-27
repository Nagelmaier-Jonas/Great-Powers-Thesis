using Model.Configuration;
using Model.Entities.Regions;

namespace Domain.Repositories;

public class NeighbourRepository : ARepository<Neighbours>, INeighbourRepository{
    public NeighbourRepository(GreatPowersDbContext context) : base(context){
    }
}