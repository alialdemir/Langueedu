using Ardalis.Specification;
using Langueedu.Core.Entities.PlaylistAggregate;

namespace Langueedu.Core.Specifications.ArtistGenreSpec;

public class GetArtistGenreSpec : Specification<ArtistGenre, ArtistGenre>
{
  public GetArtistGenreSpec(int artistId, string name)
  {
    if (!string.IsNullOrEmpty(name))
      Query
        .Where(x => x.Artist.Id == artistId && x.Genre.Description == name);

    Query
      .Select(x => x);
  }
}

