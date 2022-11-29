using Langueedu.Components.Interfaces;
using Microsoft.JSInterop;

namespace Langueedu.Components.Services;

public class CultureService : ICultureService
{
  private readonly IJSRuntime _jSRuntime;

  public CultureService(IJSRuntime jSRuntime)
  {
    _jSRuntime = jSRuntime;
  }

  public async Task<string> GetCulture()
  {
    return await _jSRuntime.InvokeAsync<string>("getCulture");
  }
}
