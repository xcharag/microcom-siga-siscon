using BaseLibrary.Entities;

namespace BaseLibrary.DTOs.PlanCta;

public class CrearPlanCuenta
{
    public PlanCuentaDto? PlanCuenta { get; set; }
    public List<Grupo>? ListaGrupos { get; set; }
    public List<PlanCuenta>? PlanCuentasCompletas { get; set; }
}