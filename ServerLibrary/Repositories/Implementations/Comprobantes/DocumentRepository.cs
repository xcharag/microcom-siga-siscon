using BaseLibrary.DTOs.Correlativos;
using BaseLibrary.DTOs.Documentos;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts.Comprobantes;
using ServerLibrary.Repositories.Contracts.SingleTables;

namespace ServerLibrary.Repositories.Implementations.Comprobantes;

public class DocumentRepository(AppDbContext appDbContext,ICorrelativo correlativoRepository) : IDocument
{
    public async Task<List<DocumentoDto>> GetAllDocumentos()
    {
        var documentosEntitie = await appDbContext.Documentos.ToListAsync();
       return documentosEntitie.Select(ParseEntityToDto).ToList();
    }

    public async Task<List<DocumentoDto>> GetAllDocumentosDateRange(string start, string end)
    {
        var documentosEntitie = await appDbContext.Documentos.ToListAsync();
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        
        var documentosDto = documentosEntitie.Select(ParseEntityToDto).ToList();
        return documentosDto.Where(doc =>
        {
            if (doc.FechaRegistro != null)
            {
                var docDate = DateTime.Parse(doc.FechaRegistro);
                return docDate >= startDate && docDate <= endDate;
            }

            return false;
        }).ToList();
    }

    public Task<List<DocumentoDto>> GetAllIngresos()
    {
        throw new NotImplementedException();
    }

    public Task<List<DocumentoDto>> GetAllEgresos()
    {
        throw new NotImplementedException();
    }

