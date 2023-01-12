namespace View.Services;

public class ViewRefreshService{
    public event Action? HandleViewRefreshChange;
    
    public void Refresh(){
        HandleViewRefreshChange?.Invoke();
    }
}