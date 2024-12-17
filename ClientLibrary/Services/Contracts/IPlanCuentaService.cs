using BaseLibrary.DTOs.PlanCta;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts;

public interface IPlanCuentaService : IGenericServiceInterface<PlanCuentaDto>
{
    Task<PlanCuentasResponse> GenerateCodPlanCuenta(string cuentaPadre, string baseUrl);
    Task<PlanCuentaDto?> GetById(string id, string baseUrl);
    Task<GeneralResponse> Delete(string id, string baseUrl);
}