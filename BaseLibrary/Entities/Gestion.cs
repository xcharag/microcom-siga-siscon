using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class Gestion
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int Mes { get; set; }
    
    [Required]
    public int Anho { get; set; }
}