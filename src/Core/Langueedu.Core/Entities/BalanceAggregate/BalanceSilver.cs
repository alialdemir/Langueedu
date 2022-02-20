using Langueedu.Core.Features.Commands.Course.CreateCourse;

namespace Langueedu.Core.Entities.BalanceAggregate;

public class BalanceSilver : Balance
{
  public BalanceSilver(string userId) : base(userId)
  {
  }

  public BalanceSilver()
  {

  }

  public override Balance Increase(Balance balance, decimal amount)
  {
    balance.Silver += amount;

    return balance;
  }

  public override Balance Decrease(Balance balance, decimal amount)
  {
    balance.Silver -= amount;

    return balance;
  }
}

