using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class Correlativo
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    public string? Tipo { get; set; }
    
    [Required]
    public string? Detalle { get; set; }
    
    public string? Modulo { get; set; }
    
    [Required]
    public string? Valor { get; set; }
    
    public bool? Actualiza { get; set; }
    
    public int Largo { get; set; }
    
    public string? Relacion { get; set; }
}