using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class Empresa
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string? Nombre { get; set; }
    
    [Required]
    public string? Direccion { get; set; }
    
    [Required]
    public string? Telefono { get; set; }
    
    [Required]
    public string? Logo { get; set; }
    
    [Required]
    public bool Sucursal { get; set; }
}