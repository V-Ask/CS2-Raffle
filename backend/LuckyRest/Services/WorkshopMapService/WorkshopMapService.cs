using System.Net;
using LuckyRest.Database.DAOs.WorkshopMapDao;
using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.DTOs.Results;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;
using WorkshopScraper.Interfaces.Scrapers;
using WorkshopScraper.Scraper;

namespace LuckyRest.Services.WorkshopMapService;

public class WorkshopMapService(IWorkshopMapDao workshopMapDao, IHttpClientFactory httpClientFactory) : IWorkshopMapService
{
    private readonly LimitedScraper<string> _scraper = new(new WebScraper());

    public async Task<ServiceResult<WorkshopMapDto>> GetWorkshopMap(long workshopMapId)
    {
        var map = await workshopMapDao.GetWorkshopMap(workshopMapId);
        return map == null
            ? ServiceResult.NotFound.WithData<WorkshopMapDto>(null)
            : ServiceResult.Success.WithData(WorkshopMapDto.FromEntity(map));
    }

    public async Task<ServiceResult<WorkshopMapDto>> EnsureWorkshopMap(long workshopMapId)
    {
        if (workshopMapDao.MapExists(workshopMapId))
        {
            return await GetWorkshopMap(workshopMapId);
        }
        
        var workshopExplorer = new SteamWorkshopExplorer(await _scraper.LoadAsync(workshopMapId.ToString()));
        var name = workshopExplorer.GetTitle();
        var description = workshopExplorer.GetDescription();
        var imageUrl = workshopExplorer.GetImageUrl();
        var spawnCount = await GetSpawnCount(workshopMapId);
        var map = new WorkshopMap
        {
            WorkshopMapId = workshopMapId,
            Description = TrimWithEllipsis(description, 600),
            Name = TrimWithEllipsis(name, 255),
            ImageUrl = TrimWithEllipsis(imageUrl, 600),
            TerroristSpawns = spawnCount?.TerroristSpawns,
            CounterTerroristSpawns = spawnCount?.CounterTerroristSpawns
        };
        await workshopMapDao.PostWorkshopMap(map);
        return ServiceResult.Success.WithData(WorkshopMapDto.FromEntity(map));
    }

    public async Task UpdateMissingSpawnCounts()
    {
        var maps = await workshopMapDao.GetMapsWithoutSpawnCount();
        var client = httpClientFactory.CreateClient("SpawnApi");
        // The spawn api takes at most 200 maps per batch
        foreach (var chunk in maps.Chunk(200))
        {
            using var response = await client.PostAsJsonAsync("v1/maps/batch",
                new { ids = chunk.Select(x => x.WorkshopMapId.ToString()) });
            response.EnsureSuccessStatusCode();
            var batch = await response.Content.ReadFromJsonAsync<SpawnBatchResultDto>();

            foreach (var result in batch?.Results ?? [])
            {
                var map = chunk.FirstOrDefault(x => x.WorkshopMapId == result.WorkshopId);
                if (map == null || result.TerroristSpawns == null) continue;
                map.TerroristSpawns = result.TerroristSpawns;
                map.CounterTerroristSpawns = result.CounterTerroristSpawns;
                await workshopMapDao.PutWorkshopMap(map.WorkshopMapId, map);
            }
        }
    }

    private async Task<SpawnCountDto?> GetSpawnCount(long workshopMapId)
    {
        try
        {
            var client = httpClientFactory.CreateClient("SpawnApi");
            using var response = await client.GetAsync(workshopMapId.ToString());
            if (response.StatusCode != HttpStatusCode.OK) return null;
            return await response.Content.ReadFromJsonAsync<SpawnCountDto>();
        }
        catch (HttpRequestException)
        {
            // Adding a map should still work if the spawn api is down
            return null;
        }
    }

    private static string TrimWithEllipsis(string input, int maxLength)
    {
        var stringLength = input.Length;
        if (stringLength < maxLength) return input;
        return input[..(maxLength - 3)] + "...";
    }
}