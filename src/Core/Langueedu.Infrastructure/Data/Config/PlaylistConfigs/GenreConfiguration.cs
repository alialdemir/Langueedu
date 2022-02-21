using Langueedu.Core.Entities.PlaylistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.PlaylistConfigs;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
  public void Configure(EntityTypeBuilder<Genre> builder)
  {
    //builder.HasKey(x => x.Description);

    builder.Property(p => p.Description)
        .IsRequired();

    builder.Property(p => p.Slug)
      .IsUnicode()
      .IsRequired();
  }
}
