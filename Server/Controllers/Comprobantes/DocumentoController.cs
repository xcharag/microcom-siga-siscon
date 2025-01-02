using BaseLibrary.DTOs.Documentos;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts.Comprobantes;

namespace Server.Controllers.Comprobantes;

[Route("api/[controller]")]
[ApiController]
public class DocumentoController (IDocument documentInterface) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll() => Ok(await documentInterface.GetAllDocumentos());
    
    [HttpGet("dateRange/{start}/{end}")]
    public async Task<IActionResult> GetAllDateRange(string start, string end) => Ok(await documentInterface.GetAllDocumentosDateRange(start, end));
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(AsientoDto? item)
    {
        if (item is null) return BadRequest("Objeto no válido");
        return Ok(await documentInterface.CreateDocumento(item));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Update(AsientoDto? item)
    {
        if (item is null) return BadRequest("Objeto no válido");
        return Ok(await documentInterface.UpdateDocumento(item));
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string? id)
    {
        if (id is null) return BadRequest("Id no válido");
        return Ok(await documentInterface.DeleteDocumento(id));
    }
}