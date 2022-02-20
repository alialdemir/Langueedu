using FluentValidation;
using Langueedu.Core.Features.Queries.BalanceQuesries.GetBalanceByUserId;

namespace Langueedu.Core.Validations;

public class GetBalanceByUserIdQueryValidator : AbstractValidator<GetBalanceByUserIdQuery>
{
  public GetBalanceByUserIdQueryValidator()
  {
    RuleFor(x => x.UserId)
    .NotEmpty()
    .WithMessage("You must enter a user id!");
  }
}

