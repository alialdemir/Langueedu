using Ardalis.Result;
using Langueedu.Core.Features.Commands.Account.SignUp;

namespace Langueedu.Core.Interfaces
{
    public interface IAccountService
    {
        Task<Result<string>> SingUpAsync(SignUpCommand signUp);
    }
}

