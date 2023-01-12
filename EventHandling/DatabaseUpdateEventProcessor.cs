using EventBus.Events;
using EventHandling.EventHandler;
using Microsoft.Extensions.DependencyInjection;

namespace EventHandling;

public class DatabaseUpdateEventProcessor : AEventProcessor{
    
    
    public DatabaseUpdateEventProcessor(IServiceScopeFactory scopeFactory) : base(scopeFactory){
        this["DATABASE_UPDATE"] = new ReceivedDatabaseUpdate();
    }
}