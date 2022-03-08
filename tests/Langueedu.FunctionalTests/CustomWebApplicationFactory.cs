using Langueedu.UnitTests;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Langueedu.FunctionalTests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
  public bool IsMockAuthentication { get; set; } = true;

  /// <summary>
  /// Overriding CreateHost to avoid creating a separate ServiceProvider per this thread:
  /// https://github.com/dotnet-architecture/eShopOnWeb/issues/465
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  protected override IHost CreateHost(IHostBuilder builder)
  {
    var host = builder.Build();

    builder.ConfigureWebHost(ConfigureWebHost);

    // Get service provider.
    var serviceProvider = host.Services;

    host.Start();
    return host;
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder
        .ConfigureServices(services =>
        {
          // Remove the app's ApplicationDbContext registration.
          //var descriptor = services.SingleOrDefault(
          //d => d.ServiceType ==
          //    typeof(DbContextOptions<AppDbContext>));

          //if (descriptor != null)
          //{
          //  services.Remove(descriptor);
          //}

          if (IsMockAuthentication)
          {
            var basicAuth = services.SingleOrDefault(
                                  s => s.ServiceType ==
                                      typeof(JwtBearerHandler));

            services.Remove(basicAuth);

            services.AddTransient<IAuthenticationSchemeProvider, MockSchemeProvider>();
          }

          // This should be set for each individual test run
          string inMemoryCollectionName = Guid.NewGuid().ToString();

          // Add ApplicationDbContext using an in-memory database for testing.
          //    services.AddDbContext<AppDbContext>(options =>
          //{
          //  options.UseInMemoryDatabase(inMemoryCollectionName);
          //});

          services.AddScoped<IMediator, NoOpMediator>();
        });
  }
}

