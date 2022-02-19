using System.Windows.Input;
using Blazored.Modal;
using Langueedu.Web.Components.Interfaces;
using Langueedu.Web.Components.Models;
using Langueedu.Web.Shared.Utilities;

namespace Langueedu.Web.Components.ViewModels;

public class GameModeViewModel : ViewModelBase
{
  private readonly string rootPicturePath = "_content/Langueedu.Web.Pages/assets/images/";
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
    GameModes = new[]
{
                new GameModeModel
                {
                    Title = "Beginner",
                    Description = "Complete the 10% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon1.png",
                    GameMode = GameMode.Beginner,
                    CourseFee = 80,
                    EntryFee = 40,
                    ProgressBar = 25,
                },
                new GameModeModel
                {
                    Title = "Intermediate",
                    Description = "Complete the 25% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon2.png",
                    GameMode = GameMode.Intermediate,
                    CourseFee= 600,
                    EntryFee = 300,
                    ProgressBar = 50,
                },
                new GameModeModel
                {
                    Title = "Advanced",
                    Description = "Complete the 50% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon3.png",
                    GameMode = GameMode.Advanced,
                    CourseFee= 2000,
                    EntryFee = 1000,
                    ProgressBar = 75,
                },
                new GameModeModel
                {
                    Title = "Expert",
                    Description = "Complete the 100% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon4.png",
                    GameMode = GameMode.Expert,
                    CourseFee= 6000,
                    EntryFee = 3000,
                    ProgressBar = 100,
                },
    };
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
