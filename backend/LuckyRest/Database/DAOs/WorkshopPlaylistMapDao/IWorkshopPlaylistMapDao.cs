using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DAOs.WorkshopPlaylistMapDao;

public interface IWorkshopPlaylistMapDao
{
    public Task<WorkshopPlaylistMap?> GetWorkshopPlaylistMap(int mapId, Guid playlistId);

    public Task PostWorkshopPlaylistMap(WorkshopPlaylistMap playlistMap);
}