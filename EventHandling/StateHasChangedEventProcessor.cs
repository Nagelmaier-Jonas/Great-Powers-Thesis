using EventBus.Events;
using EventHandling.EventHandler;
using Microsoft.Extensions.DependencyInjection;

namespace EventHandling;

public class StateHasChangedEventProcessor : AEventProcessor{
    
    public StateHasChangedEventProcessor(IServiceScopeFactory scopeFactory) : base(scopeFactory){
        this["STATE_HAS_CHANGED"] = new StateHasChangedEventHandler();
        this["READY_EVENT"] = new ReadyEventHandler(scopeFactory);
    }
}