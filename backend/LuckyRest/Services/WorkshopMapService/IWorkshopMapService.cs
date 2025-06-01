using LuckyRest.Database.DTOs.Models;
using LuckyRest.Utils;

namespace LuckyRest.Services.WorkshopMapService;

public interface IWorkshopMapService
{
    public Task<ServiceResult<WorkshopMapDto>> GetWorkshopMap(long workshopMapId);
    public Task<ServiceResult<WorkshopMapDto>> AddWorkshopMap(long workshopMapId);
}