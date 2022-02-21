using Ardalis.Specification;

namespace Langueedu.Core.Specifications.TrackSpec;

public class SearchTrackSpec : Specification<Entities.PlaylistAggregate.Track, Entities.PlaylistAggregate.Track>
{
  public SearchTrackSpec(string name)
  {
    if (!string.IsNullOrEmpty(name))
      Query
        .Search(x => x.Slug, name.GenerateSlug());

    Query
      .Select(x => x);
  }
}

