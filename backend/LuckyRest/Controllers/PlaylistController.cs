using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuckyRest.Database;
using LuckyRest.Database.DTOs;
using LuckyRest.Database.DTOs.Actions;
using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.DTOs.Results;
using LuckyRest.Database.Entities;
using LuckyRest.Services;
using LuckyRest.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LuckyRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaylistController(
        WorkshopPlaylistService workshopPlaylistService,
        WorkshopMapService workshopMapService,
        UserManager<User> userManager)
        : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<WorkshopPlaylistDto>> GetWorkshopPlaylist(int workshopPlaylistId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var playlist = await workshopPlaylistService.GetWorkshopPlaylist(user.Id, workshopPlaylistId);
            if (playlist == null) return NotFound();
            return playlist;
        }

        [HttpGet]
        public async Task<ActionResult<GetUserPlaylistsResultDto>> GetUserPlaylists()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var playlists = await workshopPlaylistService.GetWorkshopPlaylists(user.Id);
            return playlists;
        }

        // POST: api/Playlist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<WorkshopMapDto>> AddMapToWorkshopPlaylist(
            AddMapToPlaylistDto addMapToPlaylistDto)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var map = await workshopMapService.AddWorkshopMap(addMapToPlaylistDto.WorkshopMapId);
            var result = await workshopPlaylistService.AddMapToPlaylist(user.Id, addMapToPlaylistDto);
            if (result.Status == ServiceResultStatus.NoContent) return NoContent();
            return CreatedAtAction("GetWorkshopPlaylist", new { id = result.Data?.WorkshopPlaylistMap.WorkshopPlaylist.Id },
                map);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteWorkshopPlaylist(int workshopPlaylistId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await workshopPlaylistService.DeleteWorkshopPlaylist(user.Id, workshopPlaylistId);
            if (result.Status == ServiceResultStatus.NotFound) return NotFound();
            return NoContent();
        }
    }
}