using System.Text.Json.Serialization;

namespace Langueedu.Web.Components.Models;

public class StylishModel
{
  public string Answer { get; set; }

  [JsonIgnore]
  public Color Color { get; set; } = Color.Transparent;

  [JsonIgnore]
  public bool IsLeftArrowVisible { get; set; }

  [JsonIgnore]
  public bool IsRightArrowVisible { get; set; }
}
