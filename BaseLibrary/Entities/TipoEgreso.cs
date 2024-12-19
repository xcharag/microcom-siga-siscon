using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class TipoEgreso
{
    [Required]
    [Key]
    public string? CodTipoEgreso { get; set; }
    
    public string? Descripcion { get; set; }
    
    //Many to One
    public PlanCuenta? PlanCuenta { get; set; }
    public string? PlanCuentaCodCuenta { get; set; }
    
    [JsonIgnore]
    public List<Anexos>? Anexos { get; set; }
}