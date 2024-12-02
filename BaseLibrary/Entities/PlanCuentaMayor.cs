using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class PlanCuentaMayor : BaseEntity
{
    [Required]
    [Key]
    public int CodCuentaMayor { get; set; }
    
    [Required]
    public string? NomCuentaMayor { get; set; }
    
    //One-to-Many Relationships
    public List<PlanCuenta>? PlanCuentas { get; set; } = new List<PlanCuenta>();
}