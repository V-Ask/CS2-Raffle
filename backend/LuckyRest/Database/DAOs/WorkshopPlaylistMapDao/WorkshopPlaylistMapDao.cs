using LuckyRest.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Database.DAOs.WorkshopPlaylistMapDao;

public class WorkshopPlaylistMapDao(LuckyDbContext dbContext) : IWorkshopPlaylistMapDao
{
    public async Task<WorkshopPlaylistMap?> GetWorkshopPlaylistMap(int mapId, Guid playlistId)
    {
        var playlistMap =
            await dbContext.PlaylistMaps.FirstOrDefaultAsync(x =>
                x.WorkshopMap.WorkshopMapId == mapId && x.WorkshopPlaylist.WorkshopPlaylistId == playlistId);
        return playlistMap;
    }
    
    public async Task PostWorkshopPlaylistMap(WorkshopPlaylistMap playlistMap)
    {
        dbContext.PlaylistMaps.Add(playlistMap);
        await dbContext.SaveChangesAsync();
    }
}