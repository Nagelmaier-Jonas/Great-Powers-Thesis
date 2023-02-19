using EventBus.Events;
using EventHandling.EventHandler;
using Microsoft.Extensions.DependencyInjection;

namespace EventHandling;

public class StateHasChangedEventProcessor : AEventProcessor{
    
    public StateHasChangedEventProcessor(IServiceScopeFactory scopeFactory) : base(scopeFactory){
        this["STATE_HAS_CHANGED"] = new StateHasChangedEventHandler(scopeFactory);
        this["READY_EVENT"] = new ReadyEventHandler(scopeFactory);
    }
}