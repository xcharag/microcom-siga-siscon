using BaseLibrary.DTOs.Parametros.Banco;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts;

public interface IBancoService : IGenericServiceInterface<BancoDto>
{
    Task<GeneralResponse> GenerateCodBanco(string codPlanCuenta, string baseUrl);    
}