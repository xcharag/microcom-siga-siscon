using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Documento
{
    [Required]
    [Key]
    public string? NroDoc { get; set; }
    
    public string? FechaDoc { get; set; }

    [Required]
    public string? Nombre { get; set; }
    
    public string? NroCheque { get; set; }
    
    [Required]
    public string? Glosa1 { get; set; }
    
    public string? Glosa2 { get; set; }

    [Required]
    public string? Moneda { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Importe { get; set; }
    
    public string? CreatedBy { get; set; }
    
    public string? Origen { get; set; }
    
    public string? FechaRegistro { get; set; }
    
    public string? HoraRegistro { get; set; }

    //One-to-Many Relationship
    [JsonIgnore]
    public List<DetalleDocumento>? DetalleDocumentos { get; set; }
    
    //Many-to-One Relationship
    public Proveedor? Proveedor { get; set; }
    public int? ProveedorCodProveedor { get; set; }
    public Banco? Banco { get; set; }
    public int? BancoCodBanco { get; set; }
}