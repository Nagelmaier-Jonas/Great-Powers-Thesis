using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Model.Entities.Units;

[Table("AIRCRAFT_CARRIER")]
public class AircraftCarrier : Ship{
    public List<Plane> Planes{ get; set; } = new List<Plane>();

    public AircraftCarrier(int movement, int cost, int attack, int defense) : base(movement, cost, attack, defense){
    }

    public override List<AUnit> GetSubUnits(){
        return Planes.Cast<AUnit>().ToList();
    }
}