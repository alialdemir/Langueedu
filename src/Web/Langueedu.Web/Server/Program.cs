using System.Text.Json;
// using CurrieTechnologies.Razor.SweetAlert2;
using Langueedu.Components.Provider;
using Langueedu.Web.Server.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

string langueeduApiUrl = builder.Configuration.GetValue<string>("LangueeduApiUrl");


services.AddComponents();

services.AddControllersWithViews();

services.AddRazorPages();

services
    .AddOptions<LangueeduWebConfiguration>()
    .Bind(builder.Configuration);

services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

// services.AddLangueeduSdk(langueeduApiUrl, null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();

app.MapFallbackToPage("{*path:nonfile}", "/_Host");
app.MapFallbackToPage("/", "/Landing");

app.Use(async (context, next) =>
{
  using (ServiceProvider serviceProvider = services.BuildServiceProvider())
  {
    var langueeduWebConfiguration = serviceProvider.GetRequiredService<IOptions<LangueeduWebConfiguration>>();

    context.Response.Cookies.Append(nameof(LangueeduWebConfiguration), JsonSerializer.Serialize(langueeduWebConfiguration.Value));
  }

  await next(context);
});

app.Run();
