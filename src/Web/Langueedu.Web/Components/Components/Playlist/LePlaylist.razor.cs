using Langueedu.Components.Interfaces;
using Langueedu.Sdk.Playlist.Response;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Components;

public partial class LePlaylist : ComponentBase
{
  [Parameter]
  public IEnumerable<PlaylistViewModel> Playlists { get; set; }

  [Inject]
  public ITinySlider TinySlider { get; set; }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      await TinySlider.StartLearnSlider(".learn-slider1");
    }

    await base.OnAfterRenderAsync(firstRender);
  }
}
