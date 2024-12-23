using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.DTOs.Parametros.Proveedor;

public class ProveedorDto
{
    public int? CodProveedor { get; set; }

    [Required]
    public string? NomProv { get; set; }

    [Required]
    public string? DirProv { get; set; }

    [Required]
    public string? TelProv { get; set; }

    [Required]
    public string? EmailProv { get; set; }

    [Required]
    public string? Contacto { get; set; }
    
    [Required]
    public string? TipoDoc { get; set; }

    [Required]
    public string? NroDoc { get; set; }

    [Required]
    public string? CodigoEx { get; set; }

    [Required]
    public string? Complemento { get; set; }

    [Required]
    public decimal? LimiteCre { get; set; }
    
    public string? PlanCuentaCodCuenta { get; set; }
}