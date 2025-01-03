using BaseLibrary.DTOs.Documentos;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts.Comprobantes;

public interface IDocument
{
    Task<List<DocumentoDto>> GetAllDocumentos();
    Task<List<DocumentoDto>> GetAllDocumentosDateRange(string start, string end);
    Task<List<DocumentoDto>> GetAllIngresos();
    Task<List<DocumentoDto>> GetAllEgresos();
    Task<List<DocumentoDto>> GetAllFondosFijos();
    
    Task<GeneralResponse> CreateDocumento(AsientoDto item);
    Task<GeneralResponse> UpdateDocumento(AsientoDto item);
    
    Task<GeneralResponse> DeleteDocumento(string id);
}