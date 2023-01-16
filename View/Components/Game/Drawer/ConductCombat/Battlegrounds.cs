using Model.Entities.Regions;
using Model.Entities.Units.Abstract;

namespace View.Components.Game.Drawer.ConductCombat;

public class Battlegrounds{
    public List<ARegion>? Battleground{ get; set; }

    public event Action? HandleBattlegroundChange;
    
    public void SetBattlegrounds(List<ARegion> regions){
        Battleground = regions;
        HandleBattlegroundChange?.Invoke();
    }

    public void ClearBattlegrounds(){
        Battleground = null;
        HandleBattlegroundChange?.Invoke();
    }
}