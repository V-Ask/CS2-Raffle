using LuckyRest.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LuckyRest.Database;

public class User : IdentityUser
{
    public IList<WorkshopPlaylist> Playlists { get; set; } = new List<WorkshopPlaylist>();
}