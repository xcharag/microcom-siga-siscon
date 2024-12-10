using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Menu
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? IconName { get; set; }
    
    [JsonIgnore]
    public List<MenuItems>? MenuItemsList { get; set; }
}