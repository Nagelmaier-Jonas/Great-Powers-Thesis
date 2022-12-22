using Model.Entities.Regions;

namespace View.Services;

public class ActiveRegion{
    public ARegion? Region{ get; set; }

    public event Action? HandleRegionChange;
    
    public void RefreshRegion(ARegion region){
        Region = region;
        HandleRegionChange?.Invoke();
    }

    public void ClearRegion(){
        Region = null;
        HandleRegionChange?.Invoke();
    }
}