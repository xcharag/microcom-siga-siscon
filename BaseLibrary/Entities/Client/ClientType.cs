using System.Text.Json.Serialization;

namespace BaseLibrary.Entities.Client;

public class ClientType : BaseEntity
{
    public decimal? Discount { get; set; }
    
    //One-to-Many Relationships
    [JsonIgnore]
    public List<Client>? Clients { get; set; }
}