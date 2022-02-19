using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

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
    public int MainArtistId { get; set; }

    [JsonIgnore]
    public string MailTo
    {
      get
      {

        return $"mailto:?subject={ShareText}&body={TrackUrl}";
      }
    }

    private string myVar;
    public string ShareText
    {
      get
      {
        if (Artists == null)
          return string.Empty;

        string artistNames = string.Join(" - ", Artists?.Select(x => x.Name));

        return $"Play \"{artistNames} - {SongTitle}\" on Langueedu!";
      }
    }

    [JsonIgnore]
    public string FacebookShare
    {
      get { return $"https://www.facebook.com/sharer/sharer.php?u={TrackUrl}"; }
    }

    [JsonIgnore]
    public string TwitterShare
    {
      get
      {
        return $"https://twitter.com/share?url={TrackUrl}&text={ShareText}";
      }
    }
    [JsonIgnore]

    public string TrackUrl
    {
      get
      {
        string mainArtistSlug = Artists?.FirstOrDefault(x => x.Id == MainArtistId)?.Slug;

        string path = $"https://langueedu.com/{mainArtistSlug}/{AlbumSlug}/{Slug}/{Id}";

        return path;

      }
    }

  }
}

