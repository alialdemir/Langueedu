using Blazored.LocalStorage;
// using CurrieTechnologies.Razor.SweetAlert2;
using Langueedu.Sdk.Identity.Response;
using Langueedu.Web.Client.Models;
using Langueedu.Web.Components.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Newtonsoft.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var services = builder.Services;

services.AddComponents();

using (ServiceProvider serviceProvider = services.BuildServiceProvider())
{
  var localStorageService = serviceProvider.GetRequiredService<ILocalStorageService>();
  var _JSRuntime = serviceProvider.GetRequiredService<IJSRuntime>();

  string langueeduwebConfiguration = await _JSRuntime.InvokeAsync<string>("methods.getCookie", "LangueeduWebConfiguration");
  LangueeduWebConfiguration webConfiguration = new LangueeduWebConfiguration();
  if (!string.IsNullOrEmpty(langueeduwebConfiguration))
    webConfiguration = JsonConvert.DeserializeObject<LangueeduWebConfiguration>(langueeduwebConfiguration);


  TokenModel token = new();
  bool isTokenExits = await localStorageService.ContainKeyAsync("token");
  if (isTokenExits)
    token = await localStorageService.GetItemAsync<TokenModel>("token");

  // services.AddSweetAlert2(options =>
  // {
  //   options.Theme = SweetAlertTheme.Dark;
  // });

  services.AddLangueeduSdk(webConfiguration?.LangueeduApiUrl, token?.AccessToken);
}

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();


await builder.Build().RunAsync();

