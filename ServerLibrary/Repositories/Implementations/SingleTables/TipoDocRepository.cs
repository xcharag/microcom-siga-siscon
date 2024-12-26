using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.SingleTables;

public class TipoDocRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<TipoDoc>
{
    public async Task<List<TipoDoc>> GetAll()
    {
        return await appDbContext.TipoDocs.ToListAsync();
    }

    public async Task<TipoDoc?> GetById(int id)
    {
        return await appDbContext.TipoDocs.FindAsync(id);
    }
    public async Task<GeneralResponse> Create(TipoDoc item)
    {
        if (await CheckId(item.CodTipoDoc!)) return new GeneralResponse(false, "El tipo de documento ya existe");
        
        appDbContext.TipoDocs.Add(item);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }
    public async Task<GeneralResponse> Update(TipoDoc item)
    {
        var tipoDoc = await appDbContext.TipoDocs.FindAsync(item.CodTipoDoc);
        if (tipoDoc is null) return new GeneralResponse(false, "No se encontró el tipo de documento");
        tipoDoc.NomTipoDoc = item.NomTipoDoc ?? tipoDoc.NomTipoDoc;
        
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }
    public async Task<GeneralResponse> Delete(int id)
    {
        var tipoDoc = await appDbContext.TipoDocs.FindAsync(id);
        if (tipoDoc is null) return new GeneralResponse(false, "No se encontró el tipo de documento");
        
        appDbContext.TipoDocs.Remove(tipoDoc);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }
    
    private async Task<bool> CheckId(int id) => await appDbContext.Niveles.FindAsync(id) is null;
    private async Task Commit() => await appDbContext.SaveChangesAsync();
}