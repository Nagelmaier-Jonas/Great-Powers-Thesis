using Model.Entities.Regions;
using View.Components.Game.Drawer.CombatMove;
using View.Components.Game.Drawer.NonCombatMove;

namespace View.Services;

public class ActiveRegion{

    public CombatTargets CombatTargets{ get; set; }

    public NonCombatTargets NonCombatTargets{ get; set; }

    public ActiveRegion(CombatTargets combatTargets, NonCombatTargets nonCombatTargets){
        CombatTargets = combatTargets;
        NonCombatTargets = nonCombatTargets;
    }
    public ARegion? Region{ get; set; }

    public event Action? HandleRegionChange;
    
    public void RefreshRegion(ARegion region){
        Region = region;
        HandleRegionChange?.Invoke();
    }

    public void ClearRegion(){
        Region = null;
        CombatTargets.ClearRegions();
        CombatTargets.ClearUnits();
        NonCombatTargets.ClearRegions();
        NonCombatTargets.ClearUnits();
        HandleRegionChange?.Invoke();
    }
}