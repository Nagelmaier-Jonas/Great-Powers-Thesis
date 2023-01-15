using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entities;

[Table("SESSION_INFO")]
public class SessionInfo{
    [JsonPropertyName("Id")]
    [Column("SESSION_INFO_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id{ get; set; }

    [JsonPropertyName("StandardVictory")]
    [Column("STANDARD_VICTORY", TypeName = "TINYINT")]
    public bool StandardVictory{ get; set; } = true;

    [JsonPropertyName("TotalVictory")]
    [Column("TOTAL_VICTORY", TypeName = "TINYINT")]
    public bool TotalVictory{ get; set; }

    [JsonPropertyName("Round")]
    [Column("ROUND")]
    public int Round{ get; set; }

    [JsonPropertyName("Phase")]
    [Column("PHASE", TypeName = "VARCHAR(45)")]
    public EPhase Phase{ get; set; }

    [JsonPropertyName("CurrentNationId")]
    [Column("CURRENT_NATION_ID")]
    public int CurrentNationId{ get; set; }

    [JsonPropertyName("Nation")] public Nation Nation{ get; set; }

    [JsonPropertyName("AxisCapitals")]
    [Column("AXIS_CAPITALS")]
    public int AxisCapitals{ get; set; }

    [JsonPropertyName("AlliesCapitals")]
    [Column("ALLIES_CAPITALS")]
    public int AlliesCapitals{ get; set; }

    [JsonPropertyName("Path")]
    [Column("SavePath")]
    public string? Path{ get; set; }
}