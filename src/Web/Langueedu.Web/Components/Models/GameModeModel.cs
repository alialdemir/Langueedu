
using Langueedu.Sdk.Enums;

namespace Langueedu.Components.Models;
public class GameModeModel
{
  private static readonly string rootPicturePath = "_content/Langueedu.Pages/assets/images/";

  public decimal EntryFee { get; set; }
  public decimal CourseFee { get; set; }
  public string Image { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public byte ProgressBar { get; set; }
  public CourseLevel CourseLevel { get; set; }

  public static List<GameModeModel> GameModes()
  {
    return new List<GameModeModel>
{
                new GameModeModel
                {
                    Title = "Beginner",
                    Description = "Complete the 10% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon1.png",
                    CourseLevel = CourseLevel.Beginner,
                    CourseFee = 80,
                    EntryFee = 40,
                    ProgressBar = 25,
                },
                new GameModeModel
                {
                    Title = "Intermediate",
                    Description = "Complete the 25% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon2.png",
                    CourseLevel = CourseLevel.Intermediate,
                    CourseFee= 600,
                    EntryFee = 300,
                    ProgressBar = 50,
                },
                new GameModeModel
                {
                  Title = "Advanced",
                  Description = "Complete the 50% of the lyrics",
                  Image = $"{rootPicturePath}/prizeicon3.png",
                  CourseLevel = CourseLevel.Advanced,
                  CourseFee = 2000,
                  EntryFee = 1000,
                  ProgressBar = 75,
                },
                new GameModeModel
                {
                  Title = "Expert",
                  Description = "Complete the 100% of the lyrics",
                  Image = $"{rootPicturePath}/prizeicon4.png",
                  CourseLevel = CourseLevel.Expert,
                  CourseFee = 6000,
                  EntryFee = 3000,
                  ProgressBar = 100,
                },
    };
  }
}
