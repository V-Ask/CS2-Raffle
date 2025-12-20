using GameHostConveyer.Models;
using LuckyRest.Database.DTOs.Actions;
using Microsoft.AspNetCore.Mvc;

namespace LuckyRest.Controllers.GameHostControllers;

[Route("[controller]")]
[ApiController]
public class DathostController : ControllerBase
{
    [HttpPut("start")]
    public async Task<IActionResult> StartDathostServer([FromBody] DatHostActionDto actionDto)
    {
        using var conveyer = new DatHostConveyer(actionDto.ServerId, actionDto.Username, actionDto.Password);
        var result = await conveyer.StartServer();
        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPut("stop")]
    public async Task<IActionResult> StopDathostServer([FromBody] DatHostActionDto actionDto)
    {
        using var conveyer = new DatHostConveyer(actionDto.ServerId, actionDto.Username, actionDto.Password);
        var result = await conveyer.StopServer();
        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPut("select")]
    public async Task<IActionResult> SelectMap([FromBody] DatHostSelectMapActionDto actionDto)
    {
        using var conveyer = new DatHostConveyer(actionDto.ServerId, actionDto.Username, actionDto.Password);
        var result = await conveyer.SelectWorkshopMap(actionDto.MapId);
        if (result)
        {
            return Ok();
        }

        return NotFound();
    }

    [HttpPost("test")]
    public async Task<IActionResult> TestConnection([FromBody] DatHostActionDto actionDto)
    {
        using var conveyer = new DatHostConveyer(actionDto.ServerId, actionDto.Username, actionDto.Password);
        var result = await conveyer.TestConnection();
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }
}