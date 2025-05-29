using LuckyRest.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Database.DAOs;

public class WorkshopMapDao(LuckyDbContext dbContext)
{
    public async Task<WorkshopMap?> GetWorkshopMap(int workshopMapId)
    {
        var map = await dbContext.Maps.FindAsync(workshopMapId);
        return map;
    }

    public async Task PostWorkshopMap(WorkshopMap map)
    {
        dbContext.Maps.Add(map);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> PutWorkshopMap(int workshopMapId, WorkshopMap map)
    {
        if (workshopMapId != map.WorkshopMapId)
        {
            return false;
        }

        dbContext.Entry(map).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddPlaylistToMap(int workshopMapId, WorkshopPlaylist playlist)
    {
        var map = await dbContext.Maps.FindAsync(workshopMapId);
        if (map == null) return false;
        map.Playlists.Add(playlist);
        await PutWorkshopMap(workshopMapId, map);
        return true;
    }

    public bool MapExists(int workshopMapId)
    {
        return dbContext.Maps.Any(x => x.WorkshopMapId == workshopMapId);
    }
}