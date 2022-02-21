using Langueedu.Core.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Langueedu.Core;

public static class StartupSetup
{
  public static void AddCore(this IServiceCollection services, IConfiguration configuration)
  {

    services.AddOptions<SpotifySettings>()
        .Bind(configuration.GetSection(nameof(SpotifySettings)))
        .ValidateDataAnnotations();

    services.AddAutoMapper(typeof(StartupSetup).Assembly);
    // services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetAllPlaylistCommandValidator>());
  }
}
