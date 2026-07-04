using LuckyRest.Database.Entities;
using LuckyRest.Database.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LuckyRest.Database.EntityConfigurations;

public class WorkshopPlaylistEntityConfiguration : IEntityTypeConfiguration<WorkshopPlaylist>
{
    public void Configure(EntityTypeBuilder<WorkshopPlaylist> builder)
    {
        builder.HasKey(playlist => playlist.WorkshopPlaylistId);

        builder.Property(playlist => playlist.Created)
            .HasConversion(new DateTimeUtcValueConverter());
        
        builder.Property(playlist => playlist.Modified)
            .HasConversion(new DateTimeUtcValueConverter());
    }
}