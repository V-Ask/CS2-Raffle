using LuckyRest.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Database;

public class LuckyDbContext(DbContextOptions<LuckyDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<WorkshopMap> Maps { get; set; }
    public DbSet<WorkshopPlaylist> Playlists { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>()
            .HasMany(e => e.Playlists)
            .WithOne(e => e.Author)
            .HasForeignKey(e => e.AuthorId)
            .HasPrincipalKey(e => e.Id);
    }
}