using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.Result;
using Langueedu.Sdk.Identity.Request;
using Langueedu.Sdk.Identity.Response;
using Langueedu.Sdk.Utilities;

namespace Langueedu.Sdk.Identity
{
    internal class IdentityService : ServiceBase, IIdentityService
    {
        private const string API_URL_BASE = "/v1/Account";

        public IdentityService(IHttpClientFactory clientFactory)
            : base(clientFactory.CreateClient("LangueeduApi"))
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

            var result = await SendAsync<TokenModel>(HttpMethod.Form, Configs.TokenEndpoint, from);
            return result;
        }

        public async Task SignOutAsync()
        {
            await PostAsync<string>(Configs.LogoutEndpoint);
        }
    }
}