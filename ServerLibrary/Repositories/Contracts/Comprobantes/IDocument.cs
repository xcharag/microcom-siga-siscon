using BaseLibrary.DTOs.Documentos;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts.Comprobantes;

public interface IDocument
{
    Task<List<Documento>> GetAllDocumentos();
    Task<List<Documento>> GetAllDocumentosDateRange(string start, string end);
    
    Task<GeneralResponse> CreateDocumento(AsientoDto item);
    Task<GeneralResponse> UpdateDocumento(AsientoDto item);
    
    Task<GeneralResponse> DeleteDocumento(string id);
}