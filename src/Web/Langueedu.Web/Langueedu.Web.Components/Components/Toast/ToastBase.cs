using Langueedu.Sdk;
using Langueedu.Sdk.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Components;

public class ToastBase : ComponentBase
{
  [Inject] IToastService ToastService { get; set; }

  protected string Heading { get; set; }
  protected string Message { get; set; }
  protected bool IsVisible { get; set; }
  protected string BackgroundCssClass { get; set; }

  protected override void OnInitialized()
  {
    ToastService.OnShow += ShowToast;
    ToastService.OnHide += HideToast;
  }

  private void ShowToast(string message, ToastLevel level)
  {
    BuildToastSettings(level, message);
    IsVisible = true;
    StateHasChanged();
  }

  public void HideToast()
  {
    IsVisible = false;
    StateHasChanged();
  }

  private void BuildToastSettings(ToastLevel level, string message)
  {
    switch (level)
    {
      case ToastLevel.Info:
        BackgroundCssClass = "toastify--info";
        Heading = "Info";
        break;

      case ToastLevel.Success:
        BackgroundCssClass = "toastify--success";
        Heading = "Success";
        break;

      case ToastLevel.Warning:
        BackgroundCssClass = "toastify--warning";
        Heading = "Warning";
        break;

      case ToastLevel.Error:
        BackgroundCssClass = "toastify--error";
        Heading = "Error";
        break;
    }

    Message = message;
  }

  protected override void Dispose(bool disposing)
  {
    ToastService.OnShow -= ShowToast;
    base.Dispose(disposing);
  }
}

