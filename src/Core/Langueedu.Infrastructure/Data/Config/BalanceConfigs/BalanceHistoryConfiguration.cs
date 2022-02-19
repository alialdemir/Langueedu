using Langueedu.Core.Entities.BalanceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.BalanceConfigs;

public class BalanceHistoryConfiguration : IEntityTypeConfiguration<BalanceHistory>
{

  public void Configure(EntityTypeBuilder<BalanceHistory> builder)
  {
    builder.Property(p => p.UserId)
        .IsRequired();

    builder.Property(p => p.BalanceHistoryType)
        .IsRequired();

    builder.Property(p => p.BalanceType)
        .IsRequired();

    builder.Property(p => p.OldAmount)
        .IsRequired();

    builder.Property(p => p.NewAmount)
        .IsRequired();
  }
}

