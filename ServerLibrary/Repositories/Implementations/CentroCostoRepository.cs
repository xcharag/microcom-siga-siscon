using BaseLibrary.DTOs.Parametros.CentroCosto;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class CentroCostoRepository (AppDbContext appDbContext) : IGenericRepositoryInterfaceString<CentroCostoDto>
{
    public async Task<List<CentroCostoDto>> GetAll()
    {
        var centroCostos = await appDbContext.TcCostos.ToListAsync();
        return centroCostos.Select(ConvertToDto).ToList();
    }

    public async Task<CentroCostoDto?> GetById(string id)
    {
        var centroCosto = await appDbContext.TcCostos.FindAsync(id);
        return centroCosto is null ? null : ConvertToDto(centroCosto);
    }

    public async Task<GeneralResponse> Create(CentroCostoDto item)
    {
        if(!await CheckId(item.CodCc!)) return new GeneralResponse(false, "El centro de costo ya existe");
        
        appDbContext.TcCostos.Add(ConvertToEntity(item));
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> Update(CentroCostoDto item)
    {
        var checkCentroCosto = await appDbContext.TcCostos.FindAsync(item.CodCc);
        if (checkCentroCosto is null) return new GeneralResponse(false, "No se encontró el centro de costo");
        checkCentroCosto.NomCc = item.NomCc ?? checkCentroCosto.NomCc;
        checkCentroCosto.NomResp = item.NomResp ?? checkCentroCosto.NomResp;
        
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> Delete(string id)
    {
        var checkCentroCosto = await appDbContext.TcCostos.FindAsync(id);
        if (checkCentroCosto is null) return new GeneralResponse(false, "No se encontró el centro de costo");
        
        var checkIsNotAssigned = await appDbContext.DetalleDocumentos.AnyAsync(x => x.TcCostoCodCc == id);
        if (checkIsNotAssigned) return new GeneralResponse(false, "El centro de costo está asignado a un detalle de documento");
        
        appDbContext.TcCostos.Remove(checkCentroCosto);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }
    
    private async Task<bool> CheckId(string id) => await appDbContext.TcCostos.FindAsync(id) is null;
    private async Task Commit() => await appDbContext.SaveChangesAsync();
    private CentroCostoDto ConvertToDto(TcCosto centroCosto) => new CentroCostoDto
    {
        CodCc = centroCosto.CodCc,
        NomCc = centroCosto.NomCc,
        NomResp = centroCosto.NomResp
    };
    private TcCosto ConvertToEntity(CentroCostoDto centroCosto) => new TcCosto
    {
        CodCc = centroCosto.CodCc,
        NomCc = centroCosto.NomCc,
        NomResp = centroCosto.NomResp
    };
}