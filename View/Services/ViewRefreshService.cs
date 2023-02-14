using System.Text.Json;
using DataTransfer;
using EventBus.Clients;

namespace View.Services;

public class ViewRefreshService{
    public event Action? HandleViewRefreshChange;

    public EventPublisher _EventPublisher{ get; set; }
    
    public ViewRefreshService(EventPublisher eventPublisher){
        _EventPublisher = eventPublisher;
    }
    
    public void Refresh(){
        HandleViewRefreshChange?.Invoke();
        _EventPublisher.Publish(JsonSerializer.Serialize(new StateHasChangedEvent()));  
    }
}