using Model.Entities.Regions;
using Model.Entities.Units.Abstract;

namespace View.Components.Game.Drawer.MobilizeNewUnits;

public class MobilizeUnit{

    public AUnit? Unit{ get; set; }
    
    public List<ARegion>? Regions { get; set; }

    public event Action? HandleMobilization;
    
    public void SetUnit(AUnit unit){
        Unit = unit;
        HandleMobilization?.Invoke();
    }
    
    public void ClearUnit(){
        Unit = null;
        HandleMobilization?.Invoke();
    }
    
    public void SetRegions(List<ARegion> regions){
        Regions = regions;
        HandleMobilization?.Invoke();
    }
    
    public void ClearRegions(){
        Regions = null;
        HandleMobilization?.Invoke();
    }
}