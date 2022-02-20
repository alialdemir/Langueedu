using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Features.Queries.BalanceQuesries.GetBalanceByUserId;
using Langueedu.Core.Specifications.Balance;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.Interfaces;
using MediatR;

namespace Langueedu.Core.Features.Queries.BalanceQuesries;

public class GetBalanceByUserIdQueryHandler : IRequestHandler<GetBalanceByUserIdQuery, Result<Balance>>
{
  private readonly IReadRepository<Balance> _balanceReadRepository;

  public GetBalanceByUserIdQueryHandler(IReadRepository<Balance> balanceReadRepository
    )
  {
    _balanceReadRepository = balanceReadRepository;
  }

  public async Task<Result<Balance>> Handle(GetBalanceByUserIdQuery request, CancellationToken cancellationToken)
  {
    var validator = new GetBalanceByUserIdQueryValidator();
    var validate = validator.Validate(request);
    if (!validate.IsValid)
      return Result<Balance>.Invalid(validate.AsErrors());

    var spec = new GetBalanceByUserIdSpec(request.UserId);
    var balance = await _balanceReadRepository.GetBySpecAsync(spec);
    if (balance == null)
      return Result<Balance>.Error("Balance not found!");

    return Result<Balance>.Success(balance);
  }
}