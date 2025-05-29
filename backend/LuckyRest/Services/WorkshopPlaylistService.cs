using LuckyRest.Database;
using LuckyRest.Database.DAOs;
using LuckyRest.Database.DTOs;
using LuckyRest.Database.DTOs.Actions;
using LuckyRest.Database.DTOs.Results;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopScraper.Scraper;

namespace LuckyRest.Services;

public class WorkshopPlaylistService(WorkshopPlaylistDao workshopPlaylistDao, WorkshopMapDao workshopMapDao)
{
    public async Task<ServiceResult<AddMapToPlaylistResultDto>> AddMapToPlaylist(string userId,
        AddMapToPlaylistDto addMapToPlaylistDto)
    {
        var map = await workshopMapDao.GetWorkshopMap(addMapToPlaylistDto.WorkshopMapId);
        var playlist = await workshopPlaylistDao.GetWorkshopPlaylist(userId, addMapToPlaylistDto.WorkshopPlaylistId);
        if (playlist == null || map == null)
        {
            return ServiceResult.NotFound.WithData<AddMapToPlaylistResultDto>(null);
        }

        playlist.Maps.Add(map);
        var result =
            await workshopPlaylistDao.PutWorkshopPlaylist(userId, addMapToPlaylistDto.WorkshopPlaylistId, playlist);
        if (!result)
        {
            return ServiceResult.NotFound.WithData<AddMapToPlaylistResultDto>(null);
        }

        return ServiceResult.Success.WithData(new AddMapToPlaylistResultDto
        {
            WorkshopMap = WorkshopMapDto.FromEntity(map)
        });
    }

    public async Task<WorkshopPlaylistDto?> GetWorkshopPlaylist(string userId, int workshopPlaylistId)
    {
        var workshopPlaylist = await workshopPlaylistDao.GetWorkshopPlaylist(userId, workshopPlaylistId);
        if (workshopPlaylist == null) return null;
        return new WorkshopPlaylistDto
        {
            CollectionName = workshopPlaylist.CollectionName,
            Maps = workshopPlaylist.Maps.Select(WorkshopMapDto.FromEntity).ToList(),
        };
    }

    public async Task<GetUserPlaylistsResultDto> GetWorkshopPlaylists(string userId)
    {
        var playlists = await workshopPlaylistDao.GetWorkshopPlaylists(userId);
        return new GetUserPlaylistsResultDto
        {
            WorkshopPlaylists = playlists.Select(WorkshopPlaylistDto.FromEntity).ToList()
        };
    }

    public async Task<ServiceResult> DeleteWorkshopPlaylist(string userId, int workshopPlaylistId)
    {
        var isDeleted = await workshopPlaylistDao.DeleteWorkshopPlaylist(userId, workshopPlaylistId);
        if (!isDeleted)
            return new ServiceResult
            {
                Status = ServiceResultStatus.NotFound
            };
        return ServiceResult.Success;
    }
}