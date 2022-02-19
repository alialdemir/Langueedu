using Ardalis.Specification;
using Langueedu.SharedKernel.ViewModels.Balance;

namespace Langueedu.Core.Specifications.Balance;

public class GetBalanceByUserIdSpec : Specification<Entities.BalanceAggregate.Balance, BalanceViewModel>
{
  public GetBalanceByUserIdSpec(string userId)
  {
    Query
      .Where(x => x.UserId == userId);

    Query
      .Select(x => new BalanceViewModel
      {
        Gold = x.Gold
      });

  }
}

