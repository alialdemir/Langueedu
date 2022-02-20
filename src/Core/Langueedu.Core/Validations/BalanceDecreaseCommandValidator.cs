using FluentValidation;
using Langueedu.Core.Features.Commands.Balance;

namespace Langueedu.Core.Validations;

public class BalanceDecreaseCommandValidator : AbstractValidator<BalanceDecreaseCommand>
{
  public BalanceDecreaseCommandValidator()
  {
    RuleFor(x => x.Balance.UserId)
      .NotEmpty()
      .WithMessage("User id required field!");

    //RuleFor(x => x.BalanceType)
    //  .NotNull()
    //  .WithMessage("Balance type required field!");

    RuleFor(x => x.Amount)
      .GreaterThan((short)0)
      .WithMessage("Amount must be greater than zero.");
  }
}

