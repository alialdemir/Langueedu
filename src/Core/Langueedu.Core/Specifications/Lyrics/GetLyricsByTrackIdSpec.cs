using Ardalis.Specification;
using Langueedu.SharedKernel.ViewModels.Course;

namespace Langueedu.Core.Specifications.Lyrics;

public class GetLyricsByTrackIdSpec : Specification<Core.Entities.PlaylistAggregate.Lyrics, LyricsViewModel>
{
  public GetLyricsByTrackIdSpec(short trackId)
  {
    Query
      .Where(x => x.Track.Id == trackId);

    Query
      .Select(x => new LyricsViewModel
      {
        Answer = x.Answer,
        LyricsId = x.Id,
        Text = x.Text,
        Time = x.Time
      })
      .OrderBy(x => x.Time);
  }
}
