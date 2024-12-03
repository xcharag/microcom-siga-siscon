using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class BaseEntity
{
    public string? CreatedBy { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}