using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Specifications.Balance;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.Interfaces;
using Langueedu.SharedKernel.ViewModels.Balance;
using MediatR;

namespace Langueedu.Core.Features.Queries.BalanceQuesries;

public class GetBalanceByUserIdQueryHandler : IRequestHandler<GetBalanceByUserIdQuery, Result<BalanceViewModel>>
{
  private readonly IReadRepository<Balance> _balanceReadRepository;

  public GetBalanceByUserIdQueryHandler(IReadRepository<Balance> balanceReadRepository)
  {
    _balanceReadRepository = balanceReadRepository;
  }

  public async Task<Result<BalanceViewModel>> Handle(GetBalanceByUserIdQuery request, CancellationToken cancellationToken)
  {
    var validator = new GetBalanceByUserIdQueryValidator();
    var validate = validator.Validate(request);
    if (!validate.IsValid)
      return Result<BalanceViewModel>.Invalid(validate.AsErrors());

    var spec = new GetBalanceByUserIdSpec(request.UserId);
    var balance = await _balanceReadRepository.GetBySpecAsync(spec);
    if (balance == null)
      balance = new BalanceViewModel();

    return Result<BalanceViewModel>.Success(balance);
  }
}