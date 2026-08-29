using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Database.DAOs.WorkshopPlaylistDao;

public class WorkshopPlaylistDao(LuckyDbContext dbContext) : IWorkshopPlaylistDao
{
    public async Task<WorkshopPlaylist?> GetWorkshopPlaylist(string userId, Guid workshopPlaylistId)
    {
        return await dbContext.Playlists
            .Include(p => p.PlaylistMaps)
            .ThenInclude(pm => pm.WorkshopMap)
            .FirstOrDefaultAsync(x =>
                x.AuthorId == userId && x.WorkshopPlaylistId == workshopPlaylistId);
    }

    public async Task<List<WorkshopPlaylistIndexDto>> GetWorkshopPlaylistIndices(string userId)
    {
        var playlists = await dbContext.Playlists
            .Include(p => p.PlaylistMaps)
            .ThenInclude(pm => pm.WorkshopMap)
            .Where(x => x.AuthorId == userId)
            .Select(playlist => WorkshopPlaylistIndexDto.FromEntity(playlist))
            .ToListAsync();
        return playlists;
    }

    public async Task<bool> PutWorkshopPlaylist(string userId, Guid workshopPlaylistId,
        WorkshopPlaylist workshopPlaylist)
    {
        if (workshopPlaylistId != workshopPlaylist.WorkshopPlaylistId || workshopPlaylist.AuthorId != userId)
        {
            return false;
        }

        dbContext.Entry(workshopPlaylist).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteWorkshopPlaylist(string userId, Guid workshopPlaylistId)
    {
        var playlist =
            await dbContext.Playlists.FirstOrDefaultAsync(x =>
                x.AuthorId == userId && x.WorkshopPlaylistId == workshopPlaylistId);
        if (playlist == null) return false;
        dbContext.Playlists.Remove(playlist);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<WorkshopPlaylist?> CreatePlaylist(WorkshopPlaylist playlist)
    {
        if (PlaylistExists(playlist))
        {
            return null;
        }

        dbContext.Playlists.Add(playlist);
        await dbContext.SaveChangesAsync();
        return playlist;
    }

    private bool PlaylistExists(string userId, Guid workshopPlaylistId)
    {
        return dbContext.Playlists.Any(x =>
            x.WorkshopPlaylistId == workshopPlaylistId && x.AuthorId == userId);
    }

    private bool PlaylistExists(WorkshopPlaylist playlist)
    {
        return dbContext.Playlists.Any(x =>
            x.CollectionName == playlist.CollectionName && x.AuthorId == playlist.AuthorId);
    }
}