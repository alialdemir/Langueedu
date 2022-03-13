using FluentValidation;
using Langueedu.Core.Features.Commands.Account.SignIn;

namespace Langueedu.Core.Validations;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
  public SignInCommandValidator()
  {
    RuleFor(x => x.UserName)
    .NotEmpty()
    .WithMessage("You must enter a username")
    .MinimumLength(3)
    .WithMessage("Username must be a minimum of 3 characters ");


    RuleFor(x => x.Password)
    .NotEmpty()
    .WithMessage("You must enter a password")
    .MinimumLength(8)
    .WithMessage("Username must be a minimum of 8 characters ");
  }
}

