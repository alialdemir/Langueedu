using System.Threading.Tasks;
using Langueedu.Sdk.Identity.Request;
using Langueedu.Sdk.Identity.Response;

namespace Langueedu.Sdk.Identity
{
  public interface IIdentityService
  {
    Task<Result<TokenModel>> SignInAsync(LoginModel loginModel);
    Task SignOutAsync();
    Task<Result<string>> SignUpAsync(SignUpModel signUpModel);
  }
}
