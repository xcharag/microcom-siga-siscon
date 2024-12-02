using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Usuario : BaseEntity
{
    [Required]
    [Key]
    public int? CodUsuario { get; set; }
    
    [Required]
    public string? Username { get; set; }
    
    [Required]
    public string? Password { get; set; }
    
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    
    //One-to-Many Relationships
    [JsonIgnore] 
    public List<Cliente> Clientes { get; set; } = new List<Cliente>();
    
    [JsonIgnore]
    public List<Proveedor> Proveedores { get; set; } = new List<Proveedor>();
}