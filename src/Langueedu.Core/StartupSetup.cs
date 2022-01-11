using Microsoft.Extensions.DependencyInjection;

namespace Langueedu.Core;

public static class StartupSetup
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StartupSetup).Assembly);
        // services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetAllPlaylistCommandValidator>());
    }
}
