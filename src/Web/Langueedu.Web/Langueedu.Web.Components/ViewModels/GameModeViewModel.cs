using Langueedu.Web.Components.Interfaces;
using Langueedu.Web.Components.Models;

namespace Langueedu.Web.Components.ViewModels
{
    public class GameModeViewModel : ViewModelBase
    {
        private readonly string rootPicturePath = "_content/Langueedu.Web.Pages/assets/images/";
        private readonly ITinySlider _tinySlider;

        public GameModeViewModel(IServiceProvider serviceProvider,
                                 ITinySlider tinySlider) : base(serviceProvider)
        {
            _tinySlider = tinySlider;
        }

        private IEnumerable<GameModeModel> _gameModes;
        public IEnumerable<GameModeModel> GameModes
        {
            get => _gameModes;
            set => Set(ref _gameModes, value);
        }

        public override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _tinySlider.StartGameModeSlider(".leModaL-game-mode-slider");
            }
        }

        public override void OnInitialized()
        {
            base.OnInitialized();

            GameModes = new[]
                {
                new GameModeModel
                {
                    Title = "Beginner",
                    Description = "Complete the 10% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon1.png",
                    Prize = 80,
                    EntryFee = 40,
                    ProgressBar = 25,
                },
                new GameModeModel
                {
                    Title = "Intermediate",
                    Description = "Complete the 25% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon2.png",
                    Prize = 600,
                    EntryFee = 300,
                    ProgressBar = 50,
                },
                new GameModeModel
                {
                    Title = "Advanced",
                    Description = "Complete the 50% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon3.png",
                    Prize = 2000,
                    EntryFee = 1000,
                    ProgressBar = 75,
                },
                new GameModeModel
                {
                    Title = "Expert",
                    Description = "Complete the 100% of the lyrics",
                    Image = $"{rootPicturePath}/prizeicon4.png",
                    Prize = 6000,
                    EntryFee = 3000,
                    ProgressBar = 100,
                },
    };
        }
    }
}
