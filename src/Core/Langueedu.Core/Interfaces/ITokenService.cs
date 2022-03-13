using Langueedu.Core.Entities.User;
using Langueedu.SharedKernel.ViewModels.Account;

namespace Langueedu.Core.Interfaces
{
  public interface ITokenService
  {
    TokenViewModel GetToken(IUser user);
  }
}
