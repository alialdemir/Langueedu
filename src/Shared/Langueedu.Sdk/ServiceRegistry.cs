using System;
using System.Net.Http.Headers;
using Langueedu.Sdk;
using Langueedu.Sdk.Course;
using Langueedu.Sdk.Identity;
using Langueedu.Sdk.Playlist;
using Langueedu.Sdk.Track;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistry
{
  public static IServiceCollection AddLangueeduSdk(this IServiceCollection services, string langueeduApiUrl, string accessToken)
  {
    services.AddHttpClient();
    System.Console.WriteLine($"sdk client {accessToken}");

    Configs.BaseIdentityEndpoint = langueeduApiUrl;

    services.AddHttpClient("LangueeduApi", c =>
    {
      if (!string.IsNullOrEmpty(langueeduApiUrl))
        c.BaseAddress = new Uri(langueeduApiUrl);

      if (!string.IsNullOrEmpty(accessToken))
        c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    });

    services.AddScoped<IIdentityService, IdentityService>();
    services.AddScoped<IPlaylistService, PlaylistService>();
    services.AddScoped<ITrackService, TrackService>();
    services.AddScoped<ICourseService, CourseService>();

    return services;
  }
}
