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
      .Where(x => !string.IsNullOrEmpty(x.Answer));

    Query
      .Select(x => new LyricsViewModel
      {
        Answer = x.Answer,
        Id = x.Id,
        Text = x.Text,
        Duration = x.Duration
      })
      .OrderBy(x => x.Duration);
  }
}
