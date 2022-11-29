using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Components;

public partial class LeCardHeader : UIComponentBase
{
  protected string Classname =>
  new ClassnameBuilder("card-header")
      .AddClass("no-border")
      .AddClass(Class)
      .Build();

  [Parameter]
  public string Title { get; set; }

  [Parameter]
  public RenderFragment ChildContent { get; set; }
}

