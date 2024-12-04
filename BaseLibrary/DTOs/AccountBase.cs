using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class AccountBase
{
    [Required(ErrorMessage = "Debe de Ingresar un Usuario")]
    [StringLength(20)]
    public string? Username { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Debe de Ingresar su Clave/Contraseña")]
    public string? Password { get; set; }
}