using Blazored.LocalStorage;
using Langueedu.Web.Components;
using Langueedu.Web.Components.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using CurrieTechnologies.Razor.SweetAlert2;
using Langueedu.Web.Components.Services;
using Langueedu.Web.Components.Interfaces;

public static class ServiceRegistry
{
    public static IServiceCollection AddComponents(this IServiceCollection services)
    {

        services.AddScoped<ICultureService, CultureService>();

        services.AddScoped<SignInViewModel>();
        services.AddScoped<SignUpViewModel>();

        services.AddBlazoredLocalStorage();

        services.AddSweetAlert2(options =>
        {
            options.Theme = SweetAlertTheme.Dark;
        });

        return services;
    }
}