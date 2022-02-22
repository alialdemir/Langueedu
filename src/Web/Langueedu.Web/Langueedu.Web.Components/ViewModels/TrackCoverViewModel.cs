using System.Windows.Input;
using Blazored.Modal;
using Langueedu.Sdk.Playlist.Response;
using Langueedu.Sdk.Track;
using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Components.ViewModels;

public class TrackCoverViewModel : ViewModelBase
{
  private readonly ITrackService _trackService;
  private ICommand _showGameModeCommand;
  private ICommand _toggleFollowCommand;

  public TrackCoverViewModel(
      ITrackService trackService,
      IServiceProvider serviceProvider) : base(serviceProvider)
  {
    _trackService = trackService;
  }

  [Parameter]
  public int TrackId { get; set; }

  private TrackDetailViewModel _trackDetail = new();

  public TrackDetailViewModel TrackDetail
  {
    get => _trackDetail;
    set => SetField(ref _trackDetail, value);
  }

  public override async Task OnInitializedAsync()
  {
    TrackDetail = await _trackService.GetTrackDetailAsync(TrackId);
  }

  private void ShowGameModeCommandExecute()
  {
    if (IsBusy)
      return;

    IsBusy = true;

    var parameters = new ModalParameters();
    parameters.Add(nameof(TrackId), TrackId);

    ShowModal<LeCourseLevel>(string.Empty,parameters, new ModalOptions()
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

    if (result == null  || !result.Value)
    {
      TrackDetail.IsFollowed = !TrackDetail.IsFollowed;

      StateHasChanged();
    }


    IsBusy = false;
  }


  public ICommand ToggleFollowCommand => _toggleFollowCommand ??= new CommandAsync(ToggleFollowCommandExecute);

  public ICommand ShowGameModeCommand => _showGameModeCommand ??= new Command(ShowGameModeCommandExecute);
}
