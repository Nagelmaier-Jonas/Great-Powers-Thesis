using EventBus.Events;

namespace EventHandling.EventHandler;

public class ReadyEventHandler : IEventHandler{

    public event Action? HandleReadyEvent;
    public void Execute(){
        HandleReadyEvent?.Invoke();
    }
}