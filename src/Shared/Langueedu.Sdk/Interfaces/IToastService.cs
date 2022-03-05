using System;

namespace Langueedu.Sdk.Interfaces
{
  public interface IToastService
  {
    event Action<string, ToastLevel> OnShow;
    event Action OnHide;
    void ShowToast(string message, ToastLevel level);
  }
}