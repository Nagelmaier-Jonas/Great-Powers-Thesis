using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class ShipRepository : ACreatableRepository<Ship>, IShipRepository{
    public ShipRepository(GreatPowersDbContext context) : base(context){
    }
}