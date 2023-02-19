using Domain.Services;
using EventBus.Events;
using Microsoft.Extensions.DependencyInjection;

namespace EventHandling.EventHandler;

public class ReadyEventHandler : IEventHandler{

    private readonly ReadyService readyService;

    public ReadyEventHandler(IServiceScopeFactory serviceScopeFactory){
        readyService = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ReadyService>();
    }
    public void Execute(){
        readyService.Ready();
    }
}