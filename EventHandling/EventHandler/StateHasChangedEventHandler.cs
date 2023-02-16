using Domain.Services;
using EventBus.Events;
using Microsoft.Extensions.DependencyInjection;

namespace EventHandling.EventHandler;

public class StateHasChangedEventHandler : IEventHandler{
    
    private readonly ViewRefreshService viewRefreshService;
    
    public StateHasChangedEventHandler(IServiceScopeFactory serviceScopeFactory){
        viewRefreshService = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ViewRefreshService>();
    }
    public void Execute(){
        viewRefreshService.Refresh();
    }
    
}