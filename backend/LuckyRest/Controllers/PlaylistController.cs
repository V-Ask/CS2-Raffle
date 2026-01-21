using LuckyRest.Database.DTOs.Actions;
using Microsoft.AspNetCore.Mvc;
using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.DTOs.Results;
using LuckyRest.Database.DTOs.Views;
using LuckyRest.Database.Entities;
using LuckyRest.Services.WorkshopMapService;
using LuckyRest.Services.WorkshopPlaylistService;
using LuckyRest.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LuckyRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PlaylistController(
        IWorkshopPlaylistService workshopPlaylistService,
        IWorkshopMapService workshopMapService,
        UserManager<User> userManager)
        : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<WorkshopPlaylistViewDto>> GetWorkshopPlaylistView(Guid workshopPlaylistId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var result = await workshopPlaylistService.GetWorkshopPlaylistView(user.Id, workshopPlaylistId);
            if (result.Data == null || result.Status == ServiceResultStatus.NotFound) return NotFound();
            return result.Data;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<GetUserPlaylistsResultDto>> GetUserPlaylists()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var playlists = await workshopPlaylistService.GetWorkshopPlaylists(user.Id);
            if (playlists.Data == null) return StatusCode(StatusCodes.Status500InternalServerError);
            return playlists.Data;
        }

        // POST: api/Playlist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<WorkshopMapDto>> AddMapToWorkshopPlaylist(
            [FromBody] AddWorkshopMapDto addWorkshopMapDto)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var map = await workshopMapService.EnsureWorkshopMap(addWorkshopMapDto.WorkshopId);
            if (map.Data == null) return NotFound();
            var result = await workshopPlaylistService.AddMapToPlaylist(user.Id, addWorkshopMapDto.CollectionId,
                map.Data.MapId);
            if (result.Status == ServiceResultStatus.NoContent) return NoContent();
            return CreatedAtAction("GetWorkshopPlaylistView",
                new { id = result.Data?.WorkshopPlaylistMap.PlaylistId }, map.Data);
        }

        [HttpPost]
        public async Task<ActionResult<CreateWorkshopPlaylistResultDto>> CreateWorkshopPlaylist(
            [FromBody] CreateWorkshopPlaylistDto createWorkshopPlaylistDto
        )
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var playlist = await workshopPlaylistService.CreatePlaylist(user, createWorkshopPlaylistDto.CollectionName);
            if (playlist.Status == ServiceResultStatus.Exists)
            {
                return Conflict();
            }

            return CreatedAtAction("GetWorkshopPlaylistView", new { id = playlist.Data?.Id }, playlist.Data);
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

        [HttpGet("map")]
        public async Task<ActionResult<WorkshopMapDto?>> GetMapFromPlaylist(Guid collectionId, long mapId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var mapExists = await workshopPlaylistService.PlaylistContainsMap(user.Id, collectionId, mapId);
            if (!mapExists)
            {
                return NotFound();
            }

            var mapResult = await workshopMapService.GetWorkshopMap(mapId);
            if (mapResult.Status == ServiceResultStatus.NotFound) return NotFound();
            return mapResult.Data;
        }

        [HttpDelete("map")]
        public async Task<ActionResult> DeleteMapFromPlaylist(Guid collectionId, long mapId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await workshopPlaylistService.DeleteMapFromPlaylist(user.Id, collectionId, mapId);
            return result.Status switch
            {
                ServiceResultStatus.NotFound => NotFound(),
                ServiceResultStatus.Error => StatusCode(StatusCodes.Status500InternalServerError),
                _ => NoContent()
            };
        }

        [HttpPost("all/increase-weight")]
        public async Task<ActionResult> IncreaseWeight(
            [FromBody] IncreasePlaylistWeightsActionDto increasePlaylistWeightsDto)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await workshopPlaylistService.IncreaseAllMapWeights(user.Id,
                increasePlaylistWeightsDto.PlaylistId, increasePlaylistWeightsDto.Increment,
                increasePlaylistWeightsDto.Exceptions);
            return result.Status switch
            {
                ServiceResultStatus.NoContent => NoContent(),
                ServiceResultStatus.Success => Ok(),
                _ => throw new InvalidOperationException()
            };
        }
    }
}