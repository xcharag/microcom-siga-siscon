using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class Correlativo
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    public string? Tipo { get; set; } = String.Empty;
    
    [Required]
    public string? Detalle { get; set; }
    
    public string? Modulo { get; set; } = String.Empty;
    
    [Required]
    public string? Valor { get; set; }
    
    public bool? Actualiza { get; set; }
    
    public int Largo { get; set; }
    
    public string? Prefijo { get; set; } = String.Empty;
    
    public string? Sufijo { get; set; } = String.Empty;
    
    public string? Gestion { get; set; } = String.Empty;
    
    public string? Relleno { get; set; } = String.Empty;
    
    public string? Relacion { get; set; } = String.Empty;
}