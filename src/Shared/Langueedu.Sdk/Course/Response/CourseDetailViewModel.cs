using System.Collections.Generic;

namespace Langueedu.Sdk.Course.Response
{
  public class CourseDetailViewModel
  {
    public int CourseId { get; set; }
    public List<LyricsViewModel> Lyrics { get; set; }
    public string YoutubeVideoId { get; set; }
  }
}

