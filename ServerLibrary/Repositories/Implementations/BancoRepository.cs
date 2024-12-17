using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class BancoRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Banco>
{
    public async Task<List<Banco>> GetAll() => await appDbContext.Bancos.ToListAsync();

    public async Task<Banco?> GetById(int id) => await appDbContext.Bancos.FindAsync(id);

    public async Task<GeneralResponse> Create(Banco item)
    {
        if(await CheckId(item.CodBanco)) return new GeneralResponse(false, "El banco ya existe");
        
        appDbContext.Bancos.Add(item);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> Update(Banco item)
    {
        if(await CheckId(item.CodBanco)) return new GeneralResponse(false, "No se encontró el banco");
        
        appDbContext.Bancos.Update(item);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> Delete(int id)
    {
        var banco = await appDbContext.Bancos.FindAsync(id);
        if(banco is null) return new GeneralResponse(false, "No se encontró el banco");
        
        //Validar si el banco tiene movimientos
        var movimientos = await appDbContext.Documentos
            .Where(p => p.Banco!.CodBanco == id)
            .ToListAsync();
        if(movimientos.Count > 0) return new GeneralResponse(false, "El banco tiene movimientos asociados y no puede ser eliminado");
        
        appDbContext.Bancos.Remove(banco);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }
    
    private async Task<bool> CheckId(int id) => await appDbContext.Bancos.FindAsync(id) is null;
    private async Task Commit() => await appDbContext.SaveChangesAsync();
}