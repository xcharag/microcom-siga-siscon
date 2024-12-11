using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts;

public interface IPlanCuenta : IGenericRepositoryInterface<PlanCuenta>
{
    Task<PlanCuentasResponse> GenerateCodPlanCuenta(int cuentaPadre);
}