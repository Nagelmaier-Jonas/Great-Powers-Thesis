using EventBus.Events;
using Microsoft.Extensions.DependencyInjection;

namespace EventHandling.EventHandler;

public class StateHasChangedEventHandler : IEventHandler{
    public event Action? HandleViewRefreshChange;
    public void Execute(){
        HandleViewRefreshChange?.Invoke();
    }
    
}