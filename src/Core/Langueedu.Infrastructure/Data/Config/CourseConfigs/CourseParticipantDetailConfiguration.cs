using Langueedu.Core.Entities.CourseAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.CourseConfigs;

public class CourseParticipantDetailConfiguration : IEntityTypeConfiguration<CourseParticipantDetail>
{

  public void Configure(EntityTypeBuilder<CourseParticipantDetail> builder)
  {
    builder.Property(p => p.Score)
        .IsRequired();

    builder.Property(p => p.AnswerTime)
        .IsRequired();

    builder.Property(p => p.Stylish)
        .IsRequired();

    builder.Property(p => p.CourseSuccessStatus)
        .IsRequired();
  }
}