    public Task<List<DocumentoDto>> GetAllFondosFijos()
    {
        throw new NotImplementedException();
    }

public async Task<GeneralResponse> CreateDocumento(AsientoDto item)
{
    if (string.IsNullOrEmpty(item.Cabecera.NroDoc))
        return new GeneralResponse(false, "El número de documento es requerido");

    if (item.Detalle == null || item.Detalle.Count == 0)
        return new GeneralResponse(false, "Debe agregar al menos un detalle válido");

    var correlativo = await correlativoRepository.GetOneCorrelativo(item.Tipo!);
    if (correlativo is null)
        return new GeneralResponse(false, "No se encontró el correlativo correspondiente");

    var documento = ParseDtoToEntity(item);

    if (CorrelativoHelpers.VerifyCorrelativo(documento.NroDoc!, correlativo) == "Error")
        return new GeneralResponse(false, "El número de documento no cumple con el formato");

    var dtoUpdateCorrelativo = new CorrelativosDto
    {
        Tipo = item.Tipo,
        Detalle = correlativo.Detalle,
        Edition = true,
        Flag = true,
        Valor = documento.NroDoc
    };

    var checkProveedor = await appDbContext.Proveedores.FindAsync(item.Cabecera.ProveedorCodProveedor);
    if (checkProveedor == null)
        return new GeneralResponse(false, "No se encontró el proveedor");

    documento.Proveedor = checkProveedor;

    var checkBanco = await appDbContext.Bancos.FindAsync(item.Cabecera.BancoCodBanco);
    if (checkBanco == null)
        return new GeneralResponse(false, "No se encontró el banco");

    documento.Banco = checkBanco;

    // Initialize DetalleDocumentos if null
    documento.DetalleDocumentos ??= new List<DetalleDocumento>();

    foreach (var detalle in item.Detalle)
    {
        var detalleDocumento = ParseDetalleDtoToEntity(detalle);

        var checkDetalle = await appDbContext.DetalleDocumentos
            .FindAsync(detalleDocumento.NroDetalleDoc);

        if (checkDetalle != null)
            return new GeneralResponse(false, $"El detalle {detalleDocumento.NroDetalleDoc} ya existe");

        var checkTcCosto = await appDbContext.TcCostos.FindAsync(detalle.TcCostoCodCc);
        if (checkTcCosto == null)
            return new GeneralResponse(false, $"No se encontró el Centro de Costo en el detalle: {detalle.NroDetalleDoc}");

        detalleDocumento.TcCosto = checkTcCosto;

        var checkPlanCuenta = await appDbContext.PlanCuentas.FindAsync(detalle.PlanCuentaCodCuenta);
        if (checkPlanCuenta == null || checkPlanCuenta.TipoCuenta != "Detalle")
            return new GeneralResponse(false, $"Cuenta contable inválida en el detalle: {detalle.NroDetalleDoc}");

        detalleDocumento.PlanCuenta = checkPlanCuenta;
        detalleDocumento.Documento = documento;

        // Add to Documento entity list
        documento.DetalleDocumentos.Add(detalleDocumento);

        // Add to DbContext
        appDbContext.DetalleDocumentos.Add(detalleDocumento);
    }

    appDbContext.Documentos.Add(documento);

    var actualCorrelativo = await correlativoRepository.UpdateOneDocumentoCorrelativo(dtoUpdateCorrelativo);
    if (!actualCorrelativo.Flag)
        return actualCorrelativo with { Flag = false };

    await Commit();
    return new GeneralResponse(true, "Operación exitosa");
}

    
    public async Task<GeneralResponse> UpdateDocumento(AsientoDto item)
    {
        if (item.Cabecera.NroDoc == null) return new GeneralResponse(false, "El número de documento es requerido");
        
        var documento = await appDbContext.Documentos.FindAsync(item.Cabecera.NroDoc);
        if (documento == null) return new GeneralResponse(false, "No se encontró el documento con ese Codigo");

        var newDocumento = ParseDtoToEntity(item);
        
        var correlativo = await correlativoRepository.GetOneCorrelativo(item.Tipo!);
        if (correlativo == null) return new GeneralResponse(false, "No se encontró el correlativo correspondiente");
        if (CorrelativoHelpers.VerifyCorrelativo(newDocumento.NroDoc!, correlativo) == "false")
        {
            return new GeneralResponse(false, "El número de documento no cumple con los parametros");
        }
        
        //Check Proveedor and Banco then Add it to the Documento
        var checkProveedor = await appDbContext.Proveedores.FindAsync(item.Cabecera.ProveedorCodProveedor);
        if (checkProveedor == null) return new GeneralResponse(false, "No se encontró el proveedor");
        newDocumento.Proveedor = checkProveedor;

        var checkBanco = await appDbContext.Bancos.FindAsync(item.Cabecera.BancoCodBanco);
        if (checkBanco == null) return new GeneralResponse(false, "No se encontró el banco");
        newDocumento.Banco = checkBanco;

        //Now compare the new Documento with the previus one and update it
        var documentoCambiado = CheckChanges(newDocumento, documento);

        appDbContext.Documentos.Update(newDocumento);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    public async Task<GeneralResponse> DeleteDocumento(string id)
    {
        var checkDocumento = await appDbContext.Documentos.FindAsync(id);
        if (checkDocumento == null) return new GeneralResponse(false, "No se encontró el documento con ese Codigo");

        checkDocumento.DetalleDocumentos!.Clear();
        appDbContext.Documentos.Remove(checkDocumento);
        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }

    private async Task Commit() => await appDbContext.SaveChangesAsync();

    private Documento ParseDtoToEntity(AsientoDto item)
    {
        var documento = new Documento()
        {
            NroDoc = item.Cabecera.NroDoc,
            FechaDoc = item.Cabecera.FechaDoc,
            Nombre = item.Cabecera.Nombre,
            NroCheque = item.Cabecera.NroCheque,
            Glosa1 = item.Cabecera.Glosa1,
            Glosa2 = item.Cabecera.Glosa2,
            Moneda = item.Cabecera.Moneda,
            Importe = item.Cabecera.Importe,
            CreatedBy = item.Cabecera.CreatedBy,
            Origen = item.Cabecera.Origen,
            FechaRegistro = item.Cabecera.FechaRegistro,
            HoraRegistro = item.Cabecera.HoraRegistro,
            ProveedorCodProveedor = item.Cabecera.ProveedorCodProveedor,
            BancoCodBanco = item.Cabecera.BancoCodBanco
        };
        return documento;
    }
    
    private DocumentoDto ParseEntityToDto(Documento item)
    {
        var documento = new DocumentoDto()
        {
            NroDoc = item.NroDoc,
            FechaDoc = item.FechaDoc,
            Nombre = item.Nombre,
            NroCheque = item.NroCheque,
            Glosa1 = item.Glosa1,
            Glosa2 = item.Glosa2,
            Moneda = item.Moneda,
            Importe = item.Importe,
            CreatedBy = item.CreatedBy,
            Origen = item.Origen,
            FechaRegistro = item.FechaRegistro,
            HoraRegistro = item.HoraRegistro,
            ProveedorCodProveedor = item.ProveedorCodProveedor,
            BancoCodBanco = item.BancoCodBanco
        };
        return documento;
    }

    private DetalleDocumento ParseDetalleDtoToEntity(DocumentoDetalleDto item)
    {
        var detalle = new DetalleDocumento()
        {
            NroDetalleDoc = item.NroDetalleDoc,
            Correl = item.Correl,
            Tipo = item.Tipo,
            Mtobd = item.Mtobd,
            Mtodd = item.Mtodd,
            Mtobh = item.Mtobh,
            Mtodh = item.Mtodh,
            Glosa = item.Glosa,
            TcCostoCodCc = item.TcCostoCodCc,
            DocumentoNroDoc = item.DocumentoNroDoc,
            PlanCuentaCodCuenta = item.PlanCuentaCodCuenta
        };
        return detalle;
    }

    private Documento CheckChanges(Documento newDoc, Documento oldDoc)
    {
        if (newDoc.FechaDoc != oldDoc.FechaDoc) oldDoc.FechaDoc = newDoc.FechaDoc;
        if (newDoc.Nombre != oldDoc.Nombre) oldDoc.Nombre = newDoc.Nombre;
        if (newDoc.NroCheque != oldDoc.NroCheque) oldDoc.NroCheque = newDoc.NroCheque;
        if (newDoc.Glosa1 != oldDoc.Glosa1) oldDoc.Glosa1 = newDoc.Glosa1;
        if (newDoc.Glosa2 != oldDoc.Glosa2) oldDoc.Glosa2 = newDoc.Glosa2;
        if (newDoc.Moneda != oldDoc.Moneda) oldDoc.Moneda = newDoc.Moneda;
        if (newDoc.CreatedBy != oldDoc.CreatedBy) oldDoc.CreatedBy = newDoc.CreatedBy;
        if (newDoc.Origen != oldDoc.Origen) oldDoc.Origen = newDoc.Origen;
        if (newDoc.FechaRegistro != oldDoc.FechaRegistro) oldDoc.FechaRegistro = newDoc.FechaRegistro;
        if (newDoc.HoraRegistro != oldDoc.HoraRegistro) oldDoc.HoraRegistro = newDoc.HoraRegistro;
        if (newDoc.ProveedorCodProveedor != oldDoc.ProveedorCodProveedor)
            oldDoc.ProveedorCodProveedor = newDoc.ProveedorCodProveedor;
        if (newDoc.BancoCodBanco != oldDoc.BancoCodBanco) oldDoc.BancoCodBanco = newDoc.BancoCodBanco;
        return oldDoc;
    }
}