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
    TrackDetail = await _trackService.GetTrackDetail(TrackId);
  }

  private void ShowGameModeCommandExecute()
  {
    if (IsBusy)
      return;

    IsBusy = true;

    ShowModal<LeGameMode>(string.Empty, new ModalOptions()
    {
      HideCloseButton = false,
      HideHeader = true,
      DisableBackgroundCancel = true
    });

    IsBusy = false;
  }

  public ICommand ShowGameModeCommand { get => _showGameModeCommand ??= new Command(ShowGameModeCommandExecute); }
}
