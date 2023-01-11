using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class TankRepository : ACreatableRepository<Tank>, ITankRepository{
    public TankRepository(GreatPowersDbContext context) : base(context){
    }
}