using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class Rubros
{
    [Required]
    [Key]
    public int Id { get; set; }
    public string? Nombre { get; set; }
}