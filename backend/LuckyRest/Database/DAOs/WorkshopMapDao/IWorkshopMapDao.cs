using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DAOs.WorkshopMapDao;

public interface IWorkshopMapDao
{
    public Task<WorkshopMap?> GetWorkshopMap(long workshopMapId);
    public Task PostWorkshopMap(WorkshopMap map);
    public Task<bool> PutWorkshopMap(long workshopMapId, WorkshopMap map);
    public bool MapExists(long workshopMapId);
}