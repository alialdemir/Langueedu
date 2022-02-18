using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ardalis.HttpClientTestExtensions;
using Ardalis.Result;
using Langueedu.API;
using Langueedu.SharedKernel.ViewModels;
using Xunit;

namespace Langueedu.FunctionalTests.ControllerApis;

[Collection("Sequential3")]
public class TracksControllerList : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public TracksControllerList(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory
        .CreateClient();
  }

  [Fact]
  public async Task ReturnsOneTrackDetailItem()
  {
    int trackId = 1;

    var result = await _client.GetAndDeserialize<Result<TrackDetailViewModel>>($"/api/v1/Tracks/{trackId}");

    Assert.NotNull(result.Value);
  }

  [Fact]
  public async Task FollowTrackReturnsAlready()
  {
    int trackId = 1;
    string url = $"/api/v1/Tracks/{trackId}/Follow";

    await _client.PostAsJsonAsync(url, "");

    var result = await _client.PostAsJsonAsync(url, "");
    var resultJson = await result.Content.ReadAsStringAsync();

    Assert.True(resultJson?.Contains("The track is already being followed!"));
  }

  [Fact]
  public async Task FollowTrackReturnsSuccess()
  {
    int trackId = 1;

    var result = await _client.PostAsJsonAsync($"/api/v1/Tracks/{trackId}/Follow", "");

    Assert.True(result.IsSuccessStatusCode);
  }
}