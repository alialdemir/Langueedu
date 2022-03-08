using FluentValidation;
using Langueedu.Core.Features.Commands.Balance.BalanceIncrease;

namespace Langueedu.Core.Validations;

public class BalanceIncreaseCommandValidator : AbstractValidator<BalanceIncreaseCommand>
{
  public BalanceIncreaseCommandValidator()
  {
    RuleFor(x => x.UserId)
      .NotEmpty()
      .WithMessage("User id required field!");

    RuleFor(x => x.BalanceType)
    .NotNull()
    .WithMessage("BalanceType must be greater than zero.");

    RuleFor(x => x.Amount)
      .GreaterThan((short)0)
      .WithMessage("Amount must be greater than zero.");
  }
}