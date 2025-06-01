using LuckyRest.Database.DAOs.WorkshopMapDao;
using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;
using WorkshopScraper.Scraper;

namespace LuckyRest.Services.WorkshopMapService;

public class WorkshopMapService(IWorkshopMapDao workshopMapDao) : IWorkshopMapService
{
    private readonly string _workshopRoute = "https://steamcommunity.com/sharedfiles/filedetails/?id=";

    public async Task<ServiceResult<WorkshopMapDto>> GetWorkshopMap(long workshopMapId)
    {
        var map = await workshopMapDao.GetWorkshopMap(workshopMapId);
        return map == null
            ? ServiceResult.NotFound.WithData<WorkshopMapDto>(null)
            : ServiceResult.Success.WithData(WorkshopMapDto.FromEntity(map));
    }

    public async Task<ServiceResult<WorkshopMapDto>> AddWorkshopMap(long workshopMapId)
    {
        if (workshopMapDao.MapExists(workshopMapId))
        {
            return await GetWorkshopMap(workshopMapId);
        }

        var scraper = new SteamWorkshopScraper(GenericScraper.Load(_workshopRoute + workshopMapId));
        var name = scraper.GetTitle();
        var description = scraper.GetDescription();
        var imageUrl = scraper.GetImageUrl();
        var map = new WorkshopMap
        {
            WorkshopMapId = workshopMapId,
            Description = description == null ? "" : TrimWithEllipsis(description, 600),
            Name = name == null ? "" : TrimWithEllipsis(name, 255),
            ImageUrl = imageUrl == null ? "" : TrimWithEllipsis(imageUrl, 600)
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