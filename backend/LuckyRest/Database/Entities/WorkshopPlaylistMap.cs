
namespace LuckyRest.Database.Entities;

public class WorkshopPlaylistMap
{
    public required WorkshopPlaylist WorkshopPlaylist { get; set; }
    public required WorkshopMap WorkshopMap { get; set; }
    public Guid WorkshopPlaylistId { get; set; }
    public long WorkshopMapId { get; set; }
    public int Weight { get; set; } = 1;
    public bool HasPlayed { get; set; }
    public int Rating { get; set; } = 0;
    public static WorkshopPlaylistMap Join(WorkshopPlaylist playlist, WorkshopMap map)
    {
        return new WorkshopPlaylistMap
        {
            WorkshopPlaylist = playlist,
            WorkshopMap = map,
            WorkshopPlaylistId = playlist.WorkshopPlaylistId,
            WorkshopMapId = map.WorkshopMapId,
        };
    }
}