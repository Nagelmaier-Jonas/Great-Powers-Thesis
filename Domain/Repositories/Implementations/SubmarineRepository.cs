using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class SubmarineRepository : ACreatableRepository<Submarine>, ISubmarineRepository{
    public SubmarineRepository(GreatPowersDbContext context) : base(context){
    }
}