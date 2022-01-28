using Langueedu.Web.Components.Interfaces;
using Langueedu.Web.Components.Services;

namespace Langueedu.Web.Components.ViewModels
{
    public class DuelViewModel : ViewModelBase
    {
        private readonly IYoutubePlayer _youtubePlayer;

        public DuelViewModel(IYoutubePlayer _youtubePlayer)
        {
            this._youtubePlayer = _youtubePlayer;
        }

        public short EntryFee { get; set; }

        public GameMode GameMode { get; set; }

        private double _duration;

        public double Duration
        {
            get => _duration;
            set => Set(ref _duration, value);
        }

        public List<dynamic> Lyrics
        {
            get => lyrics;
        }

        public override async Task OnInitializedAsync()
        {
            await _youtubePlayer.InitYoutube("FxBnts2bZX8");

            YoutubePlayer.OnPlayerReady = new Shared.Utilities.Command(async () =>
            {
                await _youtubePlayer.PlayVideo();

                Services.Timer.Start(TimeSpan.FromSeconds(1), async () =>
                {
                    Duration = await _youtubePlayer.CurrentTime();

                    System.Console.WriteLine("duration: {0}", Duration);
                    return true;
                });
            });
        }


        List<dynamic> lyrics = new List<dynamic> {
new {
time= 870,
text= "Bir deli dağ gibiydim, yıkılmazdım"
},
new {
time= 5970,
text= ""
},
new {
time= 6770,
text= "Savrulmazdım, rüzgarlardan"
},
new {
time= 13820,
text= ""
},
new {
time= 15580,
text= "Yedi düvel gelse sarsılmazdım"
},
new {
time= 20810,
text= ""
},
new {
time= 22130,
text= "Dağılmazdım, olanlardan…"
},
new {
time= 28860,
text= ""
},
new {
time= 30760,
text= "Bilirim kendimi, alimi zalimi"
},
new {
time= 34630,
text= "Görmüş geçmişi…"
},
new {
time= 37620,
text= ""
},
new {
time= 38290,
text= "Ne zaman nefesime işledi rüzgarın"
},
new {
time= 42100,
text= "Anladım, geçmişi…"
},
new {
time= 46180,
text= ""
},
new {
time= 46530,
text= "Alırsan aşkını, benden geriye ne kalır?"
},
new {
time= 52930,
text= ""
},
new {
time= 53730,
text= "Bilmem karanlığa sensiz nasıl alışılır?"
},
new {
time= 60770,
text= ""
},
new {
time= 61490,
text= "Alırsan aşkını, benden geriye ne kalır?"
},
new {
time= 67980,
text= ""
},
new {
time= 68680,
text= "Yaşarım belki, gözlerim kör kulağım sağır…"
},
new {
time= 76210,
text= ""
},
new {
time= 92680,
text= "Bir deli dağ gibiydim, yıkılmazdım"
},
new {
time= 97770,
text= ""
},
new {
time= 98730,
text= "Savrulmazdım, rüzgarlardan"
},
new {
time= 105340,
text= ""
},
new {
time= 107450,
text= "Yedi düvel gelse sarsılmazdım"
},
new {
time= 112690,
text= ""
},
new {
time= 114000,
text= "Dağılmazdım, olanlardan…"
},
new {
time= 120460,
text= ""
},
new {
time= 122660,
text= "Bilirim kendimi, alimi zalimi"
},
new {
time= 126540,
text= "Görmüş geçmişi…"
},
new {
time= 129470,
text= ""
},
new {
time= 130160,
text= "Ne zaman nefesime işledi rüzgarın"
},
new {
time= 133950,
text= "Anladım, geçmişi…"
},
new {
time= 138050,
text= ""
},
new {
time= 138430,
text= "Alırsan aşkını, benden geriye ne kalır?"
},
new {
time= 144740,
text= ""
},
new {
time= 145660,
text= "Bilmem karanlığa sensiz nasıl alışılır?"
},
new {
time= 152320,
text= ""
},
new {
time= 153240,
text= "Alırsan aşkını, benden geriye ne kalır?"
},
new {
time= 159800,
text= ""
},
new {
time= 160520,
text= "Yaşarım belki, gözlerim kör kulağım sağır…"
},
new {
time= 168040,
text= ""
},
new {
time= 168370,
text= "Alırsan aşkını, benden geriye ne kalır?"
},
new {
time= 174590,
text= ""
},
new {
time= 175640,
text= "Bilmem karanlığa sensiz nasıl alışılır?"
},
new {
time= 182660,
text= ""
},
new {
time= 183330,
text= "Alırsan aşkını, benden geriye ne kalır?"
},
new {
time= 189910,
text= ""
},
new {
time= 190580,
text= "Yaşarım belki, gözlerim kör kulağım sağır…"
},
new {
time= 198060,
text= ""
}
};
    }
}
