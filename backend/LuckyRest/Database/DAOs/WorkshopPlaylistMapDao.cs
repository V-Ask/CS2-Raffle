using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Database.DAOs;

public class WorkshopPlaylistMapDao(LuckyDbContext dbContext)
{
    public async Task<WorkshopPlaylistMap?> GetWorkshopPlaylistMap(int mapId, int playlistId)
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