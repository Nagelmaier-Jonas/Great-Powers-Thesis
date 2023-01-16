using System.Linq.Expressions;
using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Regions;

namespace Domain.Repositories.Implementations;

public class RegionRepository : ARepository<ARegion>, IRegionRepository{
    public LandRegionRepository _LandRegionRepository{ get; set; }
    public WaterRegionRepository _WaterRegionRepository{ get; set; }
    
    public RegionRepository(GreatPowersDbContext context, LandRegionRepository landRegionRepository, WaterRegionRepository waterRegionRepository) : base(context){
        _LandRegionRepository = landRegionRepository;
        _WaterRegionRepository = waterRegionRepository;
    }
    
    public override async Task<ARegion?> ReadAsync(int id){
        ARegion region = await base.ReadAsync(id);
        if (region is null) return null;
        if (region.IsLandRegion()) return await _LandRegionRepository.ReadAsync(region.Id);
        if (region.IsWaterRegion()) return await _WaterRegionRepository.ReadAsync(region.Id);
        return region;
    }

    public override async Task<List<ARegion>> ReadAsync(Expression<Func<ARegion, bool>> filter){
        List<ARegion> regions = await base.ReadAsync(filter);
        if (regions.Count == 0) return regions;

        List<ARegion> result = new List<ARegion>();
        foreach (var region in regions){
            if (region.IsLandRegion()) result.Add(await _LandRegionRepository.ReadAsync(region.Id));
            if (region.IsWaterRegion()) result.Add(await _WaterRegionRepository.ReadAsync(region.Id));
        }
        
        return result;
    }

    public override async Task<List<ARegion>> ReadAllAsync(){
        List<ARegion> regions = await base.ReadAllAsync();
        if (regions.Count == 0) return regions;
        
        List<ARegion> result = new List<ARegion>();
        
        foreach (var region in regions){
            if (region.IsLandRegion()) result.Add(await _LandRegionRepository.ReadAsync(region.Id));
            if (region.IsWaterRegion()) result.Add(await _WaterRegionRepository.ReadAsync(region.Id));
        }

        return result;
    }

    public override async Task<List<ARegion>> ReadAsync(int start, int count){
        List<ARegion> regions = await base.ReadAsync(start, count);
        if (regions.Count == 0) return regions;
        
        List<ARegion> result = new List<ARegion>();
        
        foreach (var region in regions){
            if (region.IsLandRegion()) result.Add(await _LandRegionRepository.ReadAsync(region.Id));
            if (region.IsWaterRegion()) result.Add(await _WaterRegionRepository.ReadAsync(region.Id));
        }

        return result;
    }
}