using Ardalis.Result;
using MediatR;

namespace Langueedu.Core.Features.Commands.Account.SignUp
{
    public class SignUpCommand : IRequest<Result<string>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string LanguageCode { get; set; }
    }
}

