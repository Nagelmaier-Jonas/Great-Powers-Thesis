using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories;

public class ShipRepository : ARepository<Ship>, IShipRepository{
    public ShipRepository(GreatPowersDbContext context) : base(context){
    }
}