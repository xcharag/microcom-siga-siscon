using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.DTOs.Documentos;

public class DocumentoDto
{
    [Required(ErrorMessage = "El campo Numero del Documento es requerido")]
    public string? NroDoc { get; set; }
    
    public string? FechaDoc { get; set; }

    [Required(ErrorMessage = "El campo Nombre es requerido")]
    public string? Nombre { get; set; }
    
    public string? NroCheque { get; set; }
    
    [Required(ErrorMessage = "El campo de Glosa es requerido")]
    public string? Glosa1 { get; set; }
    
    public string? Glosa2 { get; set; }

    [Required(ErrorMessage = "El campo Moneda es requerido")]
    public string? Moneda { get; set; }

    [Required(ErrorMessage = "El campo Importe es requerido")]
    [Column(TypeName = "decimal(18, 2)")]
    public float? Importe { get; set; }
    
    public string? CreatedBy { get; set; }
    
    public string? Origen { get; set; }
    
    public string? FechaRegistro { get; set; }
    
    public string? HoraRegistro { get; set; }
    
    //FK
    public int? ProveedorCodProveedor { get; set; }
    public int? BancoCodBanco { get; set; }
}