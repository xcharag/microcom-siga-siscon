using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs.Parametros.CentroCosto;

public class CentroCostoDto
{
    [Required]
    public string? CodCc { get; set; }

    [Required]
    public string? NomCc { get; set; }

    [Required]
    public string? NomResp { get; set; }
}