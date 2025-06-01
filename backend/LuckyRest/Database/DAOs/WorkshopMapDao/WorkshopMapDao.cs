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
    
    public bool MapExists(long workshopMapId)
    {
        return dbContext.Maps.Any(x => x.WorkshopMapId == workshopMapId);
    }
}