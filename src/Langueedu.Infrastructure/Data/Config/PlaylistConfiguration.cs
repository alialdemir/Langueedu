using Langueedu.Core.Entities.PlaylistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.Property(p => p.PlaylistName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Slug)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.DisplayOrder)
            .HasDefaultValue(0);
    }
}

