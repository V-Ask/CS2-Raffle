using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DTOs;

public class WorkshopPlaylistDto
{
    public string CollectionName { get; set; } = string.Empty;
    public IList<WorkshopMapDto> Maps { get; set; } = new List<WorkshopMapDto>();

    public static WorkshopPlaylistDto FromEntity(WorkshopPlaylist entity)
    {
        return new WorkshopPlaylistDto
        {
            CollectionName = entity.CollectionName,
            Maps = entity.Maps.Select(WorkshopMapDto.FromEntity).ToList(),
        };
    }
}