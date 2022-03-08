using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.BalanceAggregate;

public class Balance : BaseEntityNoId, IAggregateRoot, IBalance
{
  public Balance(string userId)
  {
    UserId = userId;
  }

  public string UserId { get; }

  public virtual Balance Increase(decimal amount)
  {
    return this;
  }

  public virtual Balance Decrease(decimal amount)
  {
    return this;
  }

  public decimal Gold { get; internal set; }

  public decimal Silver { get; internal set; }
}
