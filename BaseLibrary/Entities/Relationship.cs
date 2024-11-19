using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Relationship
{
    //One-to-Many Relationship
    [JsonIgnore]
    public List<User>? Users { get; set; }
}