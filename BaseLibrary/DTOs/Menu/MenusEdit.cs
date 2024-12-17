namespace BaseLibrary.DTOs.Menu;

public class MenusEdit
{
    public int MenuId { get; set; }
    public string? MenuName { get; set; }
    public string? IconName { get; set; }
    public List<int>? DeleteMenuItems { get; set; }
    public List<MenuItemsDto>? AddMenuItems { get; set; }
    public List<MenuItemsDto>? UpdateMenuItems { get; set; }
}