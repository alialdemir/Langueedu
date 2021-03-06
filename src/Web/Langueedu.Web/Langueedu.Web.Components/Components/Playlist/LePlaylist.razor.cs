using Langueedu.Sdk.Playlist.Response;
using Langueedu.Web.Components.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Components;

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
