using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("SETTINGS")]
public class Settings{
    [Column("STANDARD_VICTORY")]
    public int StandardVictory{ get; set; }
    
    [Column("TOTAL_VICTORY")]
    public int TotalVictory{ get; set; }

    [Column("DICE_MODE")]
    public EDiceMode DiceMode{ get; set; }
}