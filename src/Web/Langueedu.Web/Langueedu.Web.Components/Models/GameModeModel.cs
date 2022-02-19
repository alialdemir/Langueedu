
namespace Langueedu.Web.Components.Models;
public class GameModeModel
{
  public short EntryFee { get; set; }
  public short CourseFee { get; set; }
  public string Image { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public byte ProgressBar { get; set; }
  public GameMode GameMode { get; set; }
}
