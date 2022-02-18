using System.Collections.Generic;

namespace Langueedu.Sdk.Playlist.Response
{

  public class PlaylistViewModel
  {
    public int PlaylistId { get; set; }

    public string PlaylistName { get; set; }

    public List<TrackViewModel> Tracks { get; set; }
  }
}
