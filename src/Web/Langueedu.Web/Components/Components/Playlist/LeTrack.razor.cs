using Langueedu.Sdk.Playlist.Response;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Components;

public partial class LeTrack : ComponentBase
{
  [Parameter]
  public TrackViewModel Track { get; set; }

  protected string Url
  {
    get
    {
      string mainArtistSlug = Track.Artists.FirstOrDefault().Slug;

      return $"/{mainArtistSlug}/{Track.Slug}/{Track.Id}";
    }
  }
}
