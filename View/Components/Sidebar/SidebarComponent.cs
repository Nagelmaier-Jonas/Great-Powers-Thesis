using Domain.Services;
using Microsoft.AspNetCore.Components;
using Model.Entities;

namespace View.Components.Sidebar;

public class SidebarComponent : ComponentBase{
    public List<SidebarItem> Items{ get; set; } = new();
    [Inject] public SidebarService Service{ get; set; }
    protected override void OnInitialized(){
        Service.RefreshSidebar(Items);
        StateHasChanged();
    }
    protected override Task OnInitializedAsync(){
        Service.RefreshSidebar(Items);
        StateHasChanged();
        return Task.CompletedTask;
    }
}