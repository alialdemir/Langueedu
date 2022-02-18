using System.Collections.Generic;

namespace Langueedu.Sdk.Playlist.Response
{
  public class TrackDetailViewModel
  {
    public TrackDetailViewModel()
    {
    }

    public int Id { get; set; }
    public string SongTitle { get; set; }
    public string PicturePath { get; set; }
    public string Slug { get; set; }
    public IEnumerable<ArtistViewModel> Artists { get; set; }
    public string AlbumSlug { get; set; }
    public string AlbumName { get; set; }
    public string YoutubeVideoId { get; set; }
    public bool IsFollowed { get; set; }
    public long FollowerCount { get; set; }
  }
}

