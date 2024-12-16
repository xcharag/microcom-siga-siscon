using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.Entities;

public class DetalleDocumento
{
    [Required]
    [Key]
    public int? NroDetalleDoc { get; set; }

    [Required]
    public int? Correl { get; set; }

    [Required]
    public string? Tipo { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Mtobd { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Mtodd { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Mtobh { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Mtodh { get; set; }

    [Required]
    public string? Glosa { get; set; }
    
    //Many to One
    public TcCosto? TcCosto { get; set; }
    
    public Documento? Documento { get; set; }
    
    //One to Many
    public List<Anexos>? Anexos { get; set; }
}