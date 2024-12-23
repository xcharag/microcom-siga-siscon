using BaseLibrary.DTOs.Parametros.Proveedor;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class ProveedorRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<ProveedorDto>
{
    public async Task<List<ProveedorDto>> GetAll()
    {
        var proveedores = await appDbContext.Proveedores.ToListAsync();
        return proveedores.Select(EntitieToDto).ToList();
    }

    public async Task<ProveedorDto?> GetById(int id)
    {
        var proveedor = await appDbContext.Proveedores.FindAsync(id);
        return proveedor is null ? null : EntitieToDto(proveedor);
    }

    public async Task<GeneralResponse> Create(ProveedorDto item)
    {
        var checkPlanCuenta = await appDbContext.PlanCuentas.FindAsync(item.PlanCuentaCodCuenta);
        if (checkPlanCuenta is null) return new GeneralResponse(false, "No se encontró el plan de cuenta");
        if (checkPlanCuenta.TipoCuenta != "Detalle") return new GeneralResponse(false, "El plan de cuenta no es de tipo detalle");

        var entitie = DtoToEntitie(item);
        checkPlanCuenta.Proveedores!.Add(entitie);
        appDbContext.Proveedores.Add(entitie);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> Update(ProveedorDto item)
    {
        var proveedor = await appDbContext.Proveedores.FindAsync(item.CodProveedor);
        if (proveedor is null) return new GeneralResponse(false, "El proveedor no existe");
        
        proveedor.NomProv = item.NomProv ?? proveedor.NomProv;
        proveedor.DirProv = item.DirProv ?? proveedor.DirProv;
        proveedor.TelProv = item.TelProv ?? proveedor.TelProv;
        proveedor.EmailProv = item.EmailProv ?? proveedor.EmailProv;
        proveedor.Contacto = item.Contacto ?? proveedor.Contacto;
        proveedor.TipoDoc = item.TipoDoc ?? proveedor.TipoDoc;
        proveedor.NroDoc = item.NroDoc ?? proveedor.NroDoc;
        proveedor.CodigoEx = item.CodigoEx ?? proveedor.CodigoEx;
        proveedor.Complemento = item.Complemento ?? proveedor.Complemento;
        proveedor.LimiteCre = item.LimiteCre is not 0 ? item.LimiteCre : proveedor.LimiteCre;
        
        if (proveedor.PlanCuentaCodCuenta != item.PlanCuentaCodCuenta)
        {
            var checkPlanCuenta = await appDbContext.PlanCuentas.FindAsync(item.PlanCuentaCodCuenta);
            if (checkPlanCuenta is null) return new GeneralResponse(false, "No se encontró el plan de cuenta");
            if (checkPlanCuenta.TipoCuenta != "Detalle") return new GeneralResponse(false, "El plan de cuenta no es de tipo detalle");
            
            proveedor.PlanCuentaCodCuenta = item.PlanCuentaCodCuenta;
            proveedor.PlanCuenta = checkPlanCuenta;
        }
        
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> Delete(int id)
    {
        if (await CheckId(id)) return new GeneralResponse(false, "El proveedor no existe");
        
        var proveedor = await appDbContext.Proveedores.FindAsync(id);
        var planCuenta = await appDbContext.PlanCuentas.FindAsync(proveedor!.PlanCuentaCodCuenta);
        
        planCuenta!.Proveedores!.Remove(proveedor);
        appDbContext.Proveedores.Remove(proveedor);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }
    
    private async Task Commit() => await appDbContext.SaveChangesAsync();
    private async Task<bool> CheckId(int id) => await appDbContext.Proveedores.FindAsync(id) is null;
    private ProveedorDto EntitieToDto(Proveedor item)
    {
        return new ProveedorDto
        {
            CodProveedor = item.CodProveedor,
            NomProv = item.NomProv,
            DirProv = item.DirProv,
            TelProv = item.TelProv,
            EmailProv = item.EmailProv,
            Contacto = item.Contacto,
            TipoDoc = item.TipoDoc,
            NroDoc = item.NroDoc,
            CodigoEx = item.CodigoEx,
            Complemento = item.Complemento,
            LimiteCre = item.LimiteCre,
            PlanCuentaCodCuenta = item.PlanCuentaCodCuenta
        };
    }

    private Proveedor DtoToEntitie(ProveedorDto item)
    {
        return new Proveedor
        {
            CodProveedor = item.CodProveedor,
            NomProv = item.NomProv,
            DirProv = item.DirProv,
            TelProv = item.TelProv,
            EmailProv = item.EmailProv,
            Contacto = item.Contacto,
            TipoDoc = item.TipoDoc,
            NroDoc = item.NroDoc,
            CodigoEx = item.CodigoEx,
            Complemento = item.Complemento,
            LimiteCre = item.LimiteCre,
            PlanCuentaCodCuenta = item.PlanCuentaCodCuenta
        };
    }
}