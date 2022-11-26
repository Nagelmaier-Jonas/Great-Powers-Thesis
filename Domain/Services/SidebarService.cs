using Model.Entities;

namespace Domain.Services;

public class SidebarService{
    public List<SidebarItem> Items{ get; private set; } = new ();

    public event Action? HandleSidebarChange;
    
    public void RefreshSidebar(List<SidebarItem> items){
        Items = items;
        HandleSidebarChange?.Invoke();
    }
}