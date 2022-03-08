using Langueedu.Core.Entities.BalanceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.BalanceConfigs;

public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
{

  public void Configure(EntityTypeBuilder<Balance> builder)
  {
    builder.HasKey(x => x.UserId).HasName("Id");

    builder.Property(p => p.UserId)
        .IsRequired();

    builder.Property(p => p.Gold)
        .IsRequired();

    builder.Property(p => p.Silver)
        .IsRequired();
  }
}

