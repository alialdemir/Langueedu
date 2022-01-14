using Langueedu.Web.Components;
using Langueedu.Web.Components.ViewModels;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistry
{
    public static IServiceCollection AddComponents(this IServiceCollection services)
    {
        services.AddScoped<SignInViewModel>();
        services.AddScoped<SignUpViewModel>();

        return services;
    }
}