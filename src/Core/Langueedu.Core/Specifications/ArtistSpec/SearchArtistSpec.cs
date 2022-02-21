using Ardalis.Specification;
using Langueedu.Core.Entities.PlaylistAggregate;

namespace Langueedu.Core.Specifications.ArtistSpec;

public class SearchArtistSpec : Specification<Artist, Artist>
{
  public SearchArtistSpec(string name)
  {
    if (!string.IsNullOrEmpty(name))
      Query
        .Search(x => x.Slug, name.GenerateSlug());

    Query
      .Include(x => x.Images);

    Query
      .Include(x => x.ArtistGenres)
      .ThenInclude(x => x.Genre);

    Query
      .Select(x => x);
  }
}

