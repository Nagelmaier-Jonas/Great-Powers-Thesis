using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Units;

[Table("AIRCRAFT_CARRIER")]
public class AircraftCarrier : Ship{
    public List<Plane>? Planes{ get; set; }
}