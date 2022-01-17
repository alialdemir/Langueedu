using Langueedu.Web.Components.Interfaces;
using Microsoft.JSInterop;

namespace Langueedu.Web.Components.Services
{
    public class TinySlider : ITinySlider
    {
        private readonly IJSRuntime _jSRuntime;

        public TinySlider(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task StartLearnSlider(string container)
        {
            await _jSRuntime.InvokeVoidAsync("tinySlider", new
            {
                container = container,
                items = 5,
                slideBy = 2.6,
                loop = false,
                controls = false,
                nav = false,
                swipeAngle = true,
                speed = 400,
                autoplay = false,
                mouseDrag = true,
                lazyload = true,
                gutter = 15,
                fixedWidth = 230,
            });
        }
    }
}

