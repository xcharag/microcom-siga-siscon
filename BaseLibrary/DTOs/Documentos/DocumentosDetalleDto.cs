using BaseLibrary.Entities;

namespace BaseLibrary.DTOs.Documentos;

public class DocumentosDetalleDto
{
    public DetalleDocumento? Detalle { get; set; }
    public List<Anexos>? Anexos { get; set; }
}