using System.Text.Json.Serialization;

namespace BaseLibrary.Entities.Client;

public class DocumentType : BaseEntity
{
    //One-to-Many Relationships
    [JsonIgnore]
    public List<Document>? Documents { get; set; }
}