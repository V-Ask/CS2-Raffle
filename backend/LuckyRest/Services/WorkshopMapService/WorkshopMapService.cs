using LuckyRest.Database.DAOs.WorkshopMapDao;
using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;
using WorkshopScraper.Interfaces.Scrapers;
using WorkshopScraper.Scraper;

namespace LuckyRest.Services.WorkshopMapService;

public class WorkshopMapService(IWorkshopMapDao workshopMapDao) : IWorkshopMapService
{
    private const string WorkshopUrlFormat = "https://steamcommunity.com/workshop/filedetails/?id={0}";

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
        
        var workshopUrl = string.Format(WorkshopUrlFormat, workshopMapId);
        var workshopExplorer = new SteamWorkshopExplorer(await _scraper.LoadAsync(workshopUrl));
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

    private static string TrimWithEllipsis(string input, int maxLength)
    {
        var stringLength = input.Length;
        if (stringLength < maxLength) return input;
        return input[..(maxLength - 3)] + "...";
    }
}