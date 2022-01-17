﻿namespace Langueedu.API.Extensions;


public static partial class Startup
{
    public static IServiceCollection AddCorsConfigure(this IServiceCollection services, IConfiguration? configuration = null)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                if (configuration != null)
                {
                    string clientDomain = configuration.GetValue<string>("ClientDomain");
                    if (!string.IsNullOrEmpty(clientDomain))
                        builder = builder.WithOrigins(clientDomain);
                }

                builder
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials()
                   .SetIsOriginAllowed((host) => true);
            });
        });

        return services;
    }

    public static IApplicationBuilder AddCors(this IApplicationBuilder app)
    {
        app.UseCors("CorsPolicy");

        return app;
    }
}