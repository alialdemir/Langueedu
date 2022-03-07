namespace Langueedu.API.Extensions;


public static partial class Startup
{
  public static string CORS_POLICY => "CORS_POLICY";

  public static IServiceCollection AddCorsConfigure(this IServiceCollection services, IConfiguration? configuration = null)
  {
    services.AddCors(options =>
    {
      options.AddPolicy(name: CORS_POLICY,
                   builder =>
                   {
                     if (configuration != null)
                     {
                       string clientDomain = configuration.GetValue<string>("ClientDomain");
                       if (!string.IsNullOrEmpty(clientDomain))
                         builder = builder
                         .WithOrigins(clientDomain.Replace("host.docker.internal", "localhost")
                         .TrimEnd('/'));
                     }
                     builder.AllowAnyMethod();
                     builder.AllowAnyHeader();
                   });
    });

    return services;
  }

  public static IApplicationBuilder AddCors(this IApplicationBuilder app)
  {
    app.UseCors(policyName: CORS_POLICY);


    return app;
  }
}
