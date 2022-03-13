using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.ViewModels.Account;
using MediatR;

namespace Langueedu.Core.Features.Commands.Account.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, Result<TokenViewModel>>
{
  private readonly ITokenService _tokenService;
  private readonly IAccountService _accountService;

  public SignInCommandHandler(ITokenService tokenService,
                              IAccountService accountService)
  {
    _tokenService = tokenService;
    _accountService = accountService;
  }

  public async Task<Result<TokenViewModel>> Handle(SignInCommand request, CancellationToken cancellationToken)
  {
    if (request == null)
      return Result<TokenViewModel>.Error("User information cannot be null.");

    var validator = new SignInCommandValidator();
    var valid = validator.Validate(request);
    if (!valid.IsValid)
      return Result<TokenViewModel>.Invalid(valid.AsErrors());

    var user = await _accountService.SingInAsync(request);
    if (!user.IsSuccess)
      return Result<TokenViewModel>.Error(user.Errors.ToArray());

    var token = _tokenService.GetToken(user.Value);
    return token;
  }
}