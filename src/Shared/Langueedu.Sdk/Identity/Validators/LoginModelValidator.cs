using FluentValidation;
using Langueedu.Sdk.Identity.Request;

namespace Langueedu.Sdk.Identity.Validators
{
  public class LoginModelValidator : AbstractValidator<LoginModel>
  {
    public LoginModelValidator()
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
}
