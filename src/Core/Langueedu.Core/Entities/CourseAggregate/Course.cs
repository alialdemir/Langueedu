using Langueedu.Core.Enums;
using Langueedu.SharedKernel;

namespace Langueedu.Core.Entities.CourseAggregate;

public class Course : BaseEntity
{
  public Course(BalanceTypes balanceType, CourseLevel courseLevel, CourseModes gameMode, decimal courseFee)
  {
    BalanceType = balanceType;
    CourseLevel = courseLevel;
    GameMode = gameMode;
    CourseFee = courseFee;
  }

  public BalanceTypes BalanceType { get; }

  public CourseLevel CourseLevel { get; }

  public CourseModes GameMode { get; }

  public decimal CourseFee { get; }

  private readonly List<CourseParticipant> _duelParticipants = new();

  public IReadOnlyCollection<CourseParticipant> DuelParticipants => _duelParticipants.AsReadOnly();
}

