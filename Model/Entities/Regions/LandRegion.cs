using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Units;
using Model.Entities.Units.Abstract;

namespace Model.Entities.Regions;

[Table("LAND_REGION")]
public class LandRegion : ARegion{
    [Column("INCOME")]
    public int Income{ get; set; }
    public Capital? Capital{ get; set; }
    
    [Column("OWNER_ID")]
    public int NationId{ get; set; }

    public Nation Nation{ get; set; }

    public Factory? Factory{ get; set; }

    [Column("TROOPS_MOBILIZED")]
    public int TroopsMobilized{ get; set; }

    public List<ALandUnit> StationedUnits{ get; set; } = new List<ALandUnit>();

    public List<CanalOwners> Canals{ get; set; } = new List<CanalOwners>();

    public override List<AUnit> GetStationedUnits(){
        List<AUnit> units = new List<AUnit>();
        units.AddRange(StationedUnits);
        units.AddRange(StationedPlanes);
        return units;
    }

    public override Nation GetOwner() => Nation;
    
    public override int? GetOwnerId() => NationId;
    public override int GetIncome() => Income;
    public override Capital GetCapital() => Capital;
    public override Factory GetFactory() => Factory;
    public override bool IsLandRegion() => true;
    public override int GetTroopsMobilized() => TroopsMobilized;
    public override void ResetTroopsMobilized() => TroopsMobilized = 0;
    public override void IncreaseTroopsMobilized() => TroopsMobilized++;
}