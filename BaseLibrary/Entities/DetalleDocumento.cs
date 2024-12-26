using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.Entities;

public class DetalleDocumento
{
    [Required]
    [Key]
    public string? NroDetalleDoc { get; set; }

    [Required]
    public string? Correl { get; set; }

    [Required]
    public string? Tipo { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Mtobd { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Mtodd { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Mtobh { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Mtodh { get; set; }

    [Required]
    public string? Glosa { get; set; }
    
    //Many to One
    public TcCosto? TcCosto { get; set; }
    public string? TcCostoCodCc { get; set; }
    
    public Documento? Documento { get; set; }
    public string? DocumentoNroDoc { get; set; }
    
    public PlanCuenta? PlanCuenta { get; set; }
    public string? PlanCuentaCodCuenta { get; set; }
    
    //One to Many
    public List<Anexos>? Anexos { get; set; }
}