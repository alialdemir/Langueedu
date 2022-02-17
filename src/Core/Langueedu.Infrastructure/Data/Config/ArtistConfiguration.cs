using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
  public void Configure(EntityTypeBuilder<Artist> builder)
  {
    builder.Property(p => p.Name)
        .HasMaxLength(72)
        .IsRequired();

    builder.Property(p => p.Slug)
        .HasMaxLength(100)
        .IsRequired();

    builder.Property(p => p.PicturePath)
        .HasMaxLength(150);

    builder.Property(p => p.CoverPicturePath)
        .HasMaxLength(150);

    builder.Property(p => p.ContentStatus)
        .HasDefaultValue(ContentStatus.Passive);
  }
}

