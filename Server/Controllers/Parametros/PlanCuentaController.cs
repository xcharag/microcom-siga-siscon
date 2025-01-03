using BaseLibrary.DTOs.PlanCta;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Contracts.Parametros;

namespace Server.Controllers.Parametros;

[Route("api/[controller]")]
[ApiController]
public class PlanCuentaController (IPlanCuenta planCuentaInterface) : ControllerBase
{
    [HttpGet("generate/{cuentaPadre}")]
    public async Task<IActionResult> GenerateCodPlanCuenta(string cuentaPadre)
    {
        if (cuentaPadre == "") return BadRequest("Id no válido");
        return Ok(await planCuentaInterface.GenerateCodPlanCuenta(cuentaPadre));
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll() => Ok(await planCuentaInterface.GetAll());
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string? id)
    {
        if (id is null) return BadRequest("Id no válido");
        return Ok(await planCuentaInterface.Delete(id));
    }

    [HttpGet("single/{id}")]
    public async Task<IActionResult> GetById(string? id)
    {
        if (id is null) return BadRequest("Id no válido");
        return Ok(await planCuentaInterface.GetById(id));
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(PlanCuentaDto? item)
    {
        if (item is null) return BadRequest("Objeto no válido");
        return Ok(await planCuentaInterface.Create(item));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Update(PlanCuentaDto? item)
    {
        if (item is null) return BadRequest("Objeto no válido");
        return Ok(await planCuentaInterface.Update(item));
    }
}