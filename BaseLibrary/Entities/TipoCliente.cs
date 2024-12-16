using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class TipoCliente
{
    [Required]
    [Key]
    public int CodTipoCli { get; set; }
    
    public string? NomTipoCli { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Discount { get; set; }
    
    //One-to-Many Relationships
    [JsonIgnore] 
    public List<Cliente>? Clientes { get; set; } = new List<Cliente>();
}