using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DTOs.Views;

public class WorkshopPlaylistMapViewDto
{
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long MapId { get; set; }
    public Guid PlaylistId { get; set; }
    public int Weight { get; set; }
    public bool HasPlayed { get; set; }

    public static WorkshopPlaylistMapViewDto FromEntity(WorkshopPlaylistMap? entity)
    {
        if (entity == null) return new WorkshopPlaylistMapViewDto();
        var workshopMap = entity.WorkshopMap;
        return new WorkshopPlaylistMapViewDto
        {
            Name = workshopMap.Name,
            ImageUrl = workshopMap.ImageUrl,
            Description = workshopMap.Description,
            MapId = entity.WorkshopMapId,
            PlaylistId = entity.WorkshopPlaylistId,
            Weight = entity.Weight,
            HasPlayed = entity.HasPlayed,
        };
    }
}