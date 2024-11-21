using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class User : BaseEntity
{
    [Required]
    public string? Username { get; set; }
    
    [Required]
    public string? Password { get; set; }
    
    //One-to-Many Relationships
    [JsonIgnore]
    public List<Client.Client>? Clients { get; set; }
}