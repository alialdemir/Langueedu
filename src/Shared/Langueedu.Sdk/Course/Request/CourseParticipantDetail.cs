using Langueedu.Sdk.Enums;

namespace Langueedu.Sdk.Course.Request
{
  public class CourseParticipantDetail
  {
    public int LyricsId { get; set; }

    public short Score { get; set; }

    public byte AnswerTime { get; set; }

    public Stylish Stylish { get; set; }

    public CourseSuccessStatus CourseSuccessStatus { get; set; }
  }
}

