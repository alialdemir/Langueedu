using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.PlaylistConfigs;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
  public void Configure(EntityTypeBuilder<Playlist> builder)
  {
    builder.Property(p => p.PlaylistName)
        .HasMaxLength(72)
        .IsRequired();

    builder.Property(p => p.Slug)
        .HasMaxLength(72)
        .IsRequired();

    builder.Property(p => p.ContentStatus)
        .HasDefaultValue(ContentStatus.Passive);

    builder.Property(p => p.DisplayOrder)
        .HasDefaultValue(0);
  }
}

