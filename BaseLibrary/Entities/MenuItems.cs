namespace BaseLibrary.Entities;

public class MenuItems
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public string? Url { get; set; }
    
    public int? Block { get; set; }
    
    //Many to One
    public Menu? Menu { get; set; }
    public int? MenuId { get; set; }
}