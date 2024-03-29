﻿using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Events; 

public abstract class AEventProcessor : Dictionary<string, IEventHandler>,IEventProcessor {

    protected readonly IServiceScopeFactory ScopeFactory;
    
    public AEventProcessor(IServiceScopeFactory scopeFactory) {
        ScopeFactory = scopeFactory;
    }
    public void ProcessEvent(string eventMessage) {
        var eventRecord = JsonSerializer.Deserialize<EventRecord>(eventMessage);
        var eventHandler = this[eventRecord.Type];

        eventHandler.Execute();
    }

}