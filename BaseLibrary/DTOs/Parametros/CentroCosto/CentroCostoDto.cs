using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs.Parametros.CentroCosto;

public class CentroCostoDto
{
    [Required(ErrorMessage = "El código del centro de costo es requerido")]
    public string? CodCc { get; set; }

    [Required(ErrorMessage = "El nombre del centro de costo es requerido")]
    public string? NomCc { get; set; }

    [Required(ErrorMessage = "El nombre del responsable del centro de costo es requerido")]
    public string? NomResp { get; set; }
}