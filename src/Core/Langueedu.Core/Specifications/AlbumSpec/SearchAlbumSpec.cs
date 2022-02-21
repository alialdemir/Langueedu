using Ardalis.Specification;
using Langueedu.Core.Entities.PlaylistAggregate;

namespace Langueedu.Core.Specifications.AlbumSpec;

public class SearchAlbumSpec : Specification<Album, Album>
{
  public SearchAlbumSpec(string name)
  {
    if (!string.IsNullOrEmpty(name))
      Query
        .Search(x => x.Slug, name.GenerateSlug());

    Query
      .Include(x => x.Images);

    Query
      .Include(x => x.AlbumGenres)
      .ThenInclude(x => x.Genre);

    Query
      .Select(x => x);
  }
}

