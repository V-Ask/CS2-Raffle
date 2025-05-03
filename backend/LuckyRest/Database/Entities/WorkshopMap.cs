using System.ComponentModel.DataAnnotations;

namespace LuckyRest.Database.Entities;

public class WorkshopMap
{
    public int WorkshopMapId { get; set; }
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;
    [StringLength(100)]
    public string ImageUrl { get; set; } = string.Empty;
    [StringLength(600)]
    public string Description { get; set; } = string.Empty;
    public IList<WorkshopPlaylist> Playlists { get; set; } = new List<WorkshopPlaylist>();
}