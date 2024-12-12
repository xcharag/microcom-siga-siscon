namespace BaseLibrary.DTOs.PlanCta;

public class PlanCuentasFormato
{
    public int? CodCuenta { get; set; }
    public string? NombreCuenta { get; set; }
    public string? Moneda { get; set; }
    public string? TipoCuenta { get; set; }
    public int? Nivel { get; set; }
    
    public List<PlanCuentasFormato>? SubCuentas { get; set; }
}