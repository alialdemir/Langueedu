namespace Langueedu.Core.Entities.BalanceAggregate;

public class BalanceGold : Balance,IBalance
{
  public BalanceGold(string userId) : base(userId)
  {
  }

  public override Balance Increase(decimal amount)
  {
    Gold += amount;

    return this;
  }

  public override Balance Decrease(decimal amount)
  {
    Gold -= amount;

    return this;
  }
}
