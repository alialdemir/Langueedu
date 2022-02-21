using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.PlaylistConfigs;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
  public void Configure(EntityTypeBuilder<Track> builder)
  {
    builder.Property(p => p.Name)
        .HasMaxLength(72)
        .IsRequired();

    builder.Property(p => p.Slug)
        .HasMaxLength(72)
        .IsUnicode()
        .IsRequired();

    builder.Property(p => p.YoutubeVideoId)
        .HasMaxLength(16)
        .IsRequired();

    builder.Property(p => p.Duration)
        .IsRequired();

    builder.Property(p => p.FollowerCount)
        .HasDefaultValue(0);

    builder.Property(p => p.ContentStatus)
        .HasDefaultValue(ContentStatus.Passive);

    builder.Ignore(x => x.Images);
  }
}

