using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Regions;

namespace Domain.Repositories.Implementations;

public class NeighbourRepository : ARepository<Neighbours>, INeighbourRepository{
    public NeighbourRepository(GreatPowersDbContext context) : base(context){
    }
}