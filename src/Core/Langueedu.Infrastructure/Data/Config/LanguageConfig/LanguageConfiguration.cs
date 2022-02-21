using Langueedu.Core.Entities.LanguageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.LanguageConfig;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{

  public void Configure(EntityTypeBuilder<Language> builder)
  {
    builder.HasKey(x => x.Lang);

    builder.Property(p => p.Lang)
        .IsRequired();

    builder.Property(p => p.LangCc);
  }
}
