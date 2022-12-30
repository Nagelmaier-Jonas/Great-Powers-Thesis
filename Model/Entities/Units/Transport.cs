using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Units;

[Table("TRANSPORTER")]
public class Transport : Ship{
    public List<LandUnit> Units{ get; set; } = new List<LandUnit>();

    public Transport(int movement, int cost, int attack, int defense) : base(movement, cost, attack, defense){
    }
    
    public override List<AUnit> GetSubUnits(){
        return Units.Cast<AUnit>().ToList();
    }
}