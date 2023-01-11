using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;

namespace Domain.Repositories.Implementations;

public class DestroyerRepository : ACreatableRepository<Destroyer>, IDestroyerRepository{
    public DestroyerRepository(GreatPowersDbContext context) : base(context){
    }
}