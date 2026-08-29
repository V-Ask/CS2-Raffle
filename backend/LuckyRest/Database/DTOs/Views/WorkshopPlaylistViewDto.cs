using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DTOs.Views;

public class WorkshopPlaylistViewDto
{
    public string CollectionName { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public IList<WorkshopPlaylistMapViewDto>  Maps { get; set; } = new List<WorkshopPlaylistMapViewDto>();

    public static WorkshopPlaylistViewDto FromEntity(WorkshopPlaylist? entity)
    {
        if (entity == null) return new WorkshopPlaylistViewDto();
        return new WorkshopPlaylistViewDto
        {
            CollectionName = entity.CollectionName,
            Id = entity.WorkshopPlaylistId,
            Maps = entity.PlaylistMaps.Select(WorkshopPlaylistMapViewDto.FromEntity).ToList()
        };
    }
}