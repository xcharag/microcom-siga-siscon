using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class GrupoRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Grupo>
{
    public async Task<List<Grupo>> GetAll() => await appDbContext.Grupos.ToListAsync();

    public async Task<Grupo?> GetById(int id) => await appDbContext.Grupos.FindAsync(id);

    public async Task<GeneralResponse> Create(Grupo item)
    {
        if (await CheckId(item.Id!)) return new GeneralResponse(false, "El grupo ya existe");
        
        appDbContext.Grupos.Add(item);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> Update(Grupo item)
    {
        var grupo = await appDbContext.Grupos.FindAsync(item.Id);
        if (grupo is null) return new GeneralResponse(false, "No se encontró el grupo");
        grupo.Nombre = item.Nombre ?? grupo.Nombre;
        
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> Delete(int id)
    {
        var grupo = await appDbContext.Grupos.FindAsync(id);
        if (grupo is null) return new GeneralResponse(false, "No se encontró el grupo");
        
        appDbContext.Grupos.Remove(grupo);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }
    

    private async Task<bool> CheckId(int id) => await appDbContext.Grupos.FindAsync(id) is null;
    private async Task Commit() => await appDbContext.SaveChangesAsync();
}