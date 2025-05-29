
namespace LuckyRest.Database.Entities;

public class WorkshopPlaylistMap
{
    public int WorkshopPlaylistId { get; set; }
    public required WorkshopPlaylist WorkshopPlaylist { get; set; }
    public int WorkshopMapId { get; set; }
    public required WorkshopMap WorkshopMap { get; set; }

    public int Weight { get; set; } = 1;
    public bool HasPlayed { get; set; }
}