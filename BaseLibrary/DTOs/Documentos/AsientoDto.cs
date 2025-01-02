namespace BaseLibrary.DTOs.Documentos;

public class AsientoDto
{
    public required DocumentoDto Cabecera { get; set; }
    public List<DocumentoDetalleDto>? Detalle { get; set; }
}