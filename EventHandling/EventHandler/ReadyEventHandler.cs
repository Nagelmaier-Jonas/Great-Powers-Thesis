using Domain.Services;
using EventBus.Events;
using Microsoft.Extensions.DependencyInjection;

namespace EventHandling.EventHandler;

public class ReadyEventHandler : IEventHandler{

    private readonly ReadyService _readyService;

    public ReadyEventHandler(IServiceScopeFactory serviceScopeFactory){
        _readyService = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ReadyService>();
    }
    public void Execute(){
        _readyService.Ready();
    }
}