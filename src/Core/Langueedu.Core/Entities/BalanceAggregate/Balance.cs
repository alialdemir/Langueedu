using Langueedu.SharedKernel;

namespace Langueedu.Core.Entities.BalanceAggregate;

public class Balance : BaseEntity
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

