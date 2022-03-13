using Blazored.LocalStorage;
using Blazored.Modal;
using Langueedu.Sdk.Account.Response;
// using CurrieTechnologies.Razor.SweetAlert2;
using Langueedu.Sdk.Interfaces;
using Langueedu.Web.Components.Interfaces;
using Langueedu.Web.Components.Internal.Bindings;
using Langueedu.Web.Components.Internal.PropertyBinding;
using Langueedu.Web.Components.Internal.WeakEventListener;
using Langueedu.Web.Components.Services;
using Langueedu.Web.Components.ViewModels;
using Langueedu.Web.Shared.Models;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistry
{
  public static IServiceCollection AddComponents(this IServiceCollection services)
  {
    services.AddBlazoredModal();

    services.AddBlazoredLocalStorage();

    services.AddSingleton<IWeakEventManagerFactory, WeakEventManagerFactory>();
    services.AddSingleton<IBindingFactory, BindingFactory>();
    services.AddSingleton<IParameterResolver, ParameterResolver>();
    services.AddSingleton<IParameterCache, ParameterCache>();
    services.AddSingleton<IViewModelParameterSetter, ViewModelParameterSetter>();

    services.AddScoped<ITinySlider, TinySlider>();
    services.AddScoped<ICultureService, CultureService>();
    services.AddScoped<IYoutubePlayer, YoutubePlayer>();
    services.AddScoped<ICookieService, CookieService>();

    services.AddScoped<SignInViewModel>();
    services.AddScoped<SignUpViewModel>();
    services.AddScoped<TrackDetailViewModel>();
    services.AddScoped<GameModeViewModel>();
    services.AddScoped<CourseViewModel>();

    services.AddScoped<IToastService, ToastService>();

    AddLangueeduSdk(services).ConfigureAwait(false);

    return services;
  }

  private static async Task AddLangueeduSdk(IServiceCollection services)
  {
    using (ServiceProvider serviceProvider = services.BuildServiceProvider())
    {
      var localStorageService = serviceProvider.GetRequiredService<ILocalStorageService>();
      var cookieService = serviceProvider.GetRequiredService<ICookieService>();

      var webConfiguration = await cookieService.GetItemAsync<LangueeduWebConfiguration>(nameof(LangueeduWebConfiguration));

      TokenModel token = new();
      bool isTokenExits = await localStorageService.ContainKeyAsync("token");
      if (isTokenExits)
        token = await localStorageService.GetItemAsync<TokenModel>("token");

      services.AddLangueeduSdk(webConfiguration?.LangueeduApiUrl, token);
    }
  }
}
