using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Regions;

namespace Model.Entities.Units;

[Table("UNITS_BT")]
public abstract class AUnit{

    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("UNIT_TYPE", TypeName = "VARCHAR(45)")]
    public EUnitType Type{ get; set; }

    [Column("NATION_ID")]
    public int NationId{ get; set; }
    
    public Nation Nation{ get; set; }
    
    [Column("CURRENT_MOVEMENT")] 
    public int CurrentMovement{ get; set; }
    
    public int Movement{ get; private protected set; }
    
    public int Cost{ get; private protected set; }
    public int Attack{ get; private protected set; }
    public int Defense{ get; private protected set; }

    public abstract ARegion GetLocation();
    public abstract bool SetLocation(ARegion target);
    public abstract ARegion GetTarget();
    public abstract bool SetTarget(ARegion region);
    public abstract List<ARegion> GetPathToCurrentTarget();
    public abstract bool MoveToTarget();
    protected abstract bool CanTarget(ARegion target);
    public virtual List<AUnit> GetSubUnits() => new ();



}