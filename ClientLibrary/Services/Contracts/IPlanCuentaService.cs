using BaseLibrary.DTOs.PlanCta;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts;

public interface IPlanCuentaService : IGenericServiceInterface<PlanCuentaDto>
{
    Task<PlanCuentasResponse> GenerateCodPlanCuenta(int cuentaPadre, string baseUrl);
}