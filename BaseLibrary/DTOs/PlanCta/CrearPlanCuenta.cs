using BaseLibrary.Entities;

namespace BaseLibrary.DTOs.PlanCta;

public class CrearPlanCuenta
{
    public List<Grupo>? ListaGrupos { get; set; }
    public List<PlanCuenta>? PlanCuentasCompletas { get; set; }
}