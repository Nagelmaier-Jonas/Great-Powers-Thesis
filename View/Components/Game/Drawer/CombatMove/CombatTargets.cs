using Model.Entities.Regions;
using Model.Entities.Units.Abstract;

namespace View.Components.Game.Drawer.CombatMove;

public class CombatTargets{
    public List<ARegion>? Regions{ get; set; }
    public List<AUnit>? Units{ get; set; }
    public event Action? HandleTargetChange;
    public event Action? HandleUnitChange;
    
    public void SetRegions(List<ARegion> regions){
        Regions = regions;
        HandleTargetChange?.Invoke();
    }

    public void ClearRegions(){
        Regions = null;
        HandleTargetChange?.Invoke();
    }
    
    public void SetUnits(List<AUnit> units){
        Units = units;
        HandleUnitChange?.Invoke();
    }

    public void ClearUnits(){
        Units = null;
        HandleUnitChange?.Invoke();
    }
}