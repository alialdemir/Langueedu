using Langueedu.Web.Shared.Extensions;
using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Components
{
    public partial class LeButton : LeButtonBase
    {
        protected string Classname =>
        new ClassnameBuilder("btn")
            .AddClass("btn-block", FullWidth)
            .AddClass($"btn-{Color.ToDescriptionString()}")
            .AddClass($"btn-{Size.ToDescriptionString()}")
            .AddClass(Class)
            .Build();

        [Parameter]
        public bool FullWidth { get; set; }

        [Parameter]
        public Color Color { get; set; } = Color.Primary;

        [Parameter]
        public Size Size { get; set; } = Size.Small;

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}

