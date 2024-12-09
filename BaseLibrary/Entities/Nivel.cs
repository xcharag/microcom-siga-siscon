using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class Nivel : BaseEntity
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    public int CodNivel { get; set; }
    public int Largo { get; set; }
    public int Cuantos { get; set; }
}