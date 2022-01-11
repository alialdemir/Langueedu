using Ardalis.HttpClientTestExtensions;
using Ardalis.Result;
using Langueedu.API;
using Langueedu.SharedKernel.ViewModels;
using Xunit;

namespace Langueedu.FunctionalTests.ControllerApis;

[Collection("Sequential")]
public class TracksControllerList : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
    private readonly HttpClient _client;

    public TracksControllerList(CustomWebApplicationFactory<WebMarker> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ReturnsOneTrackDetailItem()
    {
        int trackId = 1;

        var result = await _client.GetAndDeserialize<Result<TrackDetailViewModel>>($"/api/Tracks/{trackId}");

        Assert.NotNull(result.Value);
    }
}