namespace Langueedu.API.Extensions;


public static partial class Startup
{
  public static string CORS_POLICY => "CORS_POLICY";

  public static IServiceCollection AddCorsConfigure(this IServiceCollection services, IConfiguration configuration = null)
  {
    services.AddCors(options =>
    {
      options.AddPolicy(name: CORS_POLICY,
                   builder =>
                   {
                     if (configuration != null)
                     {
                       string[] clientDomains = configuration.GetValue<string>("ClientDomain").Split("|");
                       if (clientDomains.Any())
                         builder = builder
                          .WithOrigins(clientDomains.Select(x=>x.Replace("host.docker.internal", "localhost")
                         .TrimEnd('/')).ToArray());
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
