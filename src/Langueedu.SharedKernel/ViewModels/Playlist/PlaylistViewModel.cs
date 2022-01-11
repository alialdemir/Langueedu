namespace Langueedu.SharedKernel.ViewModels;

public class PlaylistViewModel
{
  public int PlaylistId { get; set; }

  public string PlaylistName { get; set; }

  public List<TrackViewModel> Tracks { get; set; }
}
