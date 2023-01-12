using Model.Entities.Regions;

namespace View.Components.Game.Drawer.CombatMove;

public class CombatTargets{
    public List<ARegion>? Regions{ get; set; }

    public event Action? HandleTargetChange;
    
    public void SetRegions(List<ARegion> regions){
        Regions = regions;
        HandleTargetChange?.Invoke();
    }

    public void ClearRegion(){
        Regions = null;
        HandleTargetChange?.Invoke();
    }
}