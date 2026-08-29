using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DTOs.Models;

public class WorkshopPlaylistMapDto
{
    public long MapId { get; set; }
    public Guid PlaylistId { get; set; }
    public int Weight { get; set; }
    public bool HasPlayed { get; set; }

    public static WorkshopPlaylistMapDto FromEntity(WorkshopPlaylistMap? entity)
    {
        if(entity == null) return new WorkshopPlaylistMapDto();
        return new WorkshopPlaylistMapDto
        {
            MapId = entity.WorkshopMapId,
            PlaylistId = entity.WorkshopPlaylistId,
            Weight = entity.Weight,
            HasPlayed = entity.HasPlayed
        };
    }
}