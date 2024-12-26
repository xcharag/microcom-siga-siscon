using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.SingleTables;

public class NivelesRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Nivel>
{
    public async Task<List<Nivel>> GetAll() => await appDbContext.Niveles.ToListAsync();

    public async Task<Nivel?> GetById(int id) => await appDbContext.Niveles.FindAsync(id);

    public async Task<GeneralResponse> Create(Nivel item)
    {
        if (await CheckId(item.Id!)) return new GeneralResponse(false, "El nivel ya existe");
        
        appDbContext.Niveles.Add(item);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> Update(Nivel item)
    {
        var nivel = await appDbContext.Niveles.FindAsync(item.Id);
        if (nivel is null) return new GeneralResponse(false, "No se encontró el nivel");
        nivel.Cuantos = item.Cuantos is not 0 ? item.Cuantos : nivel.Cuantos;
        nivel.Largo = item.Largo is not 0 ? item.Largo : nivel.Largo;
        
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> Delete(int id)
    {
        var nivel = await appDbContext.Niveles.FindAsync(id);
        if (nivel is null) return new GeneralResponse(false, "No se encontró el nivel");
        
        appDbContext.Niveles.Remove(nivel);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }
    
    private async Task<bool> CheckId(int id) => await appDbContext.Niveles.FindAsync(id) is null;
    private async Task Commit() => await appDbContext.SaveChangesAsync();
}