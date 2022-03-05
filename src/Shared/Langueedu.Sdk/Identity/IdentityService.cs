using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Langueedu.Sdk.Identity.Request;
using Langueedu.Sdk.Identity.Response;
using Langueedu.Sdk.Interfaces;
using Langueedu.Sdk.Utilities;

namespace Langueedu.Sdk.Identity
{
  internal class IdentityService : ServiceBase, IIdentityService
  {
    private const string API_URL_BASE = "/v1/Accounts";

    public IdentityService(IHttpClientFactory clientFactory,
                           IToastService toastService)
        : base(clientFactory.CreateClient("LangueeduApi"),
               toastService)
    {
    }

    public async Task<Result<string>> SignUpAsync(SignUpModel signUpModel)
    {
      if (signUpModel == null)
        throw new ArgumentNullException(nameof(signUpModel));

      string uri = UriHelper.CombineUri(Configs.GatewaEndpoint, API_URL_BASE);

      var response = await PostAsync<string>(uri, signUpModel);
      return response;
    }

    public async Task<Result<TokenModel>> SignInAsync(LoginModel loginModel)
    {
      var from = new Dictionary<string, string>
                {
                    {"username", loginModel.UserName },
                    {"password", loginModel.Password },
                    {"grant_type", "password" },
                    {"client_id", Configs.ClientId },
                    {"client_secret", Configs.ClientSecret },
                    {"scope", Configs.Scope },
                };

      SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

      var response = await _httpClient.SendAsync(new HttpRequestMessage(System.Net.Http.HttpMethod.Post, Configs.TokenEndpoint)
      {
        Content = new FormUrlEncodedContent((Dictionary<string, string>)from)
      });

      if (response.IsSuccessStatusCode)
      {
        var token = await RequestAsResultAsync<TokenModel>(response);
        return Result<TokenModel>.Success(token);
      }

      var error = await RequestAsResultAsync<TokenErrorModel>(response);
      return Result<TokenModel>.Error(error.ErrorDescription.Replace("_", " "));
    }

    public async Task SignOutAsync()
    {
      await PostAsync<string>(Configs.LogoutEndpoint);
    }
  }
}
