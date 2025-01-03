namespace BaseLibrary.DTOs.Documentos;

public class AsientoDto
{
    public string? Tipo { get; set; }
    public required DocumentoDto Cabecera { get; set; }
    public List<DocumentoDetalleDto>? Detalle { get; set; }
}