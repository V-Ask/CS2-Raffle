using Microsoft.AspNetCore.Mvc;
using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.DTOs.Results;
using LuckyRest.Database.Entities;
using LuckyRest.Services.WorkshopMapService;
using LuckyRest.Services.WorkshopPlaylistService;
using LuckyRest.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LuckyRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaylistController(
        IWorkshopPlaylistService workshopPlaylistService,
        IWorkshopMapService workshopMapService,
        UserManager<User> userManager)
        : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<WorkshopPlaylistDto>> GetWorkshopPlaylist(Guid workshopPlaylistId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var playlist = await workshopPlaylistService.GetWorkshopPlaylist(user.Id, workshopPlaylistId);
            if (playlist == null) return NotFound();
            return playlist;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<GetUserPlaylistsResultDto>> GetUserPlaylists()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var playlists = await workshopPlaylistService.GetWorkshopPlaylists(user.Id);
            if(playlists.Data == null) return StatusCode(StatusCodes.Status500InternalServerError);
            return playlists.Data;
        }

        // POST: api/Playlist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("add")]
        public async Task<ActionResult<WorkshopMapDto>> AddMapToWorkshopPlaylist(
             Guid workshopPlaylistId, long workshopMapId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var map = await workshopMapService.AddWorkshopMap(workshopMapId);
            var result = await workshopPlaylistService.AddMapToPlaylist(user.Id, workshopPlaylistId, workshopMapId);
            if (result.Status == ServiceResultStatus.NoContent) return NoContent();
            return CreatedAtAction("GetWorkshopPlaylist", new { id = result.Data?.WorkshopPlaylistMap.WorkshopPlaylist.Id },
                map.Data);
        }

        [HttpPost]
        public async Task<ActionResult<WorkshopPlaylistDto>> CreateWorkshopPlaylist(string collectionName)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var playlist = await workshopPlaylistService.CreatePlaylist(user, collectionName);
            if (playlist.Status == ServiceResultStatus.Exists)
            {
                return Conflict();
            }
            return CreatedAtAction("GetWorkshopPlaylist", new { id = playlist.Data?.Id }, playlist.Data);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteWorkshopPlaylist(Guid workshopPlaylistId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await workshopPlaylistService.DeleteWorkshopPlaylist(user.Id, workshopPlaylistId);
            if (result.Status == ServiceResultStatus.NotFound) return NotFound();
            return NoContent();
        }
    }
}