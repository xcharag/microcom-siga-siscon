using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class PlanCuentaMayorRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<PlanCuentaMayor>
{
    public async Task<List<PlanCuentaMayor>> GetAll() => await appDbContext.PlanCuentaMayores.ToListAsync();

    public async Task<PlanCuentaMayor?> GetById(int id) => await appDbContext.PlanCuentaMayores.FindAsync(id);

    public async Task<GeneralResponse> Create(PlanCuentaMayor item)
    {
        if (!await CheckName(item.NomCuentaMayor!)) return new GeneralResponse(false, "El plan de cuenta mayor ya existe");
        appDbContext.PlanCuentaMayores.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(PlanCuentaMayor item)
    {
        var planCuentaMayor = await appDbContext.PlanCuentaMayores.FindAsync(item.CodCuentaMayor);
        if (planCuentaMayor is null) return NotFound();
        planCuentaMayor.NomCuentaMayor = item.NomCuentaMayor;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Delete(int id)
    {
        var planCuentaMayor = await appDbContext.PlanCuentaMayores
            .Include(pc => pc.PlanCuentas)
            .FirstOrDefaultAsync(x => x.CodCuentaMayor == id);
        if (planCuentaMayor == null) return NotFound();
        
        appDbContext.PlanCuentas.RemoveRange(planCuentaMayor.PlanCuentas!);
        appDbContext.PlanCuentaMayores.Remove(planCuentaMayor);
        await Commit();
        return Success();
    }
    
    private static GeneralResponse NotFound() => new(false,"No se encontró el plan de cuenta mayor");
    private static GeneralResponse Error() => new(false,"Ocurrió un error al procesar la solicitud");
    private static GeneralResponse Success() => new(true,"Operación exitosa");
    private async Task Commit() => await appDbContext.SaveChangesAsync();
    private async Task<bool> CheckName(string name)
    {
        var item = await appDbContext.PlanCuentaMayores.FirstOrDefaultAsync(x => x.NomCuentaMayor!.ToLower().Equals(name.ToLower()));
        return item is null;
    }
}