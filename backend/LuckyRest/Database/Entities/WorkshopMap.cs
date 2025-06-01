using System.ComponentModel.DataAnnotations;

namespace LuckyRest.Database.Entities;

public class WorkshopMap
{
    public long WorkshopMapId { get; set; }
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;
    [StringLength(600)]
    public string ImageUrl { get; set; } = string.Empty;
    [StringLength(600)]
    public string Description { get; set; } = string.Empty;
    public IList<WorkshopPlaylistMap> Playlists { get; set; } = new List<WorkshopPlaylistMap>();
}