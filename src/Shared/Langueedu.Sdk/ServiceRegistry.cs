using System;
using Langueedu.Sdk.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistry
{
    public static IServiceCollection AddLangueeduSdk(this IServiceCollection services, string langueeduApiUrl)
    {
        // services.AddHttpClient();

        services.AddHttpClient("LangueeduApi", c =>
        {
            c.BaseAddress = new Uri(langueeduApiUrl);
        });

        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}