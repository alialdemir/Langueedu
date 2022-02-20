namespace Langueedu.Core.Entities.BalanceAggregate;

public class BalanceGold : Balance
{
  public BalanceGold(string userId) : base(userId)
  {
  }

  public override Balance Increase(Balance balance, decimal amount)
  {
    balance.Gold += amount;

    return balance;
  }

  public override Balance Decrease(Balance balance, decimal amount)
  {
    balance.Gold -= amount;

    return balance;
  }
}
