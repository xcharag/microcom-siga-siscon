using BaseLibrary.Entities;

namespace BaseLibrary.DTOs.Documentos;

public class DocumentosCompletosDto
{
    public Documento? Cabecera { get; set; }
    public List<DetalleDocumento>? Detalles { get; set; }
}