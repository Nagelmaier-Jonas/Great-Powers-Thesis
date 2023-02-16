using EventBus.Events;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace EventHandling.EventHandler;

public class ReadyEventHandler : IEventHandler{
    private NavigationManager Navigation{ get; set; }
    public ReadyEventHandler(IServiceScopeFactory factory){
        Navigation = factory.CreateScope().ServiceProvider.GetRequiredService<NavigationManager>();
    }
    public void Execute(){
        Navigation.NavigateTo("/game");
    }
}