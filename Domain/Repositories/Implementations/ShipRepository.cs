using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Domain.Repositories.Implementations;

public class ShipRepository : ACreatableRepository<AShip>, IShipRepository{
    public ShipRepository(GreatPowersDbContext context) : base(context){
    }
}