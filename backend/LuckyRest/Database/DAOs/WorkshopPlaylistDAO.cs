using LuckyRest.Database.DTOs;
using LuckyRest.Database.DTOs.Results;
using LuckyRest.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Database.DAOs;

public class WorkshopPlaylistDao(LuckyDbContext luckyDbContext)
{
    public async Task<WorkshopPlaylist?> GetWorkshopPlaylist(string userId, int workshopPlaylistId)
    {
        var playlist =
            await luckyDbContext.Playlists.Where(
                x => x.AuthorId == userId && x.WorkshopPlaylistId == workshopPlaylistId).ToListAsync();
        return playlist.FirstOrDefault();
    }

    public async Task<List<WorkshopPlaylist>> GetWorkshopPlaylists(string userId)
    {
        var playlists = await luckyDbContext.Playlists.Where(x => x.AuthorId == userId).ToListAsync();
        return playlists;
    }

    public async Task<bool> PutWorkshopPlaylist(string userId, int workshopPlaylistId,
        WorkshopPlaylist workshopPlaylist)
    {
        if (workshopPlaylistId != workshopPlaylist.WorkshopPlaylistId || workshopPlaylist.AuthorId != userId)
        {
            return false;
        }

        luckyDbContext.Entry(workshopPlaylist).State = EntityState.Modified;
        await luckyDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteWorkshopPlaylist(string userId, int workshopPlaylistId)
    {
        var playlist =
            await luckyDbContext.Playlists.FirstOrDefaultAsync(
                x => x.AuthorId == userId && x.WorkshopPlaylistId == workshopPlaylistId);
        if (playlist == null) return false;
        luckyDbContext.Playlists.Remove(playlist);
        await luckyDbContext.SaveChangesAsync();
        return true;
    }

    private bool PlaylistExists(string userId, int workshopPlaylistId)
    {
        return luckyDbContext.Playlists.Any(x =>
            x.WorkshopPlaylistId == workshopPlaylistId && x.AuthorId == userId);
    }
}