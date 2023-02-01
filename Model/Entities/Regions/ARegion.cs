using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Model.Entities.Regions;

[Table("REGIONS_BT")]
public abstract class ARegion{
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("NAME", TypeName = "VARCHAR(45)")]
    public string Name{ get; set; } = String.Empty;

    public List<Neighbours> Neighbours{ get; set; } = new();

    [Column("IDENTIFIER", TypeName = "VARCHAR(45)")] 
    public ERegion Identifier{ get; set; }
    
    [Column("POSITION_X")]
    public int? PositionX{ get; set; }
    
    [Column("POSITION_Y")]
    public int? PositionY{ get; set; }

    public List<APlane> StationedPlanes{ get; set; } = new();
    
    public List<AUnit> IncomingUnits{ get; set; } = new();

    public List<AUnit> GetStationedFriendlyUnits(Nation nation) => GetStationedUnits()
        .Where(u => u.Nation == nation || u.Nation.Allies.Any(a => a.Ally == nation)).ToList();

    public bool ContainsAnyEnemies(Nation nation) => GetStationedUnits()
        .Any(u => u.Nation != nation && u.Nation.Allies.All(a => a.Ally != nation));

    public bool ContainsEnemies(Nation nation) => GetStationedUnits()
        .Any(u => u.Nation != nation && u.Nation.Allies.All(a => a.Ally != nation) && !u.IsSubmarine() && !u.IsTransport());

    public bool IsHostile(Nation nation){
        if (IsWaterRegion()) return ContainsEnemies(nation);
        return GetOwner() != nation && GetOwner().Allies.All(a => a.Ally != nation);
    }

    public List<AircraftCarrier> GetOpenAircraftCarriers(Nation nation){
        List<AUnit> carriers = GetStationedUnits().Where(u =>
            u.IsAircraftCarrier() &&
            (u.Nation == nation || nation.Allies.Any(a => a.Ally == u.Nation)) && u.Target == null).ToList();
        
        carriers.AddRange(IncomingUnits.Where(u =>
                u.IsAircraftCarrier() &&
                (u.Nation == nation || nation.Allies.Any(a => a.Ally == u.Nation))).ToList());
        
        List<AircraftCarrier> result = carriers.Cast<AircraftCarrier>().ToList();
        
        int incomingUnits = IncomingUnits.Where(u => u.IsFighter() ||u.IsBomber()).ToList().Count;
        
        List<AircraftCarrier> openCarriers = new List<AircraftCarrier>();
        
        foreach (var aircraftCarrier in result){
            incomingUnits += aircraftCarrier.Planes.Count - 2;
            if (incomingUnits < 0) openCarriers.Add(aircraftCarrier);
        }
        
        return openCarriers;
    }
    
    public List<Transport> GetOpenTransports(Nation nation, EPhase phase){
        List<AUnit> transports = GetStationedUnits().Where(u =>
            u.IsTransport() &&
            (u.Nation == nation || nation.Allies.Any(a => a.Ally == u.Nation) && ((u.Target != null && u.Target.IsLandRegion()) || phase == EPhase.NonCombatMove))).ToList();
        
        transports.AddRange(IncomingUnits.Where(u =>
            u.IsTransport() &&
            (u.Nation == nation || nation.Allies.Any(a => a.Ally == u.Nation))).ToList());
        
        List<Transport> result = transports.Cast<Transport>().ToList();
        
        int incomingUnits = IncomingUnits.Where(u => u.IsTank() || u.IsInfantry() || u.IsArtillery() || u.IsAntiAir()).ToList().Count;

        List<Transport> openTransports = new List<Transport>();

        foreach (var transporter in result){
            incomingUnits += transporter.Units.Count - 2;
            if (incomingUnits < 0) openTransports.Add(transporter);
        }
        
        return openTransports;
    }

    public int GetUnitCount(AUnit unit) => GetStationedUnitCounts().Where(d => d.Key.IsSameType(unit))
        .ToDictionary(p => unit).Values.First().Value;
    
    public int GetMovableUnitCount(AUnit unit) => GetMovableUnitCounts().Where(d => d.Key.IsSameType(unit))
        .ToDictionary(p => unit).Values.First().Value;

    public Dictionary<AUnit, int> GetStationedUnitCounts(){
        Dictionary<AUnit, int> counts = new Dictionary<AUnit, int>();
        foreach (var type in GetOneStationedUnitPerType()){
            if(counts.Keys.Any(u => u.IsSameType(type))) continue;
            int count = GetStationedUnits().Count(unit => unit.IsSameType(type));
           counts[type] = count;
        }
        return counts;
    }
    
    public Dictionary<AUnit, int> GetMovableUnitCounts(){
        Dictionary<AUnit, int> counts = new Dictionary<AUnit, int>();
        foreach (var type in GetOneMovableUnitPerType()){
            if(counts.Keys.Any(u => u.IsSameType(type))) continue;
            int count = GetStationedUnits().Count(unit => unit.IsSameType(type) && unit.CanMove);
            counts[type] = count;
        }
        return counts;
    }

    public List<AUnit> GetOneStationedUnitPerType(){
        List<AUnit> oneUnitPerType = new List<AUnit>();
        foreach (var unit in GetStationedUnits()){
            if (oneUnitPerType.Any(u => unit.IsSameType(u))) continue;
            oneUnitPerType.Add(unit);
        }
        return oneUnitPerType;
    }
    
    public List<AUnit> GetOneMovableUnitPerType(){
        List<AUnit> oneUnitPerType = new List<AUnit>();
        foreach (var unit in GetStationedUnits().Where(u => u.CanMove)){
            if (oneUnitPerType.Any(u => unit.IsSameType(u))) continue;
            oneUnitPerType.Add(unit);
        }
        return oneUnitPerType;
    }
    public virtual List<AUnit> GetStationedUnits() => null;

    public virtual Nation? GetOwner() => null;
    public virtual int? GetOwnerId() => null;
    public virtual int GetIncome() => 0;
    public virtual Capital GetCapital() => null;
    public virtual Factory GetFactory() => null;
    public virtual bool IsLandRegion() => false;
    public virtual bool IsWaterRegion() => false;
    public virtual int GetTroopsMobilized() => 0;
    public virtual void IncreaseTroopsMobilized(){}
    public virtual void ResetTroopsMobilized(){}
}