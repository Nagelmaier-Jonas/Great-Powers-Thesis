using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class ShipRepository : ACreatableRepository<AShip>, IShipRepository{
    public ShipRepository(GreatPowersDbContext context) : base(context){
    }
}