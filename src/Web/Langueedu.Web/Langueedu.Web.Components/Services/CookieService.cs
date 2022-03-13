using Langueedu.Web.Components.Interfaces;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Langueedu.Web.Components.Services;

public class CookieService : ICookieService
{
  private readonly IJSRuntime _JSRuntime;

  public CookieService(IJSRuntime JSRuntime)
  {
    _JSRuntime = JSRuntime;
  }

  public async Task<T> GetItemAsync<T>(string key)
  {
    string json = await _JSRuntime.InvokeAsync<string>("methods.getCookie", key);
    if (string.IsNullOrEmpty(key))
      return default(T);

    var result = JsonConvert.DeserializeObject<T>(json);
    return result;
  }
}