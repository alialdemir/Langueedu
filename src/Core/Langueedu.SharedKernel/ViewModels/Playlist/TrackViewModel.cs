namespace Langueedu.SharedKernel.ViewModels;

public class TrackViewModel
{
  public string AlbumnName { get; set; }

  public string YoutubeId { get; set; }

  public string AlbumSlug { get; set; }

  public string TrackSlug { get; set; }

  public string SongTitle { get; set; }

  public string TrackImage { get; set; }

  public int MainArtistId { get; set; }

  public List<ArtistViewModel> Artists { get; set; }
}