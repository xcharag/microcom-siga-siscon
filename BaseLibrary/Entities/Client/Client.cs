using System.Text.Json.Serialization;

namespace BaseLibrary.Entities.Client;

public class Client : BaseEntity
{
    public decimal? CreditLimit { get; set; }
    public string? RecipeName { get; set; }
    
    //One-to-Many Relationship
    [JsonIgnore]
    public List<Address>? Addresses { get; set; }
    
    [JsonIgnore]
    public List<Contact>? Contacts { get; set; }

    [JsonIgnore]
    public List<Document>? Documents { get; set; }
    
    //Many-to-One Relationship
    public ClientType? ClientType { get; set; }
    public int ClientTypeId { get; set; }
    
    public User? User { get; set; }
    public int UserId { get; set; }
}