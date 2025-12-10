using LuckyRest.Database.DAOs.WorkshopMapDao;
using LuckyRest.Database.DAOs.WorkshopPlaylistDao;
using LuckyRest.Database.DAOs.WorkshopPlaylistMapDao;
using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.DTOs.Results;
using LuckyRest.Database.DTOs.Views;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;

namespace LuckyRest.Services.WorkshopPlaylistService;

public class WorkshopPlaylistService(
    IWorkshopPlaylistDao workshopPlaylistDao,
    IWorkshopMapDao workshopMapDao,
    IWorkshopPlaylistMapDao workshopPlaylistMapDao) : IWorkshopPlaylistService
{
    public async Task<ServiceResult<AddMapToPlaylistResultDto>> AddMapToPlaylist(string userId, Guid workshopPlaylistId,
        long workshopMapId)
    {
        var notFoundResult = ServiceResult.NotFound.WithData<AddMapToPlaylistResultDto>(null);
        var workshopPlaylistMap = await JoinPlaylistAndMap(userId, workshopPlaylistId, workshopMapId);
        if (workshopPlaylistMap == null)
        {
            return notFoundResult;
        }

        var result =
            await workshopPlaylistDao.AddMapToWorkshopPlaylist(userId, workshopPlaylistId, workshopPlaylistMap);
        if (!result)
        {
            return notFoundResult;
        }

        return ServiceResult.Success.WithData(new AddMapToPlaylistResultDto
        {
            WorkshopPlaylistMap = WorkshopPlaylistMapDto.FromEntity(workshopPlaylistMap)
        });
    }

    private async Task<WorkshopPlaylistMap?> JoinPlaylistAndMap(string userId, Guid workshopPlaylistId,
        long workshopMapId)
    {
        var playlist = await workshopPlaylistDao.GetWorkshopPlaylist(userId, workshopPlaylistId);
        if (playlist == null)
        {
            return null;
        }

        var map = await workshopMapDao.GetWorkshopMap(workshopMapId);
        if (map == null)
        {
            return null;
        }

        var playlistMap = WorkshopPlaylistMap.Join(playlist, map);
        var postResult = await workshopPlaylistMapDao.PostWorkshopPlaylistMap(playlistMap);
        return !postResult ? null : playlistMap;
    }

    public async Task<WorkshopPlaylistDto?> GetWorkshopPlaylist(string userId, Guid workshopPlaylistId)
    {
        var workshopPlaylist = await workshopPlaylistDao.GetWorkshopPlaylist(userId, workshopPlaylistId);
        return workshopPlaylist == null ? null : WorkshopPlaylistDto.FromEntity(workshopPlaylist);
    }

    public async Task<ServiceResult<WorkshopPlaylistViewDto>> GetWorkshopPlaylistView(string userId,
        Guid workshopPlaylistId)
    {
        var workshopPlaylist = await workshopPlaylistDao.GetWorkshopPlaylist(userId, workshopPlaylistId);
        return workshopPlaylist == null
            ? ServiceResult.NotFound.WithData<WorkshopPlaylistViewDto>(null)
            : ServiceResult.Success.WithData(WorkshopPlaylistViewDto.FromEntity(workshopPlaylist));
    }

    public async Task<ServiceResult<GetUserPlaylistsResultDto>> GetWorkshopPlaylists(string userId)
    {
        var playlists = await workshopPlaylistDao.GetWorkshopPlaylists(userId);
        return ServiceResult.Success.WithData(new GetUserPlaylistsResultDto
        {
            WorkshopPlaylists = playlists.Select(WorkshopPlaylistIndexDto.FromEntity).ToList()
        });
    }

    public async Task<ServiceResult> DeleteWorkshopPlaylist(string userId, Guid workshopPlaylistId)
    {
        var isDeleted = await workshopPlaylistDao.DeleteWorkshopPlaylist(userId, workshopPlaylistId);
        if (!isDeleted)
            return new ServiceResult
            {
                Status = ServiceResultStatus.NotFound
            };
        return ServiceResult.Success;
    }

    public async Task<ServiceResult<WorkshopPlaylistDto>> CreatePlaylist(User user, string collectionName)
    {
        var playlist = new WorkshopPlaylist
        {
            Author = user,
            CollectionName = collectionName
        };
        var result = await workshopPlaylistDao.CreatePlaylist(playlist);
        return result == null
            ? ServiceResult.Exists.WithData<WorkshopPlaylistDto>(null)
            : ServiceResult.Success.WithData(WorkshopPlaylistDto.FromEntity(result));
    }

    public async Task<bool> PlaylistContainsMap(string userId, Guid workshopPlaylistId, long workshopMapId)
    {
        return await workshopPlaylistDao.PlaylistContainsMap(userId, workshopPlaylistId, workshopMapId);
    }
}