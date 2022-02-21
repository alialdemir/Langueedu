using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.PlaylistConfigs;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{

  public void Configure(EntityTypeBuilder<Album> builder)
  {
    builder.Property(p => p.Name)
        .HasMaxLength(72)
        .IsRequired();

    builder.Property(p => p.Slug)
        .HasMaxLength(100)
        .IsUnicode()
        .IsRequired();

    builder.Property(p => p.ContentStatus)
        .HasDefaultValue(ContentStatus.Passive);


    //builder
    //  .HasMany(dm => dm.Images)
    //  .WithOne()
    //  .OnDelete(DeleteBehavior.Restrict);
  }
}

