using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.BalanceAggregate;

public class Balance : BaseEntity, IAggregateRoot
{
  public Balance(string userId)
  {
    UserId = userId;
  }

  public string UserId { get; }

  public decimal Gold { get; private set; }

  public Balance IncreaseGold(decimal gold)
  {
    Gold += gold;

    return this;
  }

  public Balance DecreaseGold(decimal gold)
  {
    Gold -= gold;

    return this;
  }
}

