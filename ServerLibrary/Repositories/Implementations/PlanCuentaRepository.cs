using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class PlanCuentaRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<PlanCuenta>
{
    public async Task<List<PlanCuenta>> GetAll() => await appDbContext.PlanCuentas.ToListAsync();

    public async Task<PlanCuenta?> GetById(int id) => await appDbContext.PlanCuentas.FindAsync(id);

    public async Task<GeneralResponse> Create(PlanCuenta item)
    {
        if (!await CheckName(item.NomCuenta!)) return new GeneralResponse(false, "El plan de cuenta ya existe");
        appDbContext.PlanCuentas.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(PlanCuenta item)
    {
        var planCuenta = await appDbContext.PlanCuentas.FindAsync(item.CodCuenta);
        if (planCuenta is null) return NotFound();
        planCuenta.NomCuenta = item.NomCuenta;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Delete(int id)
    {
        var planCuenta = await appDbContext.PlanCuentas.FindAsync(id);
        if (planCuenta == null) return NotFound();
        
        appDbContext.PlanCuentas.Remove(planCuenta);
        await Commit();
        return Success();
    }
    
    private static GeneralResponse NotFound() => new(false,"No se encontró el plan de cuenta");
    private static GeneralResponse Error() => new(false,"Ocurrió un error al procesar la solicitud");
    private static GeneralResponse Success() => new(true,"Operación exitosa");
    private async Task Commit() => await appDbContext.SaveChangesAsync();
    private async Task<bool> CheckName(string name) => await appDbContext.PlanCuentas.FirstOrDefaultAsync(x => x.NomCuenta!.ToLower().Equals(name.ToLower())) is null;
}