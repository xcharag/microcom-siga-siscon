using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class AccountBase
{
    [Required]
    public string? Username { get; set; }
    
    [DataType(DataType.Password)]
    [Required]
    public string? Password { get; set; }
}