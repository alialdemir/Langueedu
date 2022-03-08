using System.IdentityModel.Tokens.Jwt;
using Langueedu.Core.Interfaces;
using Langueedu.Infrastructure.Configuration;
using Langueedu.Infrastructure.Data;
using Langueedu.Infrastructure.Logging;
using Langueedu.Infrastructure.Mapping;
using Langueedu.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace Langueedu.Infrastructure;

public static class StartupSetup
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

    services.AddScoped<IAccountService, AccountService>();

    services.AddAutoMapper(typeof(UserProfile));

    services.AddDbContext(configuration);

    services.AddIdentity(configuration);
  }

  public static void ConfigureInfrastructure(this IApplicationBuilder app)
  {
    app.ConfigureIdentity();
  }

  internal static void ConfigureIdentity(this IApplicationBuilder app)
  {
    app.UseIdentityServer();
    app.UseAuthentication();
    app.UseAuthorization();
  }

  internal static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
  {
    string connectionString = configuration.GetConnectionString("SqliteConnection");
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(connectionString)); // will be created in web project root
  }

  internal static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
  {
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

    IdentityModelEventSource.ShowPII = true; //To show detail of error and see the problem

    services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

    var identityUrl = configuration.GetValue<string>("IdentityUrl");
    var audience = configuration.GetValue<string>("Audience");
    var identitySecret = configuration.GetValue<string>("IdentitySecret");

    var builder = services.AddIdentityServer(options =>
   {
     options.IssuerUri = identityUrl;
   })
       .AddDeveloperSigningCredential()// not recommended for production - you need to store your key material somewhere secure
       .AddInMemoryApiScopes(Config.GetApiScopes)
       .AddInMemoryApiResources(Config.Apis)
       .AddInMemoryClients(Config.Clients(identityUrl, identitySecret))
       .AddInMemoryIdentityResources(Config.Ids)
       .AddAspNetIdentity<User>();

    services
        .AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
          options.Authority = identityUrl;
          options.Audience = audience;
          options.RequireHttpsMetadata = false;//identityUrl.StartsWith("https://");
          options.TokenValidationParameters.ValidateAudience = false;
        });

    services.Configure<IdentityOptions>(options =>
    {
      // Password settings
      options.Password.RequireDigit = false;
      options.Password.RequiredLength = 8;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequireUppercase = false;
      options.Password.RequireLowercase = false;

      // Lockout settings
      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
      options.Lockout.MaxFailedAccessAttempts = 10;
      // User settings
      options.User.RequireUniqueEmail = true;
    });
  }
}

