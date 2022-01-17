using Blazored.LocalStorage;
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


        services.AddSingleton<IParameterResolver, ParameterResolver>();
        services.AddSingleton<IParameterCache, ParameterCache>();
        services.AddSingleton<IViewModelParameterSetter, ViewModelParameterSetter>();

        services.AddScoped<ITinySlider, TinySlider>();
        services.AddScoped<ICultureService, CultureService>();

        services.AddScoped<SignInViewModel>();
        services.AddScoped<SignUpViewModel>();
        services.AddScoped<TrackCoverViewModel>();

        services.AddBlazoredLocalStorage();

        services.AddSweetAlert2(options =>
        {
            options.Theme = SweetAlertTheme.Dark;
        });

        return services;
    }
}