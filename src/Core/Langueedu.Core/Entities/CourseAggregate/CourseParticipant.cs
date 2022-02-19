using Langueedu.Core.Enums;
using Langueedu.SharedKernel;

namespace Langueedu.Core.Entities.CourseAggregate;

public class CourseParticipant : BaseEntity
{
  public CourseParticipant(string userId,
                         CourseStatus duelTypes,
                         short totalScore,
                         short finishScore,
                         short victoryScore,
                         byte totalFails,
                         byte totalHits,
                         byte totalUserGaps,
                         byte gapsCount)
  {
    UserId = userId;
    DuelTypes = duelTypes;
    TotalScore = totalScore;
    FinishScore = finishScore;
    VictoryScore = victoryScore;
    TotalFails = totalFails;
    TotalHits = totalHits;
    TotalUserGaps = totalUserGaps;
    GapsCount = gapsCount;
  }

  public Course Duel { get; }
  public string UserId { get; }
  public CourseStatus DuelTypes { get; }
  public short TotalScore { get; }
  public short FinishScore { get; }
  public short VictoryScore { get; }
  public byte TotalFails { get; }
  public byte TotalHits { get; }
  public byte TotalUserGaps { get; }
  public byte GapsCount { get; }

  private readonly List<CourseParticipantDetail> _duelParticipantDetails = new();

  public IReadOnlyCollection<CourseParticipantDetail> DuelParticipantDetails => _duelParticipantDetails.AsReadOnly();
}