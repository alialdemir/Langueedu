using System.Collections.Generic;

namespace Langueedu.Sdk.Playlist.Response
{

  public class TrackViewModel
  {
    public int TrackId { get; set; }
    public string AlbumnName { get; set; }

    public string YoutubeId { get; set; }

    public string AlbumSlug { get; set; }

    public string TrackSlug { get; set; }

    public string SongTitle { get; set; }

    public string TrackImage { get; set; }

    public int MainArtistId { get; set; }

    public IEnumerable<ArtistViewModel> Artists { get; set; }
  }
}
