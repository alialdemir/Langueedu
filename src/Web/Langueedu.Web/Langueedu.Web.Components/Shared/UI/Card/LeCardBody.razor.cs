using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Components;

public partial class LeCardBody : UIComponentBase
{
  protected string Classname =>
  new ClassnameBuilder("card-body")
      .AddClass("pb-4")
      .AddClass("pt-4")
      .AddClass(Class)
      .Build();

  [Parameter]
  public RenderFragment ChildContent { get; set; }
}
