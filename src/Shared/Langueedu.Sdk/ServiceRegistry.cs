using System;
using System.Net.Http.Headers;
using Langueedu.Sdk;
using Langueedu.Sdk.Account;
using Langueedu.Sdk.Account.Response;
using Langueedu.Sdk.Course;
using Langueedu.Sdk.Playlist;
using Langueedu.Sdk.Track;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistry
{
  public static IServiceCollection AddLangueeduSdk(this IServiceCollection services, string langueeduApiUrl, TokenModel token)
  {
    services.AddHttpClient();

    Configs.BaseIdentityEndpoint = langueeduApiUrl;

    services.AddHttpClient("LangueeduApi", c =>
    {
      if (!string.IsNullOrEmpty(langueeduApiUrl))
        c.BaseAddress = new Uri(langueeduApiUrl);

      if (token != null && !string.IsNullOrEmpty(token.AccessToken))
        c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);
    });

    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<IPlaylistService, PlaylistService>();
    services.AddScoped<ITrackService, TrackService>();
    services.AddScoped<ICourseService, CourseService>();

    return services;
  }
}
