using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Langueedu.Infrastructure.Data;
using Langueedu.Infrastructure.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Langueedu.Infrastructure;

public static class StartupSetup
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
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
    app.UseAuthorization();
    app.UseAuthentication();
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

    var issuer = configuration.GetValue<string>("Issuer");
    var audience = configuration.GetValue<string>("Audience");
    var signingKey = configuration.GetValue<string>("SigningKey");

    services
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
 {
   options.TokenValidationParameters = new TokenValidationParameters()
   {
     ValidateActor = true,
     ValidateAudience = true,
     ValidateLifetime = true,
     ValidateIssuerSigningKey = true,
     ValidIssuer = issuer,
     ValidAudience = audience,
     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
   };
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

