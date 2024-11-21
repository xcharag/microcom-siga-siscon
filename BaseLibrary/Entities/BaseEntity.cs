using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class BaseEntity
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? CreatedBy { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public string? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}