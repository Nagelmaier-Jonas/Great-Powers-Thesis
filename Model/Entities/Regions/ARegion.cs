using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Diagnostics;
using Model.Entities.Units;

namespace Model.Entities.Regions;

[Table("REGIONS_BT")]
public abstract class ARegion{
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("NAME", TypeName = "VARCHAR(45)")]
    public string Name{ get; set; }

    public List<Neighbours> Neighbours{ get; set; }

    [Column("IDENTIFIER", TypeName = "VARCHAR(45)")] 
    public ERegion Identifier{ get; set; }
    
    [Column("POSITION_X")]
    public int? PositionX{ get; set; }
    
    [Column("POSITION_Y")]
    public int? PositionY{ get; set; }

    public List<Plane> StationedPlanes{ get; set; } = new List<Plane>();
    public List<Plane> IncomingPlanes{ get; set; } = new List<Plane>();
    
    public List<Ship> IncomingShips{ get; set; } = new List<Ship>();
    
    public List<AUnit> GetStationedFriendlyUnits(Nation nation) => GetStationedUnits()
        .Where(u => u.Nation == nation || u.Nation.Allies.Any(a => a.Ally == nation)).ToList();

    public bool ContainsAnyEnemies(Nation nation) => GetStationedUnits()
        .Any(u => u.Nation != nation && u.Nation.Allies.All(a => a.Ally != nation));

    public bool ContainsEnemies(Nation nation) => GetStationedUnits()
        .Any(u => u.Nation != nation && u.Nation.Allies.All(a => a.Ally != nation) && u.Type != EUnitType.SUBMARINE && u.Type != EUnitType.TRANSPORT);

    public bool IsHostile(Nation nation){
        if (IsWaterRegion()) return ContainsEnemies(nation);
        return GetOwner() != nation || GetOwner().Allies.All(a => a.Ally != nation);
    }

    public List<AircraftCarrier> GetOpenAircraftCarriers(Nation nation){
        List<AUnit> carriers = GetStationedUnits().Where(u =>
            u.Type == EUnitType.AIRCRAFT_CARRIER &&
            (u.Nation == nation || nation.Allies.Any(a => a.Ally == u.Nation)) && u.GetTarget() == null).ToList();
        
        carriers.AddRange(IncomingShips.Where(u =>
                u.Type == EUnitType.AIRCRAFT_CARRIER &&
                (u.Nation == nation || nation.Allies.Any(a => a.Ally == u.Nation))).ToList());
        
        List<AircraftCarrier> result = carriers.Cast<AircraftCarrier>().ToList();
        result.RemoveAll(c => c.Planes.Count >= 2);
        return result;
    }
    
    public List<Transport> GetOpenTransports(Nation nation){
        List<AUnit> carriers = GetStationedUnits().Where(u =>
            u.Type == EUnitType.TRANSPORT &&
            (u.Nation == nation || nation.Allies.Any(a => a.Ally == u.Nation))).ToList();
        
        carriers.AddRange(IncomingShips.Where(u =>
            u.Type == EUnitType.TRANSPORT &&
            (u.Nation == nation || nation.Allies.Any(a => a.Ally == u.Nation))).ToList());
        
        List<Transport> result = carriers.Cast<Transport>().ToList();
        result.RemoveAll(c => c.Units.Count >= 2);
        return result;
    }

    public Dictionary<EUnitType, int> GetStationedUnitCounts(){
        Dictionary<EUnitType, int> counts = new Dictionary<EUnitType, int>();
        foreach (var unit in GetStationedUnits()) counts[unit.Type] = counts.GetValueOrDefault(unit.Type, 0) + 1;
        return counts;
    }

    public List<AUnit> GetOneStationedUnitPerType(){
        List<AUnit> oneUnitPerType = new List<AUnit>();
        //make an exception for units with type EUnitType.TRANSPORT and EUnitType.AIRCRAFT_CARRIER
        foreach (var unit in GetStationedUnits()){
            if (oneUnitPerType.Any(u => u.Type == unit.Type && u.Type != EUnitType.TRANSPORT &&
                                        u.Type != EUnitType.AIRCRAFT_CARRIER)) continue;
            oneUnitPerType.Add(unit);
        }
        return oneUnitPerType;
    }
    public virtual List<AUnit> GetStationedUnits() => null;

    public virtual Nation? GetOwner() => null;
    public virtual int GetIncome() => 0;
    public virtual Capital GetCapital() => null;
    public virtual Factory GetFactory() => null;

    public abstract bool IsLandRegion();
    public abstract bool IsWaterRegion();
}