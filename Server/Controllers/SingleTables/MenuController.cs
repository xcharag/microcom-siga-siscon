using BaseLibrary.DTOs.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Contracts.SingleTables;

namespace Server.Controllers.SingleTables;

[Route("api/[controller]")]
[ApiController]
public class MenuController(IMenu menuInterface) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> CreateMenu([FromBody] MenusCreation? menu)
    {
        if (menu is null) return BadRequest("El modelo esta vacio");
        
        var result = await menuInterface.CreateMenu(menu);
        return Ok(result);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateMenu([FromBody] MenusEdit? menu)
    {
        if (menu is null) return BadRequest("El modelo esta vacio");
        
        var result = await menuInterface.UpdateMenu(menu);
        return Ok(result);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteMenu(int id)
    {
        var result = await menuInterface.DeleteMenu(id);
        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpPost("create/menu-item")]
    public async Task<IActionResult> AddMenuItem([FromBody] MenusCreation? menu)
    {
        if (menu is null) return BadRequest("El modelo esta vacio");
        
        var result = await menuInterface.AddMenuItem(menu);
        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllMenus()
    {
        var result = await menuInterface.GetAllMenus();
        return Ok(result);
    }
}