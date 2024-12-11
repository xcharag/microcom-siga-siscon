using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts;

public interface IPlanCuentaService : IGenericServiceInterface<PlanCuenta>
{
    Task<PlanCuentasResponse> GenerateCodPlanCuenta(int cuentaPadre);
}