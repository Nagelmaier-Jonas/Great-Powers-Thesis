namespace Model.Entities;

public class SidebarItem{
    
    public string Name { get; set; }

    public string Icon{ get; set; } = "";
    
    public string BgImagePath { get; set; } = "";

    public Action? OnClick{ get; set; }

    public string? Link{ get; set; }
}