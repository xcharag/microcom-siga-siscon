using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class PlanCuenta : BaseEntity
{
    [Required]
    [Key]
    public int CodCuenta { get; set; }
    
    [Required]
    public string? NomCuenta { get; set; }
    
    [Required]
    public string? Moneda { get; set; }
    
    [Required]
    public string? TipoCuenta { get; set; }
    
    [Required]
    public int Nivel { get; set; }
    
    [Required]
    public string? Grupo { get; set; }
    
    //One-to-Many Relationships
    [JsonIgnore]
    public List<Cliente>? Clientes { get; set; } = new List<Cliente>();
    
    [JsonIgnore]
    public List<Proveedor>? Proveedores { get; set; } = new List<Proveedor>();
    
    [JsonIgnore]
    public List<Banco>? Bancos { get; set; } = new List<Banco>();
}