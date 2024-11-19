namespace BaseLibrary.Entities.Client;

public class Address : BaseEntity
{
    public string? AddressLine { get; set; }
    public string? Notes { get; set; }
    
    //Many-to-One Relationships
    public Client? Client { get; set; }
    public int ClientId { get; set; }
    
    public City? City { get; set; }
    public int CityId { get; set; }
}