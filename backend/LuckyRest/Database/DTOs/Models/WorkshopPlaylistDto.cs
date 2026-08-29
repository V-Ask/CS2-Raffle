using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DTOs.Models;

public class WorkshopPlaylistDto
{
    public string CollectionName { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public IList<WorkshopMapDto> Maps { get; set; } = new List<WorkshopMapDto>();

    public static WorkshopPlaylistDto FromEntity(WorkshopPlaylist entity)
    {
        return new WorkshopPlaylistDto
        {
            CollectionName = entity.CollectionName,
            Id = entity.WorkshopPlaylistId,
            Maps = entity.PlaylistMaps.Select(x => WorkshopMapDto.FromEntity(x.WorkshopMap)).ToList()
        };
    }
}