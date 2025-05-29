using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Database.Entities;

[Index(nameof(CollectionName), nameof(AuthorId), IsUnique = true)]
public class WorkshopPlaylist
{
    public int WorkshopPlaylistId { get; set; }
    [StringLength(50)]
    public string CollectionName { get; set; } = string.Empty;
    public IList<WorkshopPlaylistMap> PlaylistMaps { get; set; } = new List<WorkshopPlaylistMap>();

    [StringLength(500)]
    public required string AuthorId { get; set; } = string.Empty;
    public required User Author { get; set; }
}