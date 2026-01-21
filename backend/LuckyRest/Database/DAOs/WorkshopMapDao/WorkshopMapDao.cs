using LuckyRest.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Database.DAOs.WorkshopMapDao;

public class WorkshopMapDao(LuckyDbContext dbContext) : IWorkshopMapDao
{
    public async Task<WorkshopMap?> GetWorkshopMap(long workshopMapId)
    {
        var map = await dbContext.Maps.FindAsync(workshopMapId);
        return map;
    }

    public async Task PostWorkshopMap(WorkshopMap map)
    {
        dbContext.Maps.Add(map);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> PutWorkshopMap(long workshopMapId, WorkshopMap map)
    {
        if (workshopMapId != map.WorkshopMapId)
        {
            return false;
        }

        dbContext.Entry(map).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<WorkshopMap> DeleteWorkshopMap(WorkshopMap map)
    {
        var deletedMap= dbContext.Maps.Remove(map);
        await dbContext.SaveChangesAsync();
        return deletedMap.Entity;
    }

    public async Task<bool> DeleteIfOrphaned(long workshopMapId)
    {
        var deletedRows = await dbContext.Maps
            .Where(m =>
                m.WorkshopMapId == workshopMapId &&
                m.Playlists.Count == 0)
            .ExecuteDeleteAsync();
        return deletedRows > 0;
    }

    public bool MapExists(long workshopMapId)
    {
        return dbContext.Maps.Any(x => x.WorkshopMapId == workshopMapId);
    }
}