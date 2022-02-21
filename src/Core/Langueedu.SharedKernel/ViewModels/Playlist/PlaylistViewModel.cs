namespace Langueedu.SharedKernel.ViewModels;

public class PlaylistViewModel
{
  public string Name { get; set; }

  public IEnumerable<TrackViewModel> Tracks { get; set; }
}
