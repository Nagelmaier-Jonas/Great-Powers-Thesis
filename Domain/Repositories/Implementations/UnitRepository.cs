using System.Linq.Expressions;
using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Domain.Repositories.Implementations;

public class UnitRepository : ACreatableRepository<AUnit>, IUnitRepository{
    public LandUnitRepository _LandUnitRepository{ get; set; }
    public PlaneRepository _PlaneRepository{ get; set; }
    public ShipRepository _ShipRepository{ get; set; }

    public FactoryRepository _FactoryRepository{ get; set; }

    public UnitRepository(GreatPowersDbContext context, LandUnitRepository landUnitRepository,
        PlaneRepository planeRepository, ShipRepository shipRepository, FactoryRepository factoryRepository) : base(context){
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

    public async Task<List<AUnit>> GetPlaceableUnits(){
        List<AUnit> units = new List<AUnit>();
        units = await ReadAllAsync();

        List<AUnit> placeableUnits = new List<AUnit>();

        foreach (var u in units){
            if (u.IsPlane()){
                var p = u as APlane;
                if (p.GetLocation() is null && p.GetAircraftCarrier() is null){
                    placeableUnits.Add(p);
                }
            }

            if (u.IsLandUnit()){
                var l = u as ALandUnit;
                if (l.GetLocation() is null && l.GetTransporter() is null){
                    placeableUnits.Add(l);
                }
            }

            if (u.IsShip()){
                var s = u as AShip;
                if (s.GetLocation() is null){
                    placeableUnits.Add(s);
                }
            }
        }

        return placeableUnits;
    }

    public async Task RemoveTargetAsync(AUnit unit){
        _context.ChangeTracker.Clear();
        unit.TargetId = null;
        unit.Target = null;
        _set.Update(unit);
        await _context.SaveChangesAsync();
    }
}