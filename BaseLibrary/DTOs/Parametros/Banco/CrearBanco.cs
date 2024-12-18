using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs.Parametros.Banco;

public class CrearBanco
{
    public BancoDto? BancoDto { get; set; }
    
    public bool IsEditMode { get; set; } = false;
}