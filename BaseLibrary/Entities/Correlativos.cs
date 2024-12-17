using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class Correlativo
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string? Tipo { get; set; }
    
    [Required]
    public string? Nombre { get; set; }
    
    [Required]
    public string? Modulo { get; set; }
    
    [Required]
    public string? Valor { get; set; }
}