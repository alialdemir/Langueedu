using System.Net.Http.Json;
using Ardalis.HttpClientTestExtensions;
using Ardalis.Result;
using Langueedu.API;
using Langueedu.SharedKernel.ViewModels;
using Xunit;

namespace Langueedu.FunctionalTests.ControllerApis;

[Collection("Sequential3")]
public class TracksController : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public TracksController(CustomWebApplicationFactory<WebMarker> factory)
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

  [Fact]
  public async Task UnFollowTrackReturnsSuccess()
  {
    int trackId = 1;

    var postResponse = await _client.PostAsJsonAsync($"/api/v1/Tracks/{trackId}/Follow", "");

    var result = await _client.DeleteAsync($"/api/v1/Tracks/{trackId}/Follow");
    var response = await result.Content.ReadFromJsonAsync<Result<bool>>();
    Assert.True(response.IsSuccess);
  }
}