using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Model.Entities.Units;

namespace Model.Entities;

[Table("USER")]
public class User : IdentityUser{
    public List<Nation> Nations{ get; set; }
}