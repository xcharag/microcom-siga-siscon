using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenericControllerString<T>(IGenericRepositoryInterfaceString<T> genericRepositoryInterface) : 
    ControllerBase where T : class
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll() => Ok(await genericRepositoryInterface.GetAll());

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string? id)
    {
        if (id is null) return BadRequest("Id no válido");
        return Ok(await genericRepositoryInterface.Delete(id!));
    }

    [HttpGet("single/{id}")]
    public async Task<IActionResult> GetById(string? id)
    {
        if (id is null) return BadRequest("Id no válido");
        return Ok(await genericRepositoryInterface.GetById(id!));
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(T? item)
    {
        if (item is null) return BadRequest("Objeto no válido");
        return Ok(await genericRepositoryInterface.Create(item));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Update(T? item)
    {
        if (item is null) return BadRequest("Objeto no válido");
        return Ok(await genericRepositoryInterface.Update(item));
    }
}