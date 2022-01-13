﻿using System.Net;
using System.Net.Http.Json;
using Ardalis.HttpClientTestExtensions;
using Ardalis.Result;
using Langueedu.API;
using Langueedu.SharedKernel.ViewModels;
using Langueedu.SharedKernel.ViewModels.Identity;
using Xunit;

namespace Langueedu.FunctionalTests.ControllerApis;

[Collection("Sequential1")]
public class IdentityController : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
    private readonly HttpClient _client;

    public IdentityController(CustomWebApplicationFactory<WebMarker> factory)
    {
        factory.IsMockAuthentication = false;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ReturnsSuccessfulSignInOnPostWithValidCredentials()
    {
        string scopes = "langueedu_api langueedu_web offline_access openid profile";

        var keyValues = new List<KeyValuePair<string, string>>();
        keyValues.Add(new KeyValuePair<string, string>("grant_type", "password"));
        keyValues.Add(new KeyValuePair<string, string>("username", "witcherfearless"));
        keyValues.Add(new KeyValuePair<string, string>("password", "12345678"));
        keyValues.Add(new KeyValuePair<string, string>("client_id", "blazor"));
        keyValues.Add(new KeyValuePair<string, string>("client_secret", "Development Secret Key"));
        keyValues.Add(new KeyValuePair<string, string>("scope", scopes));

        var formContent = new FormUrlEncodedContent(keyValues);

        var postResponse = await _client.PostAsync("/connect/token", formContent);

        TokenViewModel? token = await postResponse.Content.ReadFromJsonAsync<TokenViewModel>();

        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        Assert.NotNull(token);
        Assert.NotNull(token?.AccessToken);
        Assert.Equal(scopes, token?.Scope);
        Assert.Equal("Bearer", token?.TokenType);
    }

    [Fact]
    public async Task ReturnsUnauthorizedGivenWrongUsernameAndPassword()
    {
        var result = await _client.GetAsync("/api/playlists");

        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }
}
