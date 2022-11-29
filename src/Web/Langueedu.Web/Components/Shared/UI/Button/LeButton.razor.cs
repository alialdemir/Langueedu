using Langueedu.Web.Shared.Extensions;
using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Components;

public partial class LeButton : LeButtonBase
{
  protected string Classname =>
  new ClassnameBuilder("btn")
      .AddClass("btn-block", FullWidth)
      .AddClass("btn-shadow", IsShadow)
      .AddClass($"btn-{Color.ToDescriptionString()}")
      .AddClass($"btn-rounded-{Rounded.ToDescriptionString()}", Rounded != Rounded.None)
      .AddClass($"btn-{Size.ToDescriptionString()}")
      .AddClass(Class)
      .Build();

  [Parameter]
  public bool FullWidth { get; set; }

  [Parameter]
  public bool IsShadow { get; set; } = true;

  [Parameter]
  public Color Color { get; set; } = Color.Primary;

  [Parameter]
  public Rounded Rounded { get; set; } = Rounded.None;

  [Parameter]
  public Size Size { get; set; } = Size.Small;

  [Parameter]
  public RenderFragment ChildContent { get; set; }
}

