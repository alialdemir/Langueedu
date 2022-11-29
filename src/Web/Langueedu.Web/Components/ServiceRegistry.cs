using Blazored.LocalStorage;
using Blazored.Modal;
using Langueedu.Components.Interfaces;
using Langueedu.Components.Internal.Bindings;
using Langueedu.Components.Internal.PropertyBinding;
using Langueedu.Components.Internal.WeakEventListener;
using Langueedu.Components.Services;
using Langueedu.Components.ViewModels;
using Langueedu.Sdk.Account.Response;
// using CurrieTechnologies.Razor.SweetAlert2;
using Langueedu.Sdk.Interfaces;
using Langueedu.Web.Shared.Models;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistry
{
  public static IServiceCollection AddComponents(this IServiceCollection services)
  {

    services.AddBlazoredModal();

    services.AddBlazoredLocalStorage();
    services.AddScoped<ICookieService, CookieService>();

    AddLangueeduSdk(services);

    services.AddSingleton<IWeakEventManagerFactory, WeakEventManagerFactory>();
    services.AddSingleton<IBindingFactory, BindingFactory>();
    services.AddSingleton<IParameterResolver, ParameterResolver>();
    services.AddSingleton<IParameterCache, ParameterCache>();
    services.AddSingleton<IViewModelParameterSetter, ViewModelParameterSetter>();

    services.AddScoped<ITinySlider, TinySlider>();
    services.AddScoped<ICultureService, CultureService>();
    services.AddScoped<IYoutubePlayer, YoutubePlayer>();

    services.AddScoped<SignInViewModel>();
    services.AddScoped<SignUpViewModel>();
    services.AddScoped<TrackDetailViewModel>();
    services.AddScoped<GameModeViewModel>();
    services.AddScoped<CourseViewModel>();

    services.AddScoped<IToastService, ToastService>();

    return services;
  }

  private static async void AddLangueeduSdk(IServiceCollection services)
  {
    using (ServiceProvider serviceProvider = services.BuildServiceProvider())
    {
      // var localStorageService = serviceProvider.GetRequiredService<ISyncLocalStorageService>();
      var cookieService = serviceProvider.GetRequiredService<ICookieService>();

      var webConfiguration = await cookieService.GetItemAsync<LangueeduWebConfiguration>(nameof(LangueeduWebConfiguration));

      TokenModel token = new();
      // bool isTokenExits = localStorageService.ContainKey("token");
      // if (isTokenExits)
      //   token = localStorageService.GetItem<TokenModel>("token");

      services.AddLangueeduSdk(webConfiguration?.LangueeduApiUrl, token);
    }
  }
}
