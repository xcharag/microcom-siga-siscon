using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.Entities;

public class Proveedor : BaseEntity
{
    [Required]
    [Key]
    public int? CodProv { get; set; }

    [Required]
    public string? NomProv { get; set; }

    [Required]
    public string? DirProv { get; set; }

    [Required]
    public string? TelProv { get; set; }

    [Required]
    public string? EmailProv { get; set; }

    [Required]
    public string? Contacto { get; set; }

    [Required]
    public int? NroDoc { get; set; }

    [Required]
    public string? CodigoEx { get; set; }

    [Required]
    public string? Complemento { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LimiteCre { get; set; }
    
    //Many-to-One Relationship
    public PlanCuenta? PlanCuenta { get; set; }
    
    public Usuario? Usuario { get; set; }
    
    //One-to-Many Relationship
    public List<Documento>? Documentos { get; set; }
    
    public List<Anexos>? Anexos { get; set; }
    
}