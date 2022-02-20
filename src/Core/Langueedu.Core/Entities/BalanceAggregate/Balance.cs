using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.BalanceAggregate;

public class Balance : BaseEntity, IAggregateRoot, IBalance
{
  public Balance(string userId)
  {
    UserId = userId;
  }

  public string UserId { get; }

  public virtual Balance Increase(Balance balance, decimal amount)
  {
    return this;
  }

  public virtual Balance Decrease(Balance balance, decimal amount)
  {
    return this;
  }

  public decimal Gold { get; internal set; }

  public decimal Silver { get; internal set; }
}
