using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Grupo
{
    [Required]
    [Key]
    public int Id { get; set; }
    public string? CodGrupo { get; set; }
    public string? Nombre { get; set; }
}