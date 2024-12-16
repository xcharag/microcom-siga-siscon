using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class TipoDoc
{
    [Required]
    [Key]
    public int CodTipoDoc { get; set; }
    
    [Required]
    public string? NomTipoDoc { get; set; }
}