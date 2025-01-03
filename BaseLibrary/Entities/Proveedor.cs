using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.Entities;

public class Proveedor : BaseEntity
{
    [Required]
    [Key]
    public int? CodProveedor { get; set; }

    [Required]
    public string? NomProv { get; set; }

    [Required]
    public string? DirProv { get; set; }

    [Required]
    public string? TelProv { get; set; }
    
    public string? EmailProv { get; set; }
    
    public string? Contacto { get; set; }

    [Required]
    public string? TipoDoc { get; set; }
    
    [Required]
    public string? NroDoc { get; set; }
    
    public string? CodigoEx { get; set; }
    
    public string? Complemento { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public float? LimiteCre { get; set; }
    
    //Many-to-One Relationship
    public PlanCuenta? PlanCuenta { get; set; }
    public string? PlanCuentaCodCuenta { get; set; }
    
    //One-to-Many Relationship
    public List<Documento>? Documentos { get; set; }
    
    public List<Anexos>? Anexos { get; set; }
}