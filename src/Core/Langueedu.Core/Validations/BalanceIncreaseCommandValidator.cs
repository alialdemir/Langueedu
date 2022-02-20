using FluentValidation;
using Langueedu.Core.Features.Commands.Balance.BalanceIncrease;

namespace Langueedu.Core.Validations;

public class BalanceIncreaseCommandValidator : AbstractValidator<BalanceIncreaseCommand>
{
  public BalanceIncreaseCommandValidator()
  {
    RuleFor(x => x.Balance.UserId)
      .NotEmpty()
      .WithMessage("User id required field!");

    RuleFor(x => x.Amount)
      .GreaterThan((short)0)
      .WithMessage("Amount must be greater than zero.");
  }
}