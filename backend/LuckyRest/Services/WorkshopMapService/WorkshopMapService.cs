using LuckyRest.Database.DAOs.WorkshopMapDao;
using LuckyRest.Database.DTOs.Models;
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

        // Starts the spawn scan now so the count is ready when the map gets rolled
        _ = httpClientFactory.CreateClient("SpawnApi").GetAsync(workshopMapId.ToString());
        
        var workshopExplorer = new SteamWorkshopExplorer(await _scraper.LoadAsync(workshopMapId.ToString()));
        var name = workshopExplorer.GetTitle();
        var description = workshopExplorer.GetDescription();
        var imageUrl = workshopExplorer.GetImageUrl();
        var map = new WorkshopMap
        {
            WorkshopMapId = workshopMapId,
            Description = TrimWithEllipsis(description, 600),
            Name = TrimWithEllipsis(name, 255),
            ImageUrl = TrimWithEllipsis(imageUrl, 600)
        };
        await workshopMapDao.PostWorkshopMap(map);
        return ServiceResult.Success.WithData(WorkshopMapDto.FromEntity(map));
    }

    public async Task BackfillSpawnCounts()
    {
        var ids = await workshopMapDao.GetAllMapIds();
        var client = httpClientFactory.CreateClient("SpawnApi");
        // The spawn api takes at most 200 maps per batch
        foreach (var chunk in ids.Chunk(200))
        {
            using var response = await client.PostAsJsonAsync("v1/maps/batch",
                new { ids = chunk.Select(x => x.ToString()) });
            response.EnsureSuccessStatusCode();
        }
    }

    private static string TrimWithEllipsis(string input, int maxLength)
    {
        var stringLength = input.Length;
        if (stringLength < maxLength) return input;
        return input[..(maxLength - 3)] + "...";
    }
}