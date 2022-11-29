using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Langueedu.Sdk.Account.Response;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Logging;

namespace Langueedu.Components.Provider;

public class AuthStateProvider : AuthenticationStateProvider
{
  private readonly ILocalStorageService _localStorageService;
  private readonly AuthenticationState _anonymous;

  public AuthStateProvider(ILocalStorageService localStorageService)
  {
    _localStorageService = localStorageService;
    _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
  }

  public async override Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    try
    {
      TokenModel token = await _localStorageService.GetItemAsync<TokenModel>("token");

      if (token == null || string.IsNullOrEmpty(token.AccessToken))
        return _anonymous;

      var tokenDecode = GetClaims(token.AccessToken);
      if (tokenDecode == null)
        return _anonymous;

      var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(tokenDecode, "jwtAuthType"));

      return new AuthenticationState(claimsPrincipal);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);

      await _localStorageService.RemoveItemAsync("token");

      return _anonymous;
    }
  }

  public async Task NotifyUserLogin(TokenModel token)
  {
    if (token == null)
      return;

    var tokenDecode = GetClaims(token.AccessToken);
    if (tokenDecode == null)
      return;

    await _localStorageService.SetItemAsync<TokenModel>("token", token);

    var cp = new ClaimsPrincipal(new ClaimsIdentity(tokenDecode, "jwtAuthType"));
    var authState = Task.FromResult(new AuthenticationState(cp));

    NotifyAuthenticationStateChanged(authState);
  }

  public void NotifyUserLogout()
  {
    var authState = Task.FromResult(_anonymous);
    NotifyAuthenticationStateChanged(authState);
  }

  private IEnumerable<Claim> GetClaims(string accessToken)
  {
    if (string.IsNullOrEmpty(accessToken))
      return null;

    IdentityModelEventSource.ShowPII = true;

    var tokenDecode = new JwtSecurityToken(jwtEncodedString: accessToken);
    return tokenDecode?.Claims ?? null;
  }
}
