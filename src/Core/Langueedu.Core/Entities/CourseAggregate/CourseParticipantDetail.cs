using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel;

namespace Langueedu.Core.Entities.CourseAggregate;

public class CourseParticipantDetail : BaseEntity
{
  public CourseParticipantDetail(short score,
                                 byte answerTime,
                                 Stylish stylish,
                                 CourseSuccessStatus courseSuccessStatus)
  {
    Score = score;
    AnswerTime = answerTime;
    Stylish = stylish;
    CourseSuccessStatus = courseSuccessStatus;
  }

  public CourseParticipant CourseParticipant { get; set; }

  public Lyrics Lyrics { get; set; }

  public short Score { get; }

  public byte AnswerTime { get; }

  public Stylish Stylish { get; }

  public CourseSuccessStatus CourseSuccessStatus { get; }
}

