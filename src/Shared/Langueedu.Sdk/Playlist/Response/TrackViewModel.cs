using System.Collections.Generic;

namespace Langueedu.Sdk.Playlist.Response
{
  public class TrackViewModel
  {

    public int Id { get; set; }

    public string Slug { get; set; }

    public string Name { get; set; }


    public IEnumerable<ArtistViewModel> Artists { get; set; }
    public IEnumerable<ImageViewModel> Images { get; set; }
    public AlbumViewModel Album { get; set; }
  }
}