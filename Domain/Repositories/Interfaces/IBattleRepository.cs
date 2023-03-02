using Model.Entities;
using Model.Entities.Regions;

namespace Domain.Repositories.Interfaces;

public interface IBattleRepository : ICreatableRepository<Battle>{
    Task<Battle?> GetBattleFromLocation(ARegion region);

    Task<Battle?> ReadBattleGraphAsync(int id);
}