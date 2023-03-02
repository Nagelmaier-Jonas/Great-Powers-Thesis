using Model.Entities.Regions;

namespace Domain.Repositories.Interfaces;

public interface ILandRegionRepository : IRepository<LandRegion>{
    Task<LandRegion?> ReadGraphAsync(int Id);
}