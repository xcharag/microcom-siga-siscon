using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Cliente : BaseEntity
{
    [Required]
    [Key]
    public string? CodCli { get; set; }
    
    [Required]
    public string? NomCli { get; set; }
    
    [Required]
    public string? Contacto { get; set; }

    [Required]
    public string? DirCli { get; set; }

    [Required]
    public string? TelCli { get; set; }

    [Required]
    public string? EmailCli { get; set; }

    [Required]
    public string? Ciudad { get; set; }

    [Required]
    public string? TipoDoc { get; set; }

    [Required]
    public int? NroDoc { get; set; }

    [Required]
    public string? CodigoEx { get; set; }

    [Required]
    public string? Complemento { get; set; }

    [Required]
    public float? LimiteCre { get; set; }

    [Required]
    public string? FacturarA { get; set; }
    
    //Many-to-One Relationship
    public TipoCliente? ClientType { get; set; }
    public int? ClientTypeCodTipoCli { get; set; }
    
    public Usuario? User { get; set; }
    public int? UserCodUsuario { get; set; }
    
    public PlanCuenta? PlanCuenta { get; set; }
    public string? PlanCuentaCodCuenta { get; set; }
}