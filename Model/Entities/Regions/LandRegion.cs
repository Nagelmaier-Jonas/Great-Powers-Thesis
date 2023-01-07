using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Units;

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

    public List<LandUnit> StationedUnits{ get; set; } = new List<LandUnit>();
    public List<LandUnit> IncomingUnits{ get; set; } = new List<LandUnit>();
    
    public List<CanalOwners> Canals{ get; set; }

    public override List<AUnit> GetStationedUnits(){
        List<AUnit> units = new List<AUnit>();
        units.AddRange(StationedUnits);
        units.AddRange(StationedPlanes);
        return units;
    }

    public override Nation GetOwner() => Nation;
    public override int GetIncome() => Income;
    public override Capital GetCapital() => Capital;
    public override Factory GetFactory() => Factory;
    public override bool IsLandRegion() => true;

    public override bool IsWaterRegion() => false;
}