using BaseLibrary.DTOs.PlanCta;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts;

public interface IPlanCuenta : IGenericRepositoryInterface<PlanCuentaDto>
{
    Task<PlanCuentasResponse> GenerateCodPlanCuenta(string cuentaPadre);
    Task<GeneralResponse> Delete(string id);
    Task<PlanCuentaDto?> GetById(string id);
}