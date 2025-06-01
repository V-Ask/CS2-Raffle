using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.DTOs.Results;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;

namespace LuckyRest.Services.WorkshopPlaylistService;

public interface IWorkshopPlaylistService
{
    public Task<ServiceResult<AddMapToPlaylistResultDto>> AddMapToPlaylist(string userId, Guid workshopPlaylistId, long workshopMapId);

    public Task<WorkshopPlaylistDto?> GetWorkshopPlaylist(string userId, Guid workshopPlaylistId);

    public Task<ServiceResult<GetUserPlaylistsResultDto>> GetWorkshopPlaylists(string userId);

    public Task<ServiceResult> DeleteWorkshopPlaylist(string userId, Guid workshopPlaylistId);
    public Task<ServiceResult<WorkshopPlaylistDto>> CreatePlaylist(User user, string collectionName);
}