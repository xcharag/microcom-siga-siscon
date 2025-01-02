using BaseLibrary.DTOs.Documentos;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts.Comprobantes;

namespace ServerLibrary.Repositories.Implementations.Comprobantes;

public class DocumentRepository(AppDbContext appDbContext) : IDocument
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
        if (item == null) return new GeneralResponse(false, "El objeto AsientoDto no puede ser vacia");
        if (item.Cabecera == null)
            return new GeneralResponse(false, "La cabecera del asiento contable no puede ser vacia");
        if (string.IsNullOrEmpty(item.Cabecera.NroDoc))
            return new GeneralResponse(false, "El número de documento es requerido");

        if (item.Detalle == null || item.Detalle.Count == 0)
            return new GeneralResponse(false, "Debe agregar al menos un detalle válido");

        var documento = ParseDtoToEntity(item);
        if (documento == null) return new GeneralResponse(false, "Error al procesar el Documento");

        var checkProveedor = await appDbContext.Proveedores.FindAsync(item.Cabecera.ProveedorCodProveedor);
        if (checkProveedor == null) return new GeneralResponse(false, "No se encontró el proveedor");
        documento.Proveedor = checkProveedor;

        var checkBanco = await appDbContext.Bancos.FindAsync(item.Cabecera.BancoCodBanco);
        if (checkBanco == null) return new GeneralResponse(false, "No se encontró el banco");
        documento.Banco = checkBanco;

        appDbContext.Documentos.Add(documento);

        foreach (var detalle in item.Detalle)
        {
            if (detalle == null) continue;

            var detalleDocumento = ParseDetalleDtoToEntity(detalle);
            if (detalleDocumento == null)
                return new GeneralResponse(false,
                    "Error al procesar el Detalle del documento y asociarlo a la cabecera");

            var checkTcCosto = await appDbContext.TcCostos.FindAsync(detalle.TcCostoCodCc);
            if (checkTcCosto == null) return new GeneralResponse(false, "No se encontró el Centro de Costo");
            detalleDocumento.TcCosto = checkTcCosto;

            if (documento.DetalleDocumentos == null)
            {
                documento.DetalleDocumentos = new List<DetalleDocumento>();
            }

            documento.DetalleDocumentos.Add(detalleDocumento);
            appDbContext.DetalleDocumentos.Add(detalleDocumento);
        }

        await Commit();
        return new GeneralResponse(true, "Operación exitosa");
    }


    public async Task<GeneralResponse> UpdateDocumento(AsientoDto item)
    {
        if (item.Cabecera.NroDoc == null) return new GeneralResponse(false, "El número de documento es requerido");
        var documento = await appDbContext.Documentos.FindAsync(item.Cabecera.NroDoc);
        if (documento == null) return new GeneralResponse(false, "No se encontró el documento con ese Codigo");

        var newDocumento = ParseDtoToEntity(item);

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
        if (newDoc.Importe != oldDoc.Importe) oldDoc.Importe = newDoc.Importe;
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