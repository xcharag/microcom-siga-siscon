using BaseLibrary.Entities;

namespace BaseLibrary.DTOs;

public class SelectPlanCuentas
{
    public string? TipoCuenta { get; set; } = "General";
    public List<PlanCuenta>? PlanCuentasCompletas { get; set; }
}