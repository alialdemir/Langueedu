using Langueedu.Core.Entities.BalanceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.BalanceConfigs;

public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
{

  public void Configure(EntityTypeBuilder<Balance> builder)
  {
    builder.Property(p => p.UserId)
        .IsRequired();

    builder.Property(p => p.Gold)
        .IsRequired();
  }
}

