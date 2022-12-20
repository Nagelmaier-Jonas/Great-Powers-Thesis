using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class ShipRepository : ARepository<Ship>, IShipRepository{
    public ShipRepository(GreatPowersDbContext context) : base(context){
    }
}