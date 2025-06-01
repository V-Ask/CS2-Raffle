using System.ComponentModel.DataAnnotations;
using LuckyRest.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LuckyRest.Database.Entities;

public class User : IdentityUser
{
    [StringLength(50)]
    public string LaunchServerEndpoint { get; set; } = string.Empty;
    public IList<WorkshopPlaylist> Playlists { get; set; } = new List<WorkshopPlaylist>();
}