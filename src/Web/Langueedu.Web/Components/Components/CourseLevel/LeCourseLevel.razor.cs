using Blazored.Modal;
using Langueedu.Components.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Components;

public partial class LeCourseLevel : ComponentBase<GameModeViewModel>
{
  [Parameter]
  public short TrackId { get; set; }

  [CascadingParameter]
  BlazoredModalInstance ModalInstance { get; set; }

  protected override void OnInitialized()
  {
    base.OnInitialized();

    BindingContext.ModalInstance = ModalInstance;
  }
}
