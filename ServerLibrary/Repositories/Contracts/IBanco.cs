using BaseLibrary.DTOs.Parametros.Banco;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts;

public interface IBanco : IGenericRepositoryInterface<BancoDto>
{
    Task<GeneralResponse> GenerateCodBanco(string codPlanCuenta);
}