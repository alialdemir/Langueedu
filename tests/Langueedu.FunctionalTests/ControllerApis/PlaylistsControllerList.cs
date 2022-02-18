using Ardalis.HttpClientTestExtensions;
using Ardalis.Result;
using Langueedu.API;
using Langueedu.SharedKernel.ViewModels;
using Xunit;

namespace Langueedu.FunctionalTests.ControllerApis;

[Collection("Sequential2")]
public class PlaylistsControllerList : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{

  private readonly HttpClient _client;

  public PlaylistsControllerList(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsAnyItemsPlaylists()
  {
    var result = await _client.GetAndDeserialize<Result<IEnumerable<PlaylistViewModel>>>("/api/v1/playlists");

    Assert.True(result.Value.Any());

#nullable disable
    Assert.NotNull(result.Value.FirstOrDefault().Tracks);
#nullable enable
  }
}

