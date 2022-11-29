namespace Langueedu.SharedKernel.ViewModels.Playlist;

public class AlbumViewModel
{
  public AlbumViewModel()
  {
  }
  /// <summary>
  /// Album name
  /// </summary>
  public string Name { get; set; }
  /// <summary>
  /// Url slug
  /// </summary>
  public string Slug { get; set; }
  /// <summary>
  /// Pictures of the album
  /// </summary>
  /// <value></value>
  public IEnumerable<ImageViewModel> Images { get; set; }
}

