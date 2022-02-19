using Langueedu.Core.Entities.CourseAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.CourseConfigs;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{

  public void Configure(EntityTypeBuilder<Course> builder)
  {
    builder.Property(p => p.BalanceType)
        .IsRequired();

    builder.Property(p => p.CourseLevel)
        .IsRequired();

    builder.Property(p => p.GameMode)
        .IsRequired();

    builder.Property(p => p.CourseFee)
        .IsRequired();
  }
}

