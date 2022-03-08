namespace Langueedu.Core.Entities.BalanceAggregate;

public class BalanceSilver : Balance,IBalance
{
  public BalanceSilver(string userId) : base(userId)
  {
  }

  public override Balance Increase(decimal amount)
  {
    Silver += amount;

    return this;
  }

  public override Balance Decrease(decimal amount)
  {
    Silver -= amount;

    return this;
  }
}

