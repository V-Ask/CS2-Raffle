using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DTOs.Models;

public class WorkshopPlaylistMapDto
{
    public WorkshopMapDto WorkshopMap { get; set; }
    public WorkshopPlaylistDto WorkshopPlaylist { get; set; }
    public int Weight { get; set; }
    public bool HasPlayed { get; set; }

    public static WorkshopPlaylistMapDto FromEntity(WorkshopPlaylistMap? entity)
    {
        if(entity == null) return new WorkshopPlaylistMapDto();
        return new WorkshopPlaylistMapDto
        {
            WorkshopMap = WorkshopMapDto.FromEntity(entity.WorkshopMap),
            WorkshopPlaylist = WorkshopPlaylistDto.FromEntity(entity.WorkshopPlaylist),
            Weight = entity.Weight,
            HasPlayed = entity.HasPlayed
        };
    }
}