using Blazored.LocalStorage;
using Langueedu.Sdk.Identity.Response;
using Langueedu.Web.Components.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var services = builder.Services;

string langueeduApiUrl = builder.Configuration.GetValue<string>("LangueeduApiUrl");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(langueeduApiUrl)
});

services.AddComponents();

using (ServiceProvider serviceProvider = services.BuildServiceProvider())
{
    var localStorageService = serviceProvider.GetRequiredService<ILocalStorageService>();

    TokenModel? token = await localStorageService.GetItemAsync<TokenModel>("token");

    services.AddLangueeduSdk(langueeduApiUrl, token?.AccessToken);
}


builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();


await builder.Build().RunAsync();

