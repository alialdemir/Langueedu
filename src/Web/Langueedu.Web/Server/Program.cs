using CurrieTechnologies.Razor.SweetAlert2;
using Langueedu.Web.Components.Provider;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

string langueeduApiUrl = builder.Configuration.GetValue<string>("LangueeduApiUrl");

services.AddComponents();

services.AddLangueeduSdk(langueeduApiUrl, string.Empty);

services.AddControllersWithViews();
services.AddRazorPages();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

services.AddSweetAlert2(options => {
  options.Theme = SweetAlertTheme.Dark;
});

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

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();

app.MapFallbackToPage("{*path:nonfile}", "/_Host");
app.MapFallbackToPage("/", "/Landing");

app.Run();
