using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.Entities;

public class Anexos
{
    [Required]
    [Key]
    public int? NroAnexo { get; set; }

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
    public decimal? Importe { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Sujeto { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Mtoiva { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Ice { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Excento { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Descuento { get; set; }
    
    //Many to One
    public DetalleDocumento? DetalleDocumento { get; set; }
    
    public TipoEgreso? TipoEgreso { get; set; }

    public Proveedor? Proveedor { get; set; }
}