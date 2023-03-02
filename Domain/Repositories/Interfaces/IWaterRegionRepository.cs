using Model.Entities.Regions;

namespace Domain.Repositories.Interfaces;

public interface IWaterRegionRepository : IRepository<WaterRegion>{
    Task<WaterRegion?> ReadGraphAsync(int Id);

    Task<List<WaterRegion>> ReadAllGraphAsync();
}