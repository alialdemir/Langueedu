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

await builder.Build().RunAsync();

