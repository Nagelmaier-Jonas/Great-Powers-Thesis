using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class AntiAirRepository : ACreatableRepository<AntiAir>, IAntiAirRepository{
    public AntiAirRepository(GreatPowersDbContext context) : base(context){
    }
}