using EventBus.Events;
using Microsoft.Extensions.DependencyInjection;

namespace EventHandling.EventHandler;

public class ReceivedDatabaseUpdate : IEventHandler{
    
    
    public event Action? HandleViewRefreshChange;

    public ReceivedDatabaseUpdate(){
        
    }
    
    public void Execute(string message){ 
        HandleViewRefreshChange?.Invoke();
    }
    
}