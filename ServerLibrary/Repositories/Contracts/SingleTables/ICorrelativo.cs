using BaseLibrary.DTOs.Correlativos;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts.SingleTables;

public interface ICorrelativo
{
    Task<Correlativo?> GetOneCorrelativo(string tipo);
    Task<CorrelativosDto> NextCorrelativo(string tipo);
    Task<GeneralResponse> UpdateOneDocumentoCorrelativo(CorrelativosDto correlativo);
    Task<GeneralResponse> UpdateAllToGestion(GestionDto gestion);
}