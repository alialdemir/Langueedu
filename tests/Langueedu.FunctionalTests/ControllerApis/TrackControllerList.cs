using System.Net.Http.Headers;
using Ardalis.HttpClientTestExtensions;
using Ardalis.Result;
using Langueedu.API;
using Langueedu.SharedKernel.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
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
}