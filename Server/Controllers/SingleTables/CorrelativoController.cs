using BaseLibrary.DTOs.Correlativos;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts.SingleTables;

namespace Server.Controllers.SingleTables;

[Route("api/[controller]")]
[ApiController]
public class CorrelativoController(ICorrelativo correlativoInterface) : ControllerBase
{
    [HttpGet("next/{tipo}")]
    public async Task<IActionResult> NextCorrelativo(string tipo)
    {
        var result = await correlativoInterface.NextCorrelativo(tipo);
        return Ok(result);
    }
}