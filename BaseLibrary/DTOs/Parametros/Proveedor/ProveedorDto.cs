using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.DTOs.Parametros.Proveedor;

public class ProveedorDto
{
    public int? CodProveedor { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio")]
    public string? NomProv { get; set; }

    [Required(ErrorMessage = "El campo Dirección es obligatorio")]
    public string? DirProv { get; set; }

    [Required(ErrorMessage = "El campo Teléfono es obligatorio")]
    [RegularExpression(@"^\d+$", ErrorMessage = "El campo NroDoc solo puede contener números")]
    public string? TelProv { get; set; }

    [Required(ErrorMessage = "El campo Email es obligatorio")]
    public string? EmailProv { get; set; }

    [Required(ErrorMessage = "El campo Contacto es obligatorio")]
    public string? Contacto { get; set; }
    
    [Required(ErrorMessage = "El campo Tipo de Documento es obligatorio")]
    public string? TipoDoc { get; set; }

    
    [Required(ErrorMessage = "El Número de Documento es obligatorio")]
    [RegularExpression(@"^\d+$", ErrorMessage = "El campo NroDoc solo puede contener números")]
    public string? NroDoc { get; set; }
    
    [RegularExpression(@"^[01]$", ErrorMessage = "El Codigo de Excepcion solo puede ser 0 o 1")]
    public string? CodigoEx { get; set; }
    public string? Complemento { get; set; }

    //Make sure LimiteCre is a float number
    [Required(ErrorMessage = "El campo Limite de Credito es obligatorio")]
    public float? LimiteCre { get; set; }
    
    [Required(ErrorMessage = "El Plan Cuenta por Pagar asignado es obligatorio")]
    public string? PlanCuentaCodCuenta { get; set; }
}