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
            .HasKey(x => new { x.WorkshopPlaylistId, x.WorkshopMapId }); // Combine foreign keys to create composite
        
        builder.Entity<WorkshopPlaylistMap>() // Each WorkshopPlaylistMap has...
            .HasOne(x => x.WorkshopPlaylist) // exactly one WorkshopPlaylist,
            .WithMany(p => p.PlaylistMaps) // which can contain multiple PlaylistMaps
            .HasForeignKey(x => x.WorkshopPlaylistId); // which is identified using the WorkshopPlaylistId column
        
        builder.Entity<WorkshopPlaylistMap>() // Each WorkshopPlaylistMap has...
            .HasOne(x => x.WorkshopMap) // exactly one Map,
            .WithMany(m => m.Playlists) // and one Map can be in multiple PlaylistMaps
            .HasForeignKey(x => x.WorkshopMapId) // which is identified using the WorkshopMapId column
            .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of a map to cascade and delete the map from all parents
    }
}