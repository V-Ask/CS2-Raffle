using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DAOs.WorkshopPlaylistMapDao;

public interface IWorkshopPlaylistMapDao
{
    public Task<WorkshopPlaylistMap?> GetWorkshopPlaylistMap(int mapId, Guid playlistId);

    public Task<bool> PostWorkshopPlaylistMap(WorkshopPlaylistMap playlistMap);

    public Task<bool> DeleteWorkshopPlaylistMap(string userId, long mapId, Guid playlistId);

    public Task<bool> Exists(string userId, Guid playlistId, long mapId);

    public Task<int> IncreaseWeightsOfAllMaps(string userId, Guid playlistId, int increment, long[] exceptions);
}