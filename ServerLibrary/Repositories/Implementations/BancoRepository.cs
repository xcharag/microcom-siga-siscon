using BaseLibrary.DTOs.Parametros.Banco;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class BancoRepository(AppDbContext appDbContext) : IBanco
{
    public async Task<List<BancoDto>> GetAll()
    {
        var bancos = await appDbContext.Bancos.ToListAsync();
        var bancosDtos = bancos.Select(dto => new BancoDto()
        {
            CodBanco = dto.CodBanco,
            NomBanco = dto.NomBanco,
            Moneda = dto.Moneda,
            PlanCuenta = dto.PlanCuentaCodCuenta
        }).ToList();
        return bancosDtos;
    }

    public async Task<BancoDto?> GetById(int id)
    {
        var banco = await appDbContext.Bancos.FindAsync(id);
        if(banco is null) return null;
        return Conversor.ConvertToDto<Banco, BancoDto>(banco);
    }

    public async Task<GeneralResponse> Create(BancoDto item)
    {
        if(!await CheckId(item.CodBanco)) return new GeneralResponse(false, "El banco ya existe");
        var checkPlanCuenta = await appDbContext.PlanCuentas.FindAsync(item.PlanCuenta);
        if(checkPlanCuenta is null) return new GeneralResponse(false, "No se encontró el plan de cuenta");
        
        item.CodBanco = GenerateId(item.PlanCuenta!);
        var itemEntity = new Banco()
        {
            CodBanco = item.CodBanco,
            NomBanco = item.NomBanco,
            Moneda = item.Moneda,
            PlanCuentaCodCuenta = checkPlanCuenta.CodCuenta,
            PlanCuenta = checkPlanCuenta
        };
        
        appDbContext.Bancos.Add(itemEntity);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> Update(BancoDto item)
    {
        var checkPlanCuenta = await appDbContext.PlanCuentas.FindAsync(item.PlanCuenta);
        if(checkPlanCuenta is null) return new GeneralResponse(false, "No se encontró el plan de cuenta");
        
        var getBanco = await appDbContext.Bancos.FindAsync(item.CodBanco);
        if (getBanco is null) return new GeneralResponse(false, "No se encontró el banco");
        getBanco.NomBanco = item.NomBanco;
        getBanco.Moneda = item.Moneda;
        
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
    
    public async Task<GeneralResponse> GenerateCodBanco(string codPlanCuenta)
    {
        var existeCuenta = await appDbContext.PlanCuentas.FindAsync(codPlanCuenta);
        if(existeCuenta is null) return new GeneralResponse(false, "No se encontró el plan de cuenta");
        if(existeCuenta.TipoCuenta != "Detalle") return new GeneralResponse(false, "El plan de cuenta no es de tipo detalle");
        var codBancoGenerado = GenerateId(codPlanCuenta);
        return new GeneralResponse(true, codBancoGenerado.ToString());
    }
    
    private async Task<bool> CheckId(int id) => await appDbContext.Bancos.FindAsync(id) is null;
    private async Task Commit() => await appDbContext.SaveChangesAsync();
    
    private int GenerateId(string planCuentaCodCuenta)
    {
        //Get cuantos ultimo nivel
        var cuantosUltimoNivel = PlanCuentaHelpers.GetCuantosUltimoNivel(planCuentaCodCuenta);
        var digitosUltimoNivel = PlanCuentaHelpers.GetLastNivelDigits(planCuentaCodCuenta, cuantosUltimoNivel);
        
        //Obtener cuenta padre
        var cuentaPadre = PlanCuentaHelpers.GetCuentaPadre(planCuentaCodCuenta, cuantosUltimoNivel);
        var cuantosCuentaPadre = PlanCuentaHelpers.GetCuantosUltimoNivel(cuentaPadre);
        var digitosCuentaPadre = PlanCuentaHelpers.GetLastNivelDigits(cuentaPadre, cuantosCuentaPadre);
        
        return int.Parse(digitosCuentaPadre + digitosUltimoNivel);
    }
}