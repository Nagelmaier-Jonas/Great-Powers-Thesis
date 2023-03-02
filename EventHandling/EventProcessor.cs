using EventBus.Events;
using EventHandling.EventHandler;
using Microsoft.Extensions.DependencyInjection;

namespace EventHandling;

public class EventProcessor : AEventProcessor{
    
    public EventProcessor(IServiceScopeFactory scopeFactory) : base(scopeFactory){
        this["STATE_HAS_CHANGED"] = new StateHasChangedEventHandler(scopeFactory);
        this["READY_EVENT"] = new ReadyEventHandler(scopeFactory);
    }
}