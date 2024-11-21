using System.Text.Json.Serialization;

namespace BaseLibrary.Entities.Client;

public class City : BaseEntity
{
    //One-to-Many Relationships
    [JsonIgnore]
    public List<Address>? Addresses { get; set; }
}