using Langueedu.Components.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace Langueedu.Mobile;

public static class MauiProgram
{
  public static string Base = DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.1.180" : "http://localhost";
  public static string LangueeduApiUrl = $"{Base}:5200";

  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();
    builder
      .RegisterBlazorMauiWebView()
      .UseMauiApp<App>()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
      });

    builder.Services.AddBlazorWebView();
    AppInit(builder.Services);

    return builder.Build();
  }

  private static void AppInit(IServiceCollection services)
  {
    services.AddComponents();
    services.AddAuthorizationCore();
    services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
  }
}
