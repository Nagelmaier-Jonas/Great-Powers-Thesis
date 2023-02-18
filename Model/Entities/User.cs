using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Model.Entities.Units;

namespace Model.Entities;

[Table("USER")]
public class User : IdentityUser{
    public List<Nation> Nations{ get; set; } = new();

    [Column("READY", TypeName = "TINYINT(1)")]
    public bool Ready{ get; set; } = false;
    
    [Column("IS_OWNER", TypeName = "TINYINT(1)")]
    public bool IsOwner{ get; set; } = false;
}