using Ardalis.Specification;

namespace Langueedu.Core.Specifications.Balance;

public class GetBalanceByUserIdSpec : Specification<Entities.BalanceAggregate.Balance, Entities.BalanceAggregate.Balance>
{
  public GetBalanceByUserIdSpec(string userId)
  {
    Query
      .Where(x => x.UserId == userId);

    Query
      .Select(balance => balance);
  }
}

