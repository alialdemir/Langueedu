using Langueedu.Core.Entities.PlaylistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.PlaylistConfigs;

public class LyricsConfiguration : IEntityTypeConfiguration<Lyrics>
{
  public void Configure(EntityTypeBuilder<Lyrics> builder)
  {
    builder.Property(p => p.Text)
        .HasMaxLength(300)
        .IsRequired();

    builder.Property(p => p.Answer)
        .HasMaxLength(50)
        .IsRequired();

    builder.Property(p => p.AnswerIndex);

    builder.Property(p => p.Duration)
        .IsRequired();
  }
}
