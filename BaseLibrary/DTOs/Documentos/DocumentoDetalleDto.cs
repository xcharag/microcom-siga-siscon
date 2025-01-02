using System.ComponentModel.DataAnnotations;
using BaseLibrary.Entities;

namespace BaseLibrary.DTOs.Documentos;

public class DocumentoDetalleDto
{
    [Required]
    public string? NroDetalleDoc { get; set; }
    
    [Required]
    public string? Correl { get; set; }
    
    [Required]
    public string? Tipo { get; set; }
    
    public float? Mtobd { get; set; }
    public float? Mtodd { get; set; }
    public float? Mtobh { get; set; }
    public float? Mtodh { get; set; }
    
    [Required]
    public string? Glosa { get; set; }
    
    //FK
    public string? TcCostoCodCc { get; set; }
    public string? DocumentoNroDoc { get; set; }
    public string? PlanCuentaCodCuenta { get; set; }
}