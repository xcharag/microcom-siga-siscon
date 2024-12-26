using BaseLibrary.DTOs.PlanCta;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts.Parametros;

namespace ServerLibrary.Repositories.Implementations.Parametros;

public class PlanCuentaRepository(AppDbContext appDbContext) : IPlanCuenta
{
    public async Task<List<PlanCuentaDto>> GetAll()
    {
        var planCuentaList = await appDbContext.PlanCuentas.ToListAsync();
        return planCuentaList.Select(pc => new PlanCuentaDto
        {
            CodCuenta = pc.CodCuenta,
            NomCuenta = pc.NomCuenta,
            Moneda = pc.Moneda,
            TipoCuenta = pc.TipoCuenta,
            Nivel = pc.Nivel,
            Grupo = pc.Grupo
        }).ToList();
    }

    public async Task<PlanCuentaDto?> GetById(string id)
    {
        var planCuenta = await appDbContext.PlanCuentas.FindAsync(id);
        return planCuenta is null ? null : new PlanCuentaDto
        {
            CodCuenta = planCuenta.CodCuenta,
            NomCuenta = planCuenta.NomCuenta,
            Moneda = planCuenta.Moneda,
            TipoCuenta = planCuenta.TipoCuenta,
            Nivel = planCuenta.Nivel,
            Grupo = planCuenta.Grupo!
        };
    } 
    public async Task<GeneralResponse> Create(PlanCuentaDto item)
    {
        var isValid = await ValidateId(item.CodCuenta!);
        if (!isValid.Flag) return isValid;
        
        var planCuenta = new PlanCuenta
        {
            CodCuenta = item.CodCuenta,
            NomCuenta = item.NomCuenta,
            Moneda = item.Moneda,
            TipoCuenta = item.TipoCuenta,
            Nivel = item.Nivel,
            Grupo = item.Grupo
        };
        
        appDbContext.PlanCuentas.Add(planCuenta);
        await Commit();
        return Success();
    }
    public async Task<GeneralResponse> Update(PlanCuentaDto item)
    {
        var planCuenta = await appDbContext.PlanCuentas.FindAsync(item.CodCuenta);
        if (planCuenta is null) return NotFound();
        planCuenta.NomCuenta = item.NomCuenta;
        planCuenta.Moneda = item.Moneda;
        planCuenta.TipoCuenta = item.TipoCuenta;
        planCuenta.Nivel = item.Nivel;
        planCuenta.Grupo = item.Grupo;
        await Commit();
        return Success();
    }
    public async Task<GeneralResponse> Delete(string id)
    {
        var planCuenta = await appDbContext.PlanCuentas.FindAsync(id);
        if (planCuenta == null) return NotFound();
        
        //Revisar si Cuenta tiene Hijos
        var hijos = await appDbContext.PlanCuentas
            .Where(pc => EF.Functions.Like(pc.CodCuenta!, $"{id}.%"))
            .ToListAsync();
        if (hijos.Count > 0) return new GeneralResponse(false, "La cuenta tiene cuentas hijas, no se puede borrar");
        
        //Revisar si esta en algun detalle de documento
        var detalle = await appDbContext.DetalleDocumentos
            .Where(p => p.PlanCuenta!.CodCuenta == id)
            .ToListAsync();
        if (detalle.Count > 0) return new GeneralResponse(false, "La cuenta tiene movimientos asociados, no se puede borrar");
        
        appDbContext.PlanCuentas.Remove(planCuenta);
        await Commit();
        return Success();
    }
    
    
    public async Task<PlanCuentasResponse> GenerateCodPlanCuenta(string cuentaPadre)
    {
        Console.WriteLine(cuentaPadre);
        // Revisar si Cuenta Padre Existe
        var checkCuentaPadre = await appDbContext.PlanCuentas.FindAsync(cuentaPadre);
        if (checkCuentaPadre is null) return new PlanCuentasResponse(false, null!, 0, "La cuenta padre no existe.");
        if (checkCuentaPadre.TipoCuenta != "General") return new PlanCuentasResponse(false, null!, 0, "La cuenta padre no es de tipo General.");
        
        // Largo y Nivel de la Cuenta padre
        int largoPadre = PlanCuentaHelpers.GetLargoCuenta(checkCuentaPadre.CodCuenta!);
        int nivelPadre = checkCuentaPadre.Nivel;
        
        // Siguiente Nivel
        var siguienteNivel = await appDbContext.Niveles.FindAsync(nivelPadre + 1);
        if (siguienteNivel is null) return new PlanCuentasResponse(false, null!, 0, "No existe un nivel mas alto configurado.");
        
        //Generacion de Patron de Busqueda con Guiones
        int numeroGuiones = siguienteNivel.Cuantos;
        var pattern = $"{cuentaPadre}.{new string('_', numeroGuiones)}";

        //Obtener Ultima Cuenta Hija de Detalle
        var getLastAccount = await appDbContext.PlanCuentas
            .Where(pc =>
                EF.Functions.Like(pc.CodCuenta!, pattern) &&
                pc.CodCuenta!.Replace(".", "").Length == siguienteNivel.Largo)
            .OrderByDescending(pc => pc.CodCuenta)
            .FirstOrDefaultAsync();

        //Generar Codigo de Cuenta para primer Hija
        if (getLastAccount is null)
        {
            var cantidadCeros = siguienteNivel.Cuantos - 1;
            var primerCodCuenta = $"{cuentaPadre}.{new string('0', cantidadCeros)}1";
            return new PlanCuentasResponse(true, primerCodCuenta, nivelPadre + 1, "Codigo de Cuenta generada exitosamente", true);
        }

        //Generar Codigo de Cuenta para siguiente Hija
        var lastAccountNumber = int.Parse(PlanCuentaHelpers.GetLastNivelDigits(getLastAccount.CodCuenta!, siguienteNivel.Cuantos));
        lastAccountNumber++;
        var cantidadCerosRestantes = siguienteNivel.Cuantos - lastAccountNumber.ToString().Length;
        var nuevoCodCuenta = $"{cuentaPadre}.{new string('0', cantidadCerosRestantes)}{lastAccountNumber}";
        if (getLastAccount.TipoCuenta == "Detalle") return new PlanCuentasResponse(true, nuevoCodCuenta, nivelPadre + 1, "Codigo de Cuenta generada exitosamente", false, true);
        
        return new PlanCuentasResponse(true, nuevoCodCuenta, nivelPadre + 1, "Codigo de Cuenta generada exitosamente", false, false);
    }
    private static GeneralResponse NotFound() => new(false,"No se encontró el plan de cuenta");
    private static GeneralResponse Success() => new(true,"Operación exitosa");
    private async Task Commit() => await appDbContext.SaveChangesAsync();
    private async Task<GeneralResponse> ValidateId(string id)
    {
        //Revisar si Cuenta Existe
        var lengthId = id.Replace(".", "").Length;
        var existeCuenta = await appDbContext.PlanCuentas.FindAsync(id);
        if (existeCuenta is not null) return new GeneralResponse(false, "El codigo de cuenta ya existe");
        
        //Revisar Digitos Segun Nivel
        var niveles = await appDbContext.Niveles.ToListAsync();
        var existente = niveles.FirstOrDefault(x => x.Largo == lengthId);
        if (existente is null) return new GeneralResponse(false, "Longitud de la cuenta es incorrecta");

        //Revisar Cuenta Padre Correcta
        if (lengthId > 1)
        {
            var cuentaPadre = PlanCuentaHelpers.GetCuentaPadre(id, existente.Cuantos);
            var cuentaPadreExistente = await appDbContext.PlanCuentas.FindAsync(cuentaPadre);
            if (cuentaPadreExistente is null) return new GeneralResponse(false, "La cuenta padre no existe");
            if (cuentaPadreExistente.TipoCuenta != "General") return new GeneralResponse(false, "La cuenta padre no es de tipo General");
        }
        
        return new GeneralResponse(true, "Validación exitosa");
    }
    
    //NOT IMPLEMENTED BECAUSE ID IS NOT int
    public Task<GeneralResponse> Delete(int id)
    {
        return Task.FromResult(NotFound());
    }
    public Task<PlanCuentaDto?> GetById(int id)
    {
        return Task.FromResult<PlanCuentaDto?>(null);
    }
}