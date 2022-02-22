using System.Windows.Input;
using Blazored.Modal;
using Langueedu.Web.Components.Interfaces;
using Langueedu.Web.Components.Models;
using Langueedu.Web.Shared.Utilities;

namespace Langueedu.Web.Components.ViewModels;

public class GameModeViewModel : ViewModelBase
{
  private readonly ITinySlider _tinySlider;

  private IEnumerable<GameModeModel> _gameModes;
  private ICommand _startGameCommand;

  public GameModeViewModel(IServiceProvider serviceProvider,
                           ITinySlider tinySlider) : base(serviceProvider)
  {
    _tinySlider = tinySlider;
  }

  public IEnumerable<GameModeModel> GameModes
  {
    get => _gameModes;
    set => SetField(ref _gameModes, value);
  }

  public override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
      await _tinySlider.StartGameModeSlider(".leModaL-course-level-slider");
  }

  public override void OnInitialized()
  {
    GameModes = GameModeModel.GameModes();
  }

  private async Task StartGameCommandExecute(GameModeModel gameMode)
  {
    if (IsBusy)
      return;

    IsBusy = true;

    await HideModal();

    var modalParams = new ModalParameters();
    modalParams.Add("EntryFee", gameMode.EntryFee);
    modalParams.Add("GameMode", gameMode.GameMode);

    ShowModal<LeDuel>(string.Empty, modalParams, new ModalOptions()
    {
      HideCloseButton = false,
      HideHeader = true,
      DisableBackgroundCancel = true
    });

    IsBusy = false;
  }

  public ICommand StartGameCommand { get => _startGameCommand ??= new CommandAsync<GameModeModel>(StartGameCommandExecute); }

}
