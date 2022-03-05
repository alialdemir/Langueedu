using System.Drawing;
using System.Text.Json.Serialization;

namespace Langueedu.Sdk.Course.Response
{
  public class StylishModel
  {
    public string Answer { get; set; }

    [JsonIgnore]
    public object Color { get; set; } = 9;

    [JsonIgnore]
    public bool IsLeftArrowVisible { get; set; }

    [JsonIgnore]
    public bool IsRightArrowVisible { get; set; }
  }
}

