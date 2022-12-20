using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("NATION_HAS_ALLIES")]
public class Allies{
    [Column("NATION_ID")]
    public int NationId{ get; set; }

    public Nation Nation{ get; set; }
    
    [Column("ALLY_ID")]
    public int AllyId{ get; set; }

    public Nation Ally{ get; set; }
}