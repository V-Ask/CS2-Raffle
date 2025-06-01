using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DAOs.WorkshopPlaylistDao;

public interface IWorkshopPlaylistDao
{
    public Task<WorkshopPlaylist?> GetWorkshopPlaylist(string userId, Guid workshopPlaylistId);
    public Task<List<WorkshopPlaylist>> GetWorkshopPlaylists(string userId);
    public Task<bool> PutWorkshopPlaylist(string userId, Guid workshopPlaylistId, WorkshopPlaylist workshopPlaylist);
    public Task<bool> DeleteWorkshopPlaylist(string userId, Guid workshopPlaylistId);
    public Task<WorkshopPlaylist?> CreatePlaylist(WorkshopPlaylist playlist);
}