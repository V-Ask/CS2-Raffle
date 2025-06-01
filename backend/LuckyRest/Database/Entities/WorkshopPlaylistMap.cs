
namespace LuckyRest.Database.Entities;

public class WorkshopPlaylistMap
{
    public Guid WorkshopPlaylistId { get; set; }
    public required WorkshopPlaylist WorkshopPlaylist { get; set; }
    public long WorkshopMapId { get; set; }
    public required WorkshopMap WorkshopMap { get; set; }

    public int Weight { get; set; } = 1;
    public bool HasPlayed { get; set; }
}