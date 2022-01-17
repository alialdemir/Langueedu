using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Components
{
    public partial class LeCardHeader : UIComponentBase
    {
        protected string Classname =>
        new ClassnameBuilder("card-header")
            .AddClass("no-border")
            .AddClass("pb-0")
            .AddClass(Class)
            .Build();

        [Parameter]
        public string Title { get; set; }
    }
}

