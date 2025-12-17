using GameHostConveyer.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuckyRest.Controllers.GameHostControllers;

[Route("[controller]")]
[ApiController]
public class DathostController : ControllerBase
{
    public async Task<IActionResult> StartDathostServer(
        [FromBody] string serverId,
        [FromBody] string username,
        [FromBody] string password)
    {
        using var conveyer = new DatHostConveyer(serverId, username, password);
        var result = await conveyer.StartServer();
        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }

    public async Task<IActionResult> StopDathostServer(
        [FromBody] string serverId,
        [FromBody] string username,
        [FromBody] string password)
    {
        using var conveyer = new DatHostConveyer(serverId, username, password);
        var result = await conveyer.StopServer();
        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }

    public async Task<IActionResult> SelectMap(
        [FromBody] string serverId,
        [FromBody] string username,
        [FromBody] string password,
        [FromBody] string mapId)
    {
        using var conveyer = new DatHostConveyer(serverId, username, password);
        var result = await conveyer.SelectWorkshopMap(mapId);
        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }
}