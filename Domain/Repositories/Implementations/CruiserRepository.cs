using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class CruiserRepository : ACreatableRepository<Cruiser>, ICruiserRepository{
    public CruiserRepository(GreatPowersDbContext context) : base(context){
    }
}