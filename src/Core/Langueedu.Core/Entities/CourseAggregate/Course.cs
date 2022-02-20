using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.CourseAggregate;

public class Course : BaseEntity, IAggregateRoot
{
  public Course(BalanceTypes balanceType,
                CourseLevel courseLevel,
                CourseModes courseMode,
                decimal courseFee)
  {
    BalanceType = balanceType;
    CourseLevel = courseLevel;
    CourseMode = courseMode;
    CourseFee = courseFee;
  }

  public BalanceTypes BalanceType { get; }

  public CourseLevel CourseLevel { get; }

  public CourseModes CourseMode { get; }

  public Track Track { get; set; }

  public decimal CourseFee { get; }

  private readonly List<CourseParticipant> _courseParticipants = new();

  public IReadOnlyCollection<CourseParticipant> CourseParticipants => _courseParticipants.AsReadOnly();


  public Course AddParticipant(CourseParticipant album)
  {
    _courseParticipants.Add(album);

    return this;
  }
}

