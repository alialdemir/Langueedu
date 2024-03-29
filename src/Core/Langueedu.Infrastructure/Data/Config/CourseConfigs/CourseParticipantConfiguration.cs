﻿using Langueedu.Core.Entities.CourseAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Langueedu.Infrastructure.Data.Config.CourseConfigs;

public class CourseParticipantConfiguration : IEntityTypeConfiguration<CourseParticipant>
{

  public void Configure(EntityTypeBuilder<CourseParticipant> builder)
  {
    builder.Property(p => p.UserId)
        .HasMaxLength(255)
        .IsRequired();

    builder.Property(p => p.DuelTypes)
        .IsRequired();

    builder.Property(p => p.TotalScore)
        .IsRequired(false);

    builder.Property(p => p.FinishScore)
        .IsRequired(false);

    builder.Property(p => p.VictoryScore)
        .IsRequired(false);

    builder.Property(p => p.TotalFails)
        .IsRequired(false);

    builder.Property(p => p.TotalHits)
        .IsRequired(false);

    builder.Property(p => p.TotalUserGaps)
        .IsRequired(false);

    builder.Property(p => p.Gaps)
        .IsRequired(false);
  }
}

