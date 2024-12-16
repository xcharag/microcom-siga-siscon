using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Documento
{
    [Required]
    [Key]
    public int? NroDoc { get; set; }

    [Required]
    public DateTime? FechaDoc { get; set; }

    [Required]
    public string? Nombre { get; set; }

    [Required]
    public string? NroCheque { get; set; }

    [Required]
    public string? Moneda { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Importe { get; set; }

    [Required]
    public string? CreatedBy { get; set; }

    [Required]
    public string? Origen { get; set; }

    [Required]
    public DateTime? FechaRegistro { get; set; }

    [Required]
    public DateTime? HoraRegistro { get; set; }

    //One-to-Many Relationship
    [JsonIgnore]
    public List<DetalleDocumento>? DetalleDocumentos { get; set; }
    
    //Many-to-One Relationship
    public Proveedor? Proveedor { get; set; }
    public Banco? Banco { get; set; }
}