using LuckyRest.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Database;

public class LuckyDbContext(DbContextOptions<LuckyDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<WorkshopMap> Maps { get; set; }
    public DbSet<WorkshopPlaylist> Playlists { get; set; }
    public DbSet<WorkshopPlaylistMap> PlaylistMaps { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ModelPlaylistMaps(builder);
    }

    private static void ModelPlaylistMaps(ModelBuilder builder)
    {
        builder.Entity<WorkshopPlaylistMap>()
            .HasKey(x => new { x.WorkshopPlaylistId, x.WorkshopMapId });
        
        builder.Entity<WorkshopPlaylistMap>()
            .HasOne(x => x.WorkshopPlaylist)
            .WithMany(p => p.PlaylistMaps)
            .HasForeignKey(x => x.WorkshopPlaylistId);
        
        builder.Entity<WorkshopPlaylistMap>()
            .HasOne(x => x.WorkshopMap)
            .WithMany(m => m.Playlists)
            .HasForeignKey(x => x.WorkshopMapId);
    }
}