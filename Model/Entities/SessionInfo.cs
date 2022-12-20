using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("SESSION_INFO")]
public class SessionInfo{
    [Column("SESSION_INFO_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }
    
    [Column("STANDARD_VICTORY")]
    public int StandardVictory{ get; set; }
    
    [Column("TOTAL_VICTORY")]
    public int TotalVictory{ get; set; }

    [Column("DICE_MODE")]
    public EDiceMode DiceMode{ get; set; }

    [Column("ROUND")]
    public int Round{ get; set; }
    
    [Column("PHASE")]
    public int Phase{ get; set; }

    [Column("CURRENT_NATION_ID")]
    public int CurrentNationId{ get; set; }
    
    public Nation Nation{ get; set; }
}