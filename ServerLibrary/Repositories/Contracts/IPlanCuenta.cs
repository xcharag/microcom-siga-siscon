using BaseLibrary.DTOs.PlanCta;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts;

public interface IPlanCuenta : IGenericRepositoryInterface<PlanCuentaDto>
{
    Task<PlanCuentasResponse> GenerateCodPlanCuenta(int cuentaPadre);
}