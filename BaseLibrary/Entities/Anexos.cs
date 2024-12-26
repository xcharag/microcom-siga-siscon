using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.Entities;

public class Anexos
{
    [Required]
    [Key]
    public string? NroAnexo { get; set; }

    [Required]
    public int? Correl { get; set; }

    [Required]
    public string? Nit { get; set; }

    [Required]
    public DateTime? FechaFactura { get; set; }

    [Required]
    public int? NroAutorizacion { get; set; }

    [Required]
    public int? NroFactura { get; set; }

    [Required]
    public string? Glosa { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Importe { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Sujeto { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Mtoiva { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Ice { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Excento { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Descuento { get; set; }
    
    //Many to One
    public DetalleDocumento? DetalleDocumento { get; set; }
    public string? DetalleDocumentoNroDetalleDoc { get; set; }
    
    public TipoEgreso? TipoEgreso { get; set; }
    public string? TipoEgresoCodTipoEgreso { get; set; }

    public Proveedor? Proveedor { get; set; }
    public int? ProveedorCodProveedor { get; set; }
}