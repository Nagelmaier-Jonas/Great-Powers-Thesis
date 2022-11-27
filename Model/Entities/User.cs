using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Model.Entities;

[Table("USER")]
public class User : IdentityUser{
    public List<Nation> Nations{ get; set; }
}