using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class TipoEgreso
{
    [Required]
    [Key]
    public int? CodTipoEgreso { get; set; }
    
    public string? Descripcion { get; set; }
    
    //Many to One
    public PlanCuenta? PlanCuenta { get; set; }
    public int? CodCuenta { get; set; }
    
    [JsonIgnore]
    public List<Anexos>? Anexos { get; set; }
}