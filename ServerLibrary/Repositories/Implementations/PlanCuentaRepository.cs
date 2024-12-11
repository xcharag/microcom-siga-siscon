using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class PlanCuentaRepository(AppDbContext appDbContext) : IPlanCuenta
{
    public async Task<List<PlanCuenta>> GetAll() => await appDbContext.PlanCuentas.ToListAsync();

    public async Task<PlanCuenta?> GetById(int id) => await appDbContext.PlanCuentas.FindAsync(id);

    public async Task<GeneralResponse> Create(PlanCuenta item)
    {
        if (!await CheckId(item.CodCuenta)) return new GeneralResponse(false, "El plan de cuenta ya existe");
        var isValid = await ValidateId(item.CodCuenta);
        if (!isValid.Flag) return isValid;
        
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

    public async Task<PlanCuentasResponse> GenerateCodPlanCuenta(int cuentaPadre)
    {
        var checkCuentaPadre = await appDbContext.PlanCuentas.FindAsync(cuentaPadre);
        if (checkCuentaPadre is null) return new PlanCuentasResponse(false, 0, 0, "La cuenta padre no existe");
        if (checkCuentaPadre.TipoCuenta != "General") return new PlanCuentasResponse(false, 0, 0, "La cuenta padre no es de tipo General");
        
        //Largo y Nivel de la Cuenta padre
        int largo = checkCuentaPadre.CodCuenta.ToString().Length;
        int nivel = checkCuentaPadre.Nivel;
        
        //Siguiente Nivel
        var siguienteNivel = await appDbContext.Niveles.FindAsync(nivel+1);
        int numeroGuiones = siguienteNivel!.Largo - largo;
        
        var pattern = $"{cuentaPadre}{new string('_',numeroGuiones)}";

        var getLastAccount = await appDbContext.PlanCuentas
            .Where(pc =>
                EF.Functions.Like(pc.CodCuenta.ToString(), pattern) &&
                pc.CodCuenta.ToString().Length == siguienteNivel.Largo &&
                pc.TipoCuenta == "Detalle")
            .OrderByDescending(pc => pc.CodCuenta)
            .FirstOrDefaultAsync();

        if (getLastAccount is null)
        {
            var cantidadCeros = siguienteNivel.Cuantos - 1;
            var primerCodCuenta = $"{cuentaPadre}{new string('0',cantidadCeros)}1";
            return new PlanCuentasResponse(true, int.Parse(primerCodCuenta), nivel+1, "Codigo de Cuenta generada exitosamente");
        }
        
        var nuevoCodCuenta = getLastAccount.CodCuenta + 1;
        return new PlanCuentasResponse(true, nuevoCodCuenta, nivel+1, "Codigo de Cuenta generada exitosamente");
    }

    private static GeneralResponse NotFound() => new(false,"No se encontró el plan de cuenta");
    private static GeneralResponse Success() => new(true,"Operación exitosa");
    private async Task Commit() => await appDbContext.SaveChangesAsync();
    private async Task<bool> CheckId(int id) => await appDbContext.PlanCuentas.FindAsync(id) is null;

    private async Task<GeneralResponse> ValidateId(int id)
    {
        var idString = id.ToString();
        var niveles = await appDbContext.Niveles.ToListAsync();
        
        var existente = niveles.FirstOrDefault(x => x.Largo == idString.Length);
        if (existente is null) return new GeneralResponse(false, "Longitud de la cuenta es incorrecta");
        
        var cuentaPadre = idString.Substring(0, existente.Largo - existente.Cuantos);
        var cuentaPadreExistente = await appDbContext.PlanCuentas.FindAsync(int.Parse(cuentaPadre));
        if (cuentaPadreExistente is null) return new GeneralResponse(false, "La cuenta padre no existe");
        
        return new GeneralResponse(true, "Validación exitosa");
    }
}