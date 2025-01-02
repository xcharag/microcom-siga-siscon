using BaseLibrary.DTOs.Documentos;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using BaseLibrary.Entities;
using ServerLibrary.Repositories.Contracts.SingleTables;

namespace ServerLibrary.Repositories.Implementations;

public class DocumentoRepository (AppDbContext appDbContext) : IDocumento
{
    public async Task<List<Documento>> GetAllDocumentos() => await appDbContext.Documentos.ToListAsync();
    public async Task<List<DetalleDocumento>> GetAllDetalleDocumentosFromDocumento(string id)
    {
        var checkDocumento = await appDbContext.Documentos.FindAsync(id);
        if (checkDocumento is null) return new List<DetalleDocumento>();
        
        return await appDbContext.DetalleDocumentos.Where(dd => dd.DocumentoNroDoc == id).ToListAsync();
    }
    public async Task<List<Anexos>> GetAllAnexosFromDetalleDocumento(string id)
    {
        var checkDetalle = await appDbContext.DetalleDocumentos.FindAsync(id);
        if (checkDetalle is null) return new List<Anexos>();
        
        return await appDbContext.Anexos.Where(a => a.DetalleDocumentoNroDetalleDoc == id).ToListAsync();
    }
    public async Task<DocumentosCompletosDto> GetByIdDocumento(string id)
    {
        var checkDocumento = await appDbContext.Documentos.FindAsync(id);
        if (checkDocumento is null) return new DocumentosCompletosDto();
        
        var detalles = await GetAllDetalleDocumentosFromDocumento(id);
        
        return new DocumentosCompletosDto()
        {
            Cabecera = checkDocumento,
            Detalles = detalles
        };
    }
    public async Task<DocumentosDetalleDto> GetByIdDetalle(string id)
    {
        var checkDetalle = await appDbContext.DetalleDocumentos.FindAsync(id);
        if (checkDetalle is null) return new DocumentosDetalleDto();
        
        var anexos = await GetAllAnexosFromDetalleDocumento(id);
        
        return new DocumentosDetalleDto()
        {
            Detalle = checkDetalle,
            Anexos = anexos
        };
    }
    public async Task<Anexos> GetByIdAnexo(string id) => (await appDbContext.Anexos.FindAsync(id))!;
    
    
    public Task<GeneralResponse> CreateIngreso(DocumentosCompletosDto item)
    {
        throw new NotImplementedException();
    }

    public Task<GeneralResponse> CreateEgresoTraspaso(DocumentosCompletosDto item)
    {
        throw new NotImplementedException();
    }

    public Task<GeneralResponse> UpdateIngreso(DocumentosCompletosDto item)
    {
        throw new NotImplementedException();
    }

    public Task<GeneralResponse> UpdateEgresoTraspaso(DocumentosCompletosDto item)
    {
        throw new NotImplementedException();
    }

    public Task<GeneralResponse> DeleteAsiento(string id)
    {
        throw new NotImplementedException();
    }

    public Task<GeneralResponse> DeleteDetalle(string id)
    {
        throw new NotImplementedException();
    }

    public Task<GeneralResponse> DeleteAnexo(string id)
    {
        throw new NotImplementedException();
    }
    
    private async Task<bool> CheckDocumentoId(string id) => await appDbContext.Documentos.FindAsync(id) is null;
    private async Task Commit() => await appDbContext.SaveChangesAsync();
    private static GeneralResponse Success() => new GeneralResponse(true, "Operación exitosa");
    private static GeneralResponse NotFound(string item) => new GeneralResponse(false, "No se encontró " + item);
    
}