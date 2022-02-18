namespace Langueedu.Web.Components.Models;

public class LyricsModel
{
  public double Duration { get; set; }

  public string Text { get; set; }

  public List<StylishModel> Stylish { get; set; } = new();
}
