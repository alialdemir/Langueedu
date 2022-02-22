using Langueedu.Sdk.Playlist.Response;

namespace Langueedu.Web.Components.Extensions;

public static class ArtistViewModelExtension
{
  public static string GetArtistNames(this IEnumerable<ArtistViewModel> artists)
  {
    if (artists == null || !artists.Any())
      return string.Empty;

    string artistNames = string.Join(" - ", artists.Select(x => x.Name));
    return artistNames;
  }
}
