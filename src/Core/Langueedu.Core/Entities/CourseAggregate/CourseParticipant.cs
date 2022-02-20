using Langueedu.Core.Enums;
using Langueedu.SharedKernel;

namespace Langueedu.Core.Entities.CourseAggregate;

public class CourseParticipant : BaseEntity
{
  public CourseParticipant(string userId,
                           CourseStatus duelTypes)
  {
    UserId = userId;
    DuelTypes = duelTypes;
  }

  public Course Course { get; }

  public string UserId { get; }

  public CourseStatus DuelTypes { get; }

  public short? TotalScore { get; }

  public short? FinishScore { get; }

  public short? VictoryScore { get; }

  public short? TotalFails { get; }

  public short? TotalHits { get; }

  public short? TotalUserGaps { get; }

  public short? Gaps { get; }

  private readonly List<CourseParticipantDetail> _courseParticipantDetails = new();

  public IReadOnlyCollection<CourseParticipantDetail> CourseParticipantDetail => _courseParticipantDetails.AsReadOnly();
}