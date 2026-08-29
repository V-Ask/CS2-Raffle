using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DTOs.Models;

public class WorkshopPlaylistIndexDto
{
    public string CollectionName { get; set; } = string.Empty;
    public string PlaylistId { get; set; } = string.Empty;
    public int Size { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Modified { get; set; }

    public static WorkshopPlaylistIndexDto FromEntity(WorkshopPlaylist playlist)
    {
        return new WorkshopPlaylistIndexDto
        {
            CollectionName = playlist.CollectionName,
            PlaylistId = playlist.WorkshopPlaylistId.ToString(),
            Size = playlist.PlaylistMaps.Count,
            Created = playlist.Created,
            Modified = playlist.Modified
        };
    }
}