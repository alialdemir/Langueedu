using System.Text.Json.Serialization;

namespace Langueedu.Sdk.Course.Response
{
  public class CourseIndicatorModel
  {
    public short TotalScore { get; set; }

    public short FinishScore { get; set; }

    public short VictoryScore { get; set; }

    public short TotalFails { get; set; }

    public short TotalHits { get; set; }

    public short TotalUserGaps { get; set; }

    public short Gaps { get; set; }

    [JsonIgnore]
    public byte Time { get; set; } = 100;
  }
}
