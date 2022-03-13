using Langueedu.Mobile.Data;
using Langueedu.Web.Components.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace Langueedu.Mobile;

public static class MauiProgram
{
  public static string Base = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2" : "http://localhost";
  public static string APIUrl = $"{Base}:5200/v1/";

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
		builder.Services.AddSingleton<WeatherForecastService>();
		AppInit(builder.Services);

		return builder.Build();
	}

	private static void AppInit(IServiceCollection 	services){

    services.AddComponents();

    // using (ServiceProvider serviceProvider = services.BuildServiceProvider())
    // {
    //   var localStorageService = serviceProvider.GetRequiredService<ILocalStorageService>();
    //   var _JSRuntime = serviceProvider.GetRequiredService<IJSRuntime>();

    //   string langueeduwebConfiguration = await _JSRuntime.InvokeAsync<string>("methods.getCookie", "LangueeduWebConfiguration");
    //   LangueeduWebConfiguration webConfiguration = new LangueeduWebConfiguration();
    //   if (!string.IsNullOrEmpty(langueeduwebConfiguration))
    //     webConfiguration = JsonConvert.DeserializeObject<LangueeduWebConfiguration>(langueeduwebConfiguration);


    //   TokenModel token = new();
    //   bool isTokenExits = await localStorageService.ContainKeyAsync("token");
    //   if (isTokenExits)
    //     token = await localStorageService.GetItemAsync<TokenModel>("token");

      // services.AddSweetAlert2(options =>
      // {
      //   options.Theme = SweetAlertTheme.Dark;
      // });

      services.AddLangueeduSdk(APIUrl, "");
    // }

    services.AddAuthorizationCore();
    services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
	}
}
