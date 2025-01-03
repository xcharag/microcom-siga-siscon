using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Banco
{
    [Required]
    [Key]
    public int CodBanco { get; set; }
    
    [Required]
    public string? NomBanco { get; set; }
    
    [Required]
    public string? Moneda { get; set; }
    
    //Many-to-One Relationship
    public PlanCuenta? PlanCuenta { get; set; }
    public string? PlanCuentaCodCuenta { get; set; }
    
    //One-to-Many Relationship
    [JsonIgnore]
    public List<Documento>? Documentos { get; set; }
}