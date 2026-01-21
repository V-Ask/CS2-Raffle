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
    
    public async Task<bool> PostWorkshopPlaylistMap(WorkshopPlaylistMap playlistMap)
    {
        try
        {
            dbContext.PlaylistMaps.Add(playlistMap);
            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteWorkshopPlaylistMap(string userId, long mapId, Guid playlistId)
    {
        var playlistMap = await dbContext.PlaylistMaps
            .Include(x => x.WorkshopPlaylist)
            .FirstOrDefaultAsync(playlistMap =>
                playlistMap.WorkshopMapId == mapId &&
                playlistMap.WorkshopPlaylistId == playlistId);

        if (playlistMap == null || playlistMap.WorkshopPlaylist.AuthorId != userId)
        {
            return false;
        }
        dbContext.PlaylistMaps.Remove(playlistMap);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Exists(string userId, Guid playlistId, long mapId)
    {
        return await dbContext.PlaylistMaps
            .AnyAsync(pm =>
                pm.WorkshopPlaylistId == playlistId &&
                pm.WorkshopMapId == mapId &&
                pm.WorkshopPlaylist.AuthorId == userId);
    }

    public async Task<int> IncreaseWeightsOfAllMaps(string userId, Guid playlistId, int increment, long[] exceptions)
    {
        return await dbContext.PlaylistMaps
            .Where(pm => 
                pm.WorkshopPlaylist.AuthorId == userId &&
                pm.WorkshopPlaylistId == playlistId &&
                !exceptions.Contains(pm.WorkshopMapId))
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(pm => pm.Weight, pm => pm.Weight + increment));
    }
}