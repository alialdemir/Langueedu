using System.Windows.Input;
using Langueedu.Web.Components.Interfaces;
using Langueedu.Web.Components.Models;
using Langueedu.Web.Components.Services;
using Langueedu.Web.Shared.Utilities;

namespace Langueedu.Web.Components.ViewModels;

public class DuelViewModel : ViewModelBase
{
  private readonly IYoutubePlayer _youtubePlayer;
  private ICommand _stylishCommand;

  public DuelViewModel(IYoutubePlayer _youtubePlayer)
  {
    this._youtubePlayer = _youtubePlayer;
  }

  public short EntryFee { get; set; }

  public GameMode GameMode { get; set; }

  private bool IsTimeActive { get; set; } = true;
  private double _currentDuration;
  public double CurrentDuration
  {
    get => _currentDuration;
    set => SetField(ref _currentDuration, value);
  }

  private LyricsModel _nextLyrics = new();
  public LyricsModel NextLyrics
  {
    get => _nextLyrics;
    set => SetField(ref _nextLyrics, value);
  }

  private LyricsModel _currentLyrics = new();
  public LyricsModel CurrentLyrics
  {
    get => _currentLyrics;
    set => SetField(ref _currentLyrics, value);
  }

  public List<LyricsModel> Lyrics
  {
    get => lyrics.Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
  }

  public override async Task OnInitializedAsync()
  {
    await _youtubePlayer.InitYoutube("FxBnts2bZX8");
    YoutubePlayer.OnPlayerReady = new Command(OnPlayerReady);
  }

  private async Task OnPlayerReady()
  {
    await _youtubePlayer.PlayVideo();

    Services.Timer.Start(TimeSpan.FromSeconds(1), SetCurrentDuration);
  }

  private async Task<bool> SetCurrentDuration()
  {
    if (!IsTimeActive)
      return false;

    double currentDuration = (await _youtubePlayer.CurrentTime()) * 1000;// milliseconds
    LyricsModel nextDuration = Lyrics.Where(x => x.Duration <= currentDuration).LastOrDefault();
    if (nextDuration == null)
      return true;

    nextDuration.Stylish = nextDuration.Text.Split(" ").Take(4).Select(x => new StylishModel
    {
      Answer = x,

    }).ToList();

    CurrentDuration = currentDuration;
    CurrentLyrics = NextLyrics;
    NextLyrics = nextDuration;

    await _youtubePlayer.ScrollIntoView($"id{nextDuration.Duration}");

    return IsTimeActive;
  }

  private async Task StylishCommandExecute(StylishModel stylish)
  {
    if (IsBusy)
      return;

    IsBusy = true;

    IsTimeActive = false;

    await _youtubePlayer.PauseVideo();

    ChangeStylishColor(Color.Danger, stylish.Answer);
    StateHasChanged();

    IsBusy = false;
  }

  private void ChangeStylishColor(Color color, string answer)
  {
    CurrentLyrics
    .Stylish
    .ForEach(stylish =>
               {
                 // Set default value
                 stylish.Color = Color.Transparent;
                 stylish.IsLeftArrowVisible = false;
                 stylish.IsRightArrowVisible = false;

                 if (stylish.Answer == answer)
                 {
                   stylish.Color = color;
                 }
               });
  }

  public ICommand StylishCommand { get => _stylishCommand ??= new CommandAsync<StylishModel>(StylishCommandExecute); }

