using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs.PlanCta;

public class PlanCuentaDto
{
    [Required]
    public int CodCuenta { get; set; }
    
    [Required]
    public string? NomCuenta { get; set; }
    
    [Required]
    public string? Moneda { get; set; }
    
    [Required]
    public string? TipoCuenta { get; set; }
    
    [Required]
    public int Nivel { get; set; }
    
    [Required]
    public string? Grupo { get; set; }
    
    public bool Selected { get; set; }
}