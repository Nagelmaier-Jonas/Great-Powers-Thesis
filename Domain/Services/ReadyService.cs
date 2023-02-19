namespace Domain.Services;

public class ReadyService{
    
    public event Action? HandleReadyChange;
    public void Ready(){
        HandleReadyChange?.Invoke();
    }
}