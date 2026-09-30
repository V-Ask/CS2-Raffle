using System.Net;
using LuckyRest.Database.DTOs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuckyRest.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class SpawnController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<SpawnCountDto>> GetSpawnCount(long mapId)
    {
        var client = httpClientFactory.CreateClient("SpawnApi");
        using var response = await client.GetAsync(mapId.ToString());
        if (response.StatusCode == HttpStatusCode.Accepted) return Accepted();
        if (!response.IsSuccessStatusCode) return NotFound();

        var spawnCount = await response.Content.ReadFromJsonAsync<SpawnCountDto>();
        if (spawnCount == null) return NotFound();
        return spawnCount;
    }
}
