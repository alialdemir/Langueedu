using Blazored.LocalStorage;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Langueedu.Web.Components.Interfaces;
using Langueedu.Web.Components.Internal.Bindings;
using Langueedu.Web.Components.Internal.PropertyBinding;
using Langueedu.Web.Components.Internal.WeakEventListener;
using Langueedu.Web.Components.Services;
using Langueedu.Web.Components.ViewModels;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistry
{
  public static IServiceCollection AddComponents(this IServiceCollection services)
  {
    services.AddBlazoredModal();


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
    services.AddScoped<TrackCoverViewModel>();
    services.AddScoped<GameModeViewModel>();
    services.AddScoped<CourseViewModel>();

    services.AddBlazoredLocalStorage();

    services.AddSweetAlert2(options =>
    {
      options.Theme = SweetAlertTheme.Dark;
    });

    return services;
  }
}
