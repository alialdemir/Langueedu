using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Components;

public partial class LeCard : UIComponentBase
{
  protected string Classname =>
  new ClassnameBuilder("card")
      .AddClass("mb-0")
      .AddClass(Class)
      .Build();

  [Parameter]
  public RenderFragment ChildContent { get; set; }
}
