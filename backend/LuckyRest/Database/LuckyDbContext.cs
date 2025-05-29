using LuckyRest.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Database;

public class LuckyDbContext(DbContextOptions<LuckyDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<WorkshopMap> Maps { get; set; }
    public DbSet<WorkshopPlaylist> Playlists { get; set; }
}