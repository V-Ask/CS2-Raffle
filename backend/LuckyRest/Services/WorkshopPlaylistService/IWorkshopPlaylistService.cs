using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.DTOs.Results;
using LuckyRest.Database.DTOs.Views;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;

namespace LuckyRest.Services.WorkshopPlaylistService;

public interface IWorkshopPlaylistService
{
    public Task<ServiceResult<AddMapToPlaylistResultDto>> AddMapToPlaylist(string userId, Guid workshopPlaylistId, long workshopMapId);

    public Task<WorkshopPlaylistDto?> GetWorkshopPlaylist(string userId, Guid workshopPlaylistId);
    
    public Task<ServiceResult<WorkshopPlaylistViewDto>> GetWorkshopPlaylistView(string userId, Guid workshopPlaylistId);

    public Task<ServiceResult<GetUserPlaylistsResultDto>> GetWorkshopPlaylists(string userId);

    public Task<ServiceResult> DeleteWorkshopPlaylist(string userId, Guid workshopPlaylistId);
    
    public Task<ServiceResult<WorkshopPlaylistDto>> CreatePlaylist(User user, string collectionName);
    
    public Task<bool> PlaylistContainsMap(string userId, Guid workshopPlaylistId, long workshopMapId);
    
    public Task<ServiceResult> DeleteMapFromPlaylist(string userId, Guid workshopPlaylistId, long workshopMapId);
    
    public Task<ServiceResult> IncreaseAllMapWeights(string userId, Guid workshopPlaylistId,
        int increment, long[] exceptions, bool removeExceptions);
    
    public Task<ServiceResult> RateMap(string userId, Guid workshopPlaylistId, long workshopMapId, int newRating);
    
    public Task<ServiceResult> MarkMapAsPlayed(string userId, Guid workshopPlaylistId, long workshopMapId);
    
    public Task<ServiceResult> MarkMapAsUnPlayed(string userId, Guid workshopPlaylistId, long workshopMapId);
}