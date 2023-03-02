using Domain.Services;
using EventBus.Events;
using Microsoft.Extensions.DependencyInjection;

namespace EventHandling.EventHandler;

public class StateHasChangedEventHandler : IEventHandler{
    
    private readonly ViewRefreshService _viewRefreshService;
    
    public StateHasChangedEventHandler(IServiceScopeFactory serviceScopeFactory){
        _viewRefreshService = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ViewRefreshService>();
    }
    public void Execute(){
        _viewRefreshService.Refresh();
    }
    
}