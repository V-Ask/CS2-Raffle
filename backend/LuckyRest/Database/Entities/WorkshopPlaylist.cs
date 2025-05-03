using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Database.Entities;

[Index(nameof(CollectionName), nameof(AuthorId), IsUnique = true)]
public class WorkshopPlaylist
{
    public int WorkshopPlaylistId { get; set; }
    [StringLength(50)]
    public string CollectionName { get; set; } = string.Empty;
    public IList<WorkshopMap> Maps { get; set; } = new List<WorkshopMap>();
    
    public string AuthorId { get; set; }
    public User Author { get; set; }
}