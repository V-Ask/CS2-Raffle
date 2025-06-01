using LuckyRest.Database.DAOs.WorkshopMapDao;
using LuckyRest.Database.DAOs.WorkshopPlaylistDao;
using LuckyRest.Database.DAOs.WorkshopPlaylistMapDao;
using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.DTOs.Results;
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
        var playlist = await workshopPlaylistDao.GetWorkshopPlaylist(userId, workshopPlaylistId);
        if (playlist == null)
        {
            return ServiceResult.NotFound.WithData<AddMapToPlaylistResultDto>(null);
        }

        var map = await workshopMapDao.GetWorkshopMap(workshopMapId);
        if (map == null)
        {
            return ServiceResult.NotFound.WithData<AddMapToPlaylistResultDto>(null);
        }

        var playlistMap = new WorkshopPlaylistMap
        {
            WorkshopMap = map,
            WorkshopPlaylist = playlist
        };
        await workshopPlaylistMapDao.PostWorkshopPlaylistMap(playlistMap);
        playlist.PlaylistMaps.Add(playlistMap);

        var result =
            await workshopPlaylistDao.PutWorkshopPlaylist(userId, workshopPlaylistId ,playlist);
        if (!result)
        {
            return ServiceResult.NotFound.WithData<AddMapToPlaylistResultDto>(null);
        }

        return ServiceResult.Success.WithData(new AddMapToPlaylistResultDto
        {
            WorkshopPlaylistMap = WorkshopPlaylistMapDto.FromEntity(playlistMap)
        });
    }

    public async Task<WorkshopPlaylistDto?> GetWorkshopPlaylist(string userId, Guid workshopPlaylistId)
    {
        var workshopPlaylist = await workshopPlaylistDao.GetWorkshopPlaylist(userId, workshopPlaylistId);
        if (workshopPlaylist == null) return null;
        return new WorkshopPlaylistDto
        {
            CollectionName = workshopPlaylist.CollectionName,
            Maps = workshopPlaylist.PlaylistMaps.Select(WorkshopPlaylistMapDto.FromEntity).ToList(),
        };
    }

    public async Task<ServiceResult<GetUserPlaylistsResultDto>> GetWorkshopPlaylists(string userId)
    {
        var playlists = await workshopPlaylistDao.GetWorkshopPlaylists(userId);
        return ServiceResult.Success.WithData(new GetUserPlaylistsResultDto
        {
            WorkshopPlaylists = playlists.Select(WorkshopPlaylistDto.FromEntity).ToList()
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
}