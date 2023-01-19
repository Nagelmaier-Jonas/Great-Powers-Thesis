using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Regions;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Domain.Repositories.Implementations;

public class FactoryRepository : ACreatableRepository<Factory>, IFactoryRepository{
    public FactoryRepository(GreatPowersDbContext context) : base(context){
    }
    public async Task<List<AUnit>?> GetPlaceableFactories(){
        var factories = await ReadAllAsync();
        return factories.Where(f => f.RegionId is null).Cast<AUnit>().ToList();
    }
}