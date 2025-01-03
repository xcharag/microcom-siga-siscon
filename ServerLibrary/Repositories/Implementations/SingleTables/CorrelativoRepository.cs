using BaseLibrary.DTOs.Correlativos;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts.SingleTables;

namespace ServerLibrary.Repositories.Implementations.SingleTables;

public class CorrelativoRepository(AppDbContext appDbContext) : ICorrelativo
{
    public async Task<Correlativo?> GetOneCorrelativo(string tipo)
    {
        return await appDbContext.Correlativos.FirstOrDefaultAsync(x => x.Tipo == tipo);
    }
    
    public async Task<CorrelativosDto> NextCorrelativo(string tipo)
    {
        var correlativo = await appDbContext.Correlativos.FirstOrDefaultAsync(x => x.Tipo == tipo);
        if (correlativo == null)
        {
            return new CorrelativosDto
            {
                Flag = false,
                Tipo = tipo,
                Detalle = "No se encontro correlativo",
                Valor = "0",
                Edition = false
            };
        }
        
        var valor = (int.Parse(correlativo.Valor!)+1).ToString();
        var largo = correlativo.Largo;
        var relleno = correlativo.Relleno;
        var gestion = correlativo.Gestion;
        var prefijo = correlativo.Prefijo;
        var sufijo = correlativo.Sufijo;
        
        if (valor!.Length < largo)
        {
            var dif = largo - valor.Length;
            for (var i = 0; i < dif; i++)
            {
                valor = relleno + valor;
            }
        }
        
        valor = prefijo + gestion + valor + sufijo;
        
        return new CorrelativosDto
        {
            Flag = true,
            Tipo = correlativo.Tipo,
            Detalle = correlativo.Detalle,
            Valor = valor,
            Edition = correlativo.Actualiza
        };
    }

    public async Task<GeneralResponse> UpdateOneDocumentoCorrelativo(CorrelativosDto correlativo)
    {
        var correlativoEntity = await appDbContext.Correlativos.FirstOrDefaultAsync(x => x.Tipo == correlativo.Tipo);
        if (correlativoEntity == null)
        {
            return new GeneralResponse(false, "No se encontro el correlativo");
        }
        if (correlativoEntity.Actualiza == false)
        {
            return new GeneralResponse(false, "El correlativo no se puede actualizar");
        }
        
        if (CorrelativoHelpers.VerifyCorrelativo(correlativo.Valor!, correlativoEntity) == "false")
        {
            return new GeneralResponse(false, "El numero de documento no cumple con los parametros");
        }
        
        var valorSinRelleno = CorrelativoHelpers.ExtractValor(correlativo.Valor!, correlativoEntity);

        Console.WriteLine($"Correlativo: {valorSinRelleno}");
        Console.WriteLine($"CorrelativoEntity: {correlativoEntity.Valor}");
        if (int.Parse(valorSinRelleno) == int.Parse(correlativoEntity.Valor!) + 1)
        {
            correlativoEntity.Valor = valorSinRelleno;
        }
        else
        {
            return new GeneralResponse(false, "El correlativo no es el siguiente");
        }
        
        appDbContext.Correlativos.Update(correlativoEntity);
        await appDbContext.SaveChangesAsync();
        return new GeneralResponse(true, "Correlativo actualizado");
    }

    public async Task<GeneralResponse> UpdateAllToGestion(GestionDto gestion)
    {
        var correlativos = await appDbContext.Correlativos.ToListAsync();
        foreach (var correlativo in correlativos)
        {
            if (correlativo.Actualiza == true)
            {
                correlativo.Gestion = gestion.Year + gestion.Month;
                var ultimoCorrelativo = await appDbContext.Documentos
                    .Where(x=>x.NroDoc!.Contains(CorrelativoHelpers.BuildCorrelativo(correlativo)))
                    .OrderByDescending(x=>x.NroDoc)
                    .FirstOrDefaultAsync();
                correlativo.Valor = ultimoCorrelativo != null ? CorrelativoHelpers.ExtractValor(ultimoCorrelativo.NroDoc!, correlativo) : "1";
            }
        }
        return new GeneralResponse(true, $"Correlativos actualizados a la gestion\n Año: {gestion.Year} Mes: {gestion.Month}");
    }
}