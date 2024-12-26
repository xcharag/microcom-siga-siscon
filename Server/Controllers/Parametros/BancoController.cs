using BaseLibrary.DTOs.Parametros.Banco;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Contracts.Parametros;

namespace Server.Controllers.Parametros;

[Route("api/[controller]")]
[ApiController]
public class BancoController(IBanco bancoInterface) : ControllerBase
{
    [HttpGet("generate/{codPlanCuenta}")]
    public async Task<IActionResult> GenerateCodBanco(string codPlanCuenta)
    {
        if (codPlanCuenta == "") return BadRequest("Id no válido");
        return Ok(await bancoInterface.GenerateCodBanco(codPlanCuenta));
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll() => Ok(await bancoInterface.GetAll());
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return BadRequest("Id no válido");
        return Ok(await bancoInterface.Delete(id));
    }

    [HttpGet("single/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id == 0) return BadRequest("Id no válido");
        return Ok(await bancoInterface.GetById(id));
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(BancoDto? item)
    {
        if (item is null) return BadRequest("Objeto no válido");
        return Ok(await bancoInterface.Create(item));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Update(BancoDto? item)
    {
        if (item is null) return BadRequest("Objeto no válido");
        return Ok(await bancoInterface.Update(item));
    }
}