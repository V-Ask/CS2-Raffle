using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DTOs.Models;

public class WorkshopPlaylistDto
{
    public string CollectionName { get; set; } = string.Empty;
    public Guid Id { get; set; } 
    public IList<WorkshopPlaylistMapDto> Maps { get; set; } = new List<WorkshopPlaylistMapDto>();

    public static WorkshopPlaylistDto FromEntity(WorkshopPlaylist entity)
    {
        return new WorkshopPlaylistDto
        {
            CollectionName = entity.CollectionName,
            Id = entity.WorkshopPlaylistId,
            Maps = entity.PlaylistMaps.Select(WorkshopPlaylistMapDto.FromEntity).ToList(),
        };
    }
}