  List<LyricsModel> lyrics = new List<LyricsModel> {
new LyricsModel {
Duration= 870,
Text= "Bir deli dağ gibiydim, yıkılmazdım",
},
new LyricsModel {
Duration= 5970,
Text= ""
},
new LyricsModel {
Duration= 6770,
Text= "Savrulmazdım, rüzgarlardan"
},
new LyricsModel {
Duration= 13820,
Text= ""
},
new LyricsModel {
Duration= 15580,
Text= "Yedi düvel gelse sarsılmazdım"
},
new LyricsModel {
Duration= 20810,
Text= ""
},
new LyricsModel {
Duration= 22130,
Text= "Dağılmazdım, olanlardan…"
},
new LyricsModel {
Duration= 28860,
Text= ""
},
new LyricsModel {
Duration= 30760,
Text= "Bilirim kendimi, alimi zalimi"
},
new LyricsModel {
Duration= 34630,
Text= "Görmüş geçmişi…"
},
new LyricsModel {
Duration= 37620,
Text= ""
},
new LyricsModel {
Duration= 38290,
Text= "Ne zaman nefesime işledi rüzgarın"
},
new LyricsModel {
Duration= 42100,
Text= "Anladım, geçmişi…"
},
new LyricsModel {
Duration= 46180,
Text= ""
},
new LyricsModel {
Duration= 46530,
Text= "Alırsan aşkını, benden geriye ne kalır?"
},
new LyricsModel {
Duration= 52930,
Text= ""
},
new LyricsModel {
Duration= 53730,
Text= "Bilmem karanlığa sensiz nasıl alışılır?"
},
new LyricsModel {
Duration= 60770,
Text= ""
},
new LyricsModel {
Duration= 61490,
Text= "Alırsan aşkını, benden geriye ne kalır?"
},
new LyricsModel {
Duration= 67980,
Text= ""
},
new LyricsModel {
Duration= 68680,
Text= "Yaşarım belki, gözlerim kör kulağım sağır…"
},
new LyricsModel {
Duration= 76210,
Text= ""
},
new LyricsModel {
Duration= 92680,
Text= "Bir deli dağ gibiydim, yıkılmazdım"
},
new LyricsModel {
Duration= 97770,
Text= ""
},
new LyricsModel {
Duration= 98730,
Text= "Savrulmazdım, rüzgarlardan"
},
new LyricsModel {
Duration= 105340,
Text= ""
},
new LyricsModel {
Duration= 107450,
Text= "Yedi düvel gelse sarsılmazdım"
},
new LyricsModel {
Duration= 112690,
Text= ""
},
new LyricsModel {
Duration= 114000,
Text= "Dağılmazdım, olanlardan…"
},
new LyricsModel {
Duration= 120460,
Text= ""
},
new LyricsModel {
Duration= 122660,
Text= "Bilirim kendimi, alimi zalimi"
},
new LyricsModel {
Duration= 126540,
Text= "Görmüş geçmişi…"
},
new LyricsModel {
Duration= 129470,
Text= ""
},
new LyricsModel {
Duration= 130160,
Text= "Ne zaman nefesime işledi rüzgarın"
},
new LyricsModel {
Duration= 133950,
Text= "Anladım, geçmişi…"
},
new LyricsModel {
Duration= 138050,
Text= ""
},
new LyricsModel {
Duration= 138430,
Text= "Alırsan aşkını, benden geriye ne kalır?"
},
new LyricsModel {
Duration= 144740,
Text= ""
},
new LyricsModel {
Duration= 145660,
Text= "Bilmem karanlığa sensiz nasıl alışılır?"
},
new LyricsModel {
Duration= 152320,
Text= ""
},
new LyricsModel {
Duration= 153240,
Text= "Alırsan aşkını, benden geriye ne kalır?"
},
new LyricsModel {
Duration= 159800,
Text= ""
},
new LyricsModel {
Duration= 160520,
Text= "Yaşarım belki, gözlerim kör kulağım sağır…"
},
new LyricsModel {
Duration= 168040,
Text= ""
},
new LyricsModel {
Duration= 168370,
Text= "Alırsan aşkını, benden geriye ne kalır?"
},
new LyricsModel {
Duration= 174590,
Text= ""
},
new LyricsModel {
Duration= 175640,
Text= "Bilmem karanlığa sensiz nasıl alışılır?"
},
new LyricsModel {
Duration= 182660,
Text= ""
},
new LyricsModel {
Duration= 183330,
Text= "Alırsan aşkını, benden geriye ne kalır?"
},
new LyricsModel {
Duration= 189910,
Text= ""
},
new LyricsModel {
Duration= 190580,
Text= "Yaşarım belki, gözlerim kör kulağım sağır…"
},
new LyricsModel {
Duration= 198060,
Text= ""
}
};
}
