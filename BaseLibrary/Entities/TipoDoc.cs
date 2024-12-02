using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class TipoDoc : BaseEntity
{
    [Required]
    [Key]
    public int CodTipoDoc { get; set; }
    
    [Required]
    public string? NomTipoDoc { get; set; }
    
    //One-to-Many Relationships
    [JsonIgnore]
    public List<Cliente>? Clientes { get; set; } = new List<Cliente>();
    
    [JsonIgnore]
    public List<Proveedor>? Proveedores { get; set; } = new List<Proveedor>();
}