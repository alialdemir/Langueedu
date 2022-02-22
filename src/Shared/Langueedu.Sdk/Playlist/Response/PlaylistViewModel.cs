using System.Collections.Generic;

namespace Langueedu.Sdk.Playlist.Response
{
  public class PlaylistViewModel
  {
    public string Name { get; set; }

    public IEnumerable<TrackViewModel> Tracks { get; set; }
  }
}