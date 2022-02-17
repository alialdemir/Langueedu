using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{

  public void Configure(EntityTypeBuilder<Album> builder)
  {
    builder.Property(p => p.Name)
        .HasMaxLength(72)
        .IsRequired();

    builder.Property(p => p.Slug)
        .HasMaxLength(100)
        .IsRequired();

    builder.Property(p => p.ReleaseDate);

    builder.Property(p => p.AlbumCoverImage)
        .HasMaxLength(150);

    builder.Property(p => p.ContentStatus)
        .HasDefaultValue(ContentStatus.Passive);
  }
}

