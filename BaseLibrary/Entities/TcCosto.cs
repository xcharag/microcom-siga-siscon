using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class TcCosto
{
    [Required]
    [Key]
    public string? CodCc { get; set; }

    [Required]
    public string? NomCc { get; set; }
    
    public string? NomResp { get; set; }
    
    //Many to One
    [JsonIgnore]
    public List<DetalleDocumento>? DetalleDocumentos { get; set; }
}