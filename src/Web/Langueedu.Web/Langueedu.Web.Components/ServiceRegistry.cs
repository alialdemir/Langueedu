using Blazored.LocalStorage;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Langueedu.Web.Components.Interfaces;
using Langueedu.Web.Components.PropertyBinding;
using Langueedu.Web.Components.Services;
using Langueedu.Web.Components.ViewModels;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistry
{
    public static IServiceCollection AddComponents(this IServiceCollection services)
    {
        services.AddBlazoredModal();

        services.AddSingleton<IParameterResolver, ParameterResolver>();
        services.AddSingleton<IParameterCache, ParameterCache>();
        services.AddSingleton<IViewModelParameterSetter, ViewModelParameterSetter>();

         services.AddScoped<ITinySlider, TinySlider>();
         services.AddScoped<ICultureService, CultureService>();

        services.AddScoped<SignInViewModel>();
        services.AddScoped<SignUpViewModel>();
        services.AddScoped<TrackCoverViewModel>();
        services.AddScoped<GameModeViewModel>();

        services.AddBlazoredLocalStorage();

        services.AddSweetAlert2(options =>
        {
            options.Theme = SweetAlertTheme.Dark;
        });

        return services;
    }
}