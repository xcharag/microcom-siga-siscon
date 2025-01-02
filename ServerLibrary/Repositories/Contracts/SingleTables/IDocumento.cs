using BaseLibrary.DTOs.Documentos;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts.SingleTables;

public interface IDocumento
{
    Task<List<Documento>> GetAllDocumentos();
    Task<List<DetalleDocumento>> GetAllDetalleDocumentosFromDocumento(string id);
    Task<List<Anexos>> GetAllAnexosFromDetalleDocumento(string id);
    
    Task<DocumentosCompletosDto> GetByIdDocumento(string id);
    Task<DocumentosDetalleDto> GetByIdDetalle(string id);
    Task<Anexos> GetByIdAnexo(string id);
    
    Task<GeneralResponse> CreateIngreso(DocumentosCompletosDto item);
    Task<GeneralResponse> CreateEgresoTraspaso(DocumentosCompletosDto item);
    
    Task<GeneralResponse> UpdateIngreso(DocumentosCompletosDto item);
    Task<GeneralResponse> UpdateEgresoTraspaso(DocumentosCompletosDto item);
    
    Task<GeneralResponse> DeleteAsiento(string id);
    Task<GeneralResponse> DeleteDetalle(string id);
    Task<GeneralResponse> DeleteAnexo(string id);
}