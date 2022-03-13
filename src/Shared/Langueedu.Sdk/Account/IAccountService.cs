using System.Threading.Tasks;
using Langueedu.Sdk.Account.Request;
using Langueedu.Sdk.Account.Response;

namespace Langueedu.Sdk.Account
{
  public interface IAccountService
  {
    Task<Result<TokenModel>> SignInAsync(LoginModel loginModel);
    Task SignOutAsync();
    Task<Result<string>> SignUpAsync(SignUpModel signUpModel);
  }
}
