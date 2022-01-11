using Ardalis.HttpClientTestExtensions;
using Ardalis.Result;
using Langueedu.SharedKernel.ViewModels;
using Langueedu.Web;
using Xunit;

namespace Langueedu.FunctionalTests.ControllerApis;

[Collection("Sequential")]
public class PlaylistControllerList : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
    private readonly HttpClient _client;

    public PlaylistControllerList(CustomWebApplicationFactory<WebMarker> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ReturnsOnePlaylist()
    {
        var result = await _client.GetAndDeserialize<Result<IEnumerable<PlaylistViewModel>>>("/api/playlist");

        Assert.Single(result.Value);

#nullable disable
        Assert.NotNull(result.Value.FirstOrDefault().Tracks);
#nullable enable
    }
}

