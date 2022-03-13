using System;
using System.Net.Http;
using System.Threading.Tasks;
using Langueedu.Sdk.Account.Request;
using Langueedu.Sdk.Account.Response;
using Langueedu.Sdk.Interfaces;
using Langueedu.Sdk.Utilities;

namespace Langueedu.Sdk.Account
{
  internal class AccountService : ServiceBase, IAccountService
  {
    private const string API_URL_BASE = "/v1/Accounts";

    public AccountService(IHttpClientFactory clientFactory,
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
      var response = await PostAsync<TokenModel>(Configs.TokenEndpoint, loginModel);
      return response;
    }

    public async Task SignOutAsync()
    {
      await PostAsync<string>(Configs.LogoutEndpoint);
    }
  }
}
