using Model.Entities.Units;

namespace Domain.Repositories.Interfaces;

public interface IAircraftCarrierRepository : ICreatableRepository<AircraftCarrier>{ Task<AircraftCarrier?> ReadGraphAsync(int Id);
}