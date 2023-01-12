using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Events; 

public abstract class AEventProcessor : Dictionary<string, IEventHandler>, IEventProcessor {
    private readonly IServiceScopeFactory _scopeFactory;
    
    public AEventProcessor(IServiceScopeFactory scopeFactory){
        this._scopeFactory = scopeFactory;
    }

    public void ProcessEvent(string message) {
        var eventRecord = JsonSerializer.Deserialize<EventRecord>(message);
        var eventHandler = this[eventRecord.Type];

        eventHandler.Execute(message);
    }
}