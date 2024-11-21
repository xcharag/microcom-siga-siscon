using System.Text.Json.Serialization;

namespace BaseLibrary.Entities.Client;

public class ContactType : BaseEntity
{
    //One-to-Many Relationships
    [JsonIgnore]
    public List<Contact>? Contacts { get; set; }
}