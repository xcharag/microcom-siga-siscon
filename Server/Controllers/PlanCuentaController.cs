using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlanCuentaController (IPlanCuenta planCuentaInterface) : ControllerBase
{
    [HttpGet("generate/{cuentaPadre}")]
    public async Task<IActionResult> GenerateCodPlanCuenta(int cuentaPadre)
    {
        if (cuentaPadre <= 0) return BadRequest("Id no válido");
        return Ok(await planCuentaInterface.GenerateCodPlanCuenta(cuentaPadre));
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll() => Ok(await planCuentaInterface.GetAll());
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Id no válido");
        return Ok(await planCuentaInterface.Delete(id));
    }

    [HttpGet("single/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0) return BadRequest("Id no válido");
        return Ok(await planCuentaInterface.GetById(id));
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(PlanCuenta? item)
    {
        if (item is null) return BadRequest("Objeto no válido");
        return Ok(await planCuentaInterface.Create(item));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Update(PlanCuenta? item)
    {
        if (item is null) return BadRequest("Objeto no válido");
        return Ok(await planCuentaInterface.Update(item));
    }
}