using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Components
{
    public partial class LeCard : UIComponentBase
    {
        protected string Classname =>
        new ClassnameBuilder("card")
            .AddClass(Class)
            .Build();

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}