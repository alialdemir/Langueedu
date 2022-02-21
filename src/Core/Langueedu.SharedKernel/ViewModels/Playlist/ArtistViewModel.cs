using Langueedu.SharedKernel.ViewModels.Playlist;

namespace Langueedu.SharedKernel.ViewModels;

public class ArtistViewModel
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Slug { get; set; }
  public IEnumerable<ImageViewModel> Images { get; set; }
}
