using System.Linq.Expressions;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Domain.Repositories.Implementations;

public class UnitRepository : ACreatableRepository<AUnit>, IUnitRepository{
    public ILandUnitRepository _LandUnitRepository{ get; set; }
    public IPlaneRepository _PlaneRepository{ get; set; }
    public IShipRepository _ShipRepository{ get; set; }
    public IFactoryRepository _FactoryRepository{ get; set; }

    public UnitRepository(GreatPowersDbContext context, ILandUnitRepository landUnitRepository,
        IPlaneRepository planeRepository, IShipRepository shipRepository, IFactoryRepository factoryRepository) : base(context){
        _LandUnitRepository = landUnitRepository;
        _PlaneRepository = planeRepository;
        _ShipRepository = shipRepository;
        _FactoryRepository = factoryRepository;
    }

    public override async Task<AUnit?> ReadAsync(int id){
        AUnit unit = await base.ReadAsync(id);
        if (unit is null) return null;
        if (unit.IsLandUnit()) return await _LandUnitRepository.ReadGraphAsync(unit.Id);
        if (unit.IsPlane()) return await _PlaneRepository.ReadGraphAsync(unit.Id);
        if (unit.IsShip()) return await _ShipRepository.ReadGraphAsync(unit.Id);
        return unit;
    }

    public override async Task<List<AUnit>> ReadAsync(Expression<Func<AUnit, bool>> filter){
        List<AUnit> units = await base.ReadAsync(filter);
        if (units.Count == 0) return units;

        List<AUnit> result = new List<AUnit>();
        foreach (var unit in units){
            if (unit.IsLandUnit()) result.Add(await _LandUnitRepository.ReadGraphAsync(unit.Id));
            if (unit.IsPlane()) result.Add(await _PlaneRepository.ReadGraphAsync(unit.Id));
            if (unit.IsShip()) result.Add(await _ShipRepository.ReadGraphAsync(unit.Id));
        }

        return result;
    }

    public override async Task<List<AUnit>> ReadAllAsync(){
        List<AUnit> units = await base.ReadAllAsync();
        if (units.Count == 0) return units;

        List<AUnit> result = new List<AUnit>();

        foreach (var unit in units){
            if (unit.IsLandUnit()) result.Add(await _LandUnitRepository.ReadGraphAsync(unit.Id));
            if (unit.IsPlane()) result.Add(await _PlaneRepository.ReadGraphAsync(unit.Id));
            if (unit.IsShip()) result.Add(await _ShipRepository.ReadGraphAsync(unit.Id));
        }

        return result;
    }

    public override async Task<List<AUnit>> ReadAsync(int start, int count){
        List<AUnit> units = new List<AUnit>();
        units = await base.ReadAsync(start, count);
        if (units.Count == 0) return units;

        List<AUnit> result = new List<AUnit>();

        foreach (var unit in units){
            if (unit.IsLandUnit()) result.Add(await _LandUnitRepository.ReadGraphAsync(unit.Id));
            if (unit.IsPlane()) result.Add(await _PlaneRepository.ReadGraphAsync(unit.Id));
            if (unit.IsShip()) result.Add(await _ShipRepository.ReadGraphAsync(unit.Id));
        }

        return result;
    }
}