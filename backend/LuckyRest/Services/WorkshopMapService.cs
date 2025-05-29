using LuckyRest.Database;
using LuckyRest.Database.DAOs;
using LuckyRest.Database.DTOs;
using LuckyRest.Database.DTOs.Actions;
using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.DTOs.Results;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;
using WorkshopScraper.Scraper;

namespace LuckyRest.Services;

public class WorkshopMapService(WorkshopMapDao workshopMapDao, WorkshopPlaylistDao workshopPlaylistDao)
{
    private string WORKSHOP_ROUTE = "https://steamcommunity.com/sharedfiles/filedetails/?id=";

    public async Task<ServiceResult<WorkshopMapDto>> GetWorkshopMap(int workshopMapId)
    {
        var map = await workshopMapDao.GetWorkshopMap(workshopMapId);
        return map == null
            ? ServiceResult.NotFound.WithData<WorkshopMapDto>(null)
            : ServiceResult.Success.WithData(WorkshopMapDto.FromEntity(map));
    }

    public async Task<ServiceResult<WorkshopMapDto>> AddWorkshopMap(int workshopMapId)
    {
        if (workshopMapDao.MapExists(workshopMapId))
        {
            return await GetWorkshopMap(workshopMapId);
        }

        var scraper = new SteamWorkshopScraper(GenericScraper.Load(WORKSHOP_ROUTE + workshopMapId));
        var map = new WorkshopMap
        {
            WorkshopMapId = workshopMapId,
            Description = scraper.GetDescription() ?? "",
            Name = scraper.GetTitle() ?? "",
            ImageUrl = scraper.GetImageUrl() ?? ""
        };
        await workshopMapDao.PostWorkshopMap(map);
        return ServiceResult.Success.WithData(WorkshopMapDto.FromEntity(map));
    }
}