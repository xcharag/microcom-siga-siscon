namespace BaseLibrary.Entities;

public class User : BaseEntity
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    
    public string? Role { get; set; }
    
    //One-to-Many Relationships
    public List<Client.Client>? Clients { get; set; }
}