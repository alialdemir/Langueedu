using Langueedu.Components.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var services = builder.Services;


services.AddAuthorizationCore();
services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

services.AddComponents();

await builder.Build().RunAsync();

