using System.ComponentModel.DataAnnotations;
using LuckyRest.Database.Entities;
using LuckyRest.Models;
using Microsoft.AspNetCore.Identity;

namespace LuckyRest.Database.Entities;

public class User : IdentityUser
{
    public IList<WorkshopPlaylist> Playlists { get; set; } = new List<WorkshopPlaylist>();
}