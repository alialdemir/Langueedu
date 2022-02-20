namespace Langueedu.SharedKernel.ViewModels.Course;

public class CourseDetailViewModel
{
  public int CourseId { get; set; }
  public IEnumerable<LyricsViewModel> Lyrics { get; set; }
  public string YoutubeVideoId { get; set; }
}

