using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("SESSION_INFO")]
public class SessionInfo{
    [Column("SESSION_INFO_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [Column("STANDARD_VICTORY", TypeName = "TINYINT")]
    public bool StandardVictory{ get; set; } = true;
    
    [Column("TOTAL_VICTORY", TypeName = "TINYINT")]
    public bool TotalVictory{ get; set; }

    [Column("ROUND")]
    public int Round{ get; set; }
    
    [Column("PHASE", TypeName = "VARCHAR(45)")]
    public EPhase Phase{ get; set; }

    [Column("CURRENT_NATION_ID")]
    public int CurrentNationId{ get; set; }
    
    public Nation Nation{ get; set; }

    [Column("AXIS_CAPITALS")]
    public int AxisCapitals{ get; set; }
    
    [Column("ALLIES_CAPITALS")]
    public int AlliesCapitals{ get; set; }
}