using Ardalis.Result;
using Langueedu.Core.Interfaces;
using MediatR;

namespace Langueedu.Core.Features.Commands.Account.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result<string>>
    {
        private readonly IAccountService _accountService;

        public SignUpCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Result<string>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountService.SingUpAsync(request);
            return result;
        }
    }
}