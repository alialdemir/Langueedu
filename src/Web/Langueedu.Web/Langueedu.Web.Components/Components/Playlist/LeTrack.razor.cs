using Langueedu.Sdk.Playlist.Response;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Components;

public partial class LeTrack : ComponentBase
{
  [Parameter]
  public TrackViewModel Track { get; set; }

  protected string Url
  {
    get
    {
      string mainArtistSlug = Track.Artists.FirstOrDefault(x => x.Id == Track.MainArtistId).Slug;

      return $"/{mainArtistSlug}/{Track.AlbumSlug}/{Track.TrackSlug}/{Track.TrackId}";
    }
  }
}
