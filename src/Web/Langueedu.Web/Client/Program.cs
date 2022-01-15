using Langueedu.Web.Components.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var services = builder.Services;

string langueeduApiUrl = "http://localhost:57679";// builder.Configuration.GetValue<string>("LangueeduApiUrl");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(langueeduApiUrl)
});

services.AddLangueeduSdk(langueeduApiUrl);

services.AddComponents();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();


await builder.Build().RunAsync();

