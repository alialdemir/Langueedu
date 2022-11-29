using System.Windows.Input;
using Blazored.Modal;
using Langueedu.Sdk.Track;
using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Components.ViewModels;

public class TrackDetailViewModel : ViewModelBase
{
  private readonly ITrackService _trackService;
  private ICommand _showGameModeCommand;
  private ICommand _toggleFollowCommand;

  public TrackDetailViewModel(
      ITrackService trackService,
      IServiceProvider serviceProvider) : base(serviceProvider)
  {
    _trackService = trackService;
  }
  [Parameter]
  public short TrackId { get; set; }

  private Sdk.Playlist.Response.TrackDetailViewModel _trackDetail = new();

  public Sdk.Playlist.Response.TrackDetailViewModel TrackDetail
  {
    get => _trackDetail;
    set => SetField(ref _trackDetail, value);
  }

  public override async Task OnInitializedAsync()
  {
    var trackDetail = await _trackService.GetTrackDetailAsync(TrackId);
    if (trackDetail is not null && trackDetail.Value is not null)
      TrackDetail = trackDetail.Value;
  }

  private void ShowGameModeCommandExecute()
  {
    if (IsBusy)
      return;

    IsBusy = true;

    var parameters = new ModalParameters();
    parameters.Add(nameof(TrackId), TrackId);

    ShowModal<LeCourseLevel>(string.Empty, parameters, new ModalOptions()
    {
      HideCloseButton = false,
      HideHeader = true,
      DisableBackgroundCancel = true
    });

    IsBusy = false;
  }

  private async Task ToggleFollowCommandExecute()
  {
    if (IsBusy)
      return;

    IsBusy = true;

    TrackDetail.IsFollowed = !TrackDetail.IsFollowed;
    StateHasChanged();

    Sdk.Result<bool> result = null;

    if (TrackDetail.IsFollowed)
      result = await _trackService.FollowTrackAsync(TrackId);
    else
      result = await _trackService.UnFollowTrackAsync(TrackId);

    if (result == null || !result.Value)
    {
      TrackDetail.IsFollowed = !TrackDetail.IsFollowed;

      StateHasChanged();
    }

    IsBusy = false;
  }


  public ICommand ToggleFollowCommand => _toggleFollowCommand ??= new CommandAsync(ToggleFollowCommandExecute);

  public ICommand ShowGameModeCommand => _showGameModeCommand ??= new Command(ShowGameModeCommandExecute);
}
