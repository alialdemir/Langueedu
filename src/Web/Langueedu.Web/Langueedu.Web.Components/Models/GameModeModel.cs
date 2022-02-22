
namespace Langueedu.Web.Components.Models;
public class GameModeModel
{
  private static readonly string rootPicturePath = "_content/Langueedu.Web.Pages/assets/images/";

  public short EntryFee { get; set; }
  public short CourseFee { get; set; }
  public string Image { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public byte ProgressBar { get; set; }
  public GameMode GameMode { get; set; }

  public static List<GameModeModel> GameModes()
  {
    return new List<GameModeModel>
{
                new GameModeModel
                {
                    Title = "Beginner",
                    Description = "Complete the 10% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon1.png",
                    GameMode = GameMode.Beginner,
                    CourseFee = 80,
                    EntryFee = 40,
                    ProgressBar = 25,
                },
                new GameModeModel
                {
                    Title = "Intermediate",
                    Description = "Complete the 25% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon2.png",
                    GameMode = GameMode.Intermediate,
                    CourseFee= 600,
                    EntryFee = 300,
                    ProgressBar = 50,
                },
                new GameModeModel
                {
                  Title = "Advanced",
                  Description = "Complete the 50% of the lyrics",
                  Image = $"{rootPicturePath}/prizeicon3.png",
                  GameMode = GameMode.Advanced,
                  CourseFee = 2000,
                  EntryFee = 1000,
                  ProgressBar = 75,
                },
                new GameModeModel
                {
                  Title = "Expert",
                  Description = "Complete the 100% of the lyrics",
                  Image = $"{rootPicturePath}/prizeicon4.png",
                  GameMode = GameMode.Expert,
                  CourseFee = 6000,
                  EntryFee = 3000,
                  ProgressBar = 100,
                },
    };
  }
}
