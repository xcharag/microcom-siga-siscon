using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs.Parametros.Banco;

public class BancoDto
{
    [Required(ErrorMessage = "El código del banco es requerido")]
    public int CodBanco { get; set; }
    
    [Required(ErrorMessage = "El nombre del banco es requerido")]
    public string? NomBanco { get; set; }
    
    [Required(ErrorMessage = "La moneda del banco es requerida")]
    public string? Moneda { get; set; }
    
    [Required(ErrorMessage = "El código del plan de cuenta es requerido")]
    public string? PlanCuenta { get; set; }
}