using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;
using Model.Entities.Units.Abstract;
using Model.Entities.Units;
using Model.Factories;

namespace Model.Entities.Units;

[Table("FACTORIES")]
public class Factory : AUnit{

    [Column("DAMAGE")]
    public int Damage{ get; set; }

    [Column("REGION_ID")]
    public int? RegionId{ get; set; }

    public LandRegion? Region{ get; set; }

    public override int Movement{ get; protected set; } = 0;
    public override int Cost{ get; protected set; } = 15;
    public override int Attack{ get; protected set; } = 0;
    public override int Defense{ get; protected set; } = 0;
    public int GetCost() => Cost;
    public override ARegion? GetLocation() => Region;
    
    public override ARegion? GetPreviousLocation() => null;
    public override bool SetLocation(ARegion region){
        if (region.IsLandRegion()){
            Region = (LandRegion)region;
            RegionId = region.Id;
            return true;
        }

        return false;
    }
    public override List<AUnit> GetSubUnits() => null;

    protected override bool CheckForMovementRestrictions(int distance, Neighbours target, EPhase phase,bool planeCheck) => false;
    
    public override bool IsFactory() => true;
    
    public override bool IsSameType(AUnit unit) => unit.IsFactory();
    
    public override AUnit GetNewInstanceOfSameType() => IndustryFactory.Create(null);
}