using Langueedu.Core.Entities.BalanceAggregate;
using Xunit;

namespace Langueedu.UnitTests.Core.PlaylistAggregate;

public class Balance_IncreaseGold
{
  [Fact]
  public void IncreaseGoldAndCheckGoldBalance()
  {
    BalanceGold balance = new BalanceGold("test user id");

    decimal gold = 9999;

    balance.Increase(balance, gold);

    Assert.Equal(gold, balance.Gold);
  }

  [Fact]
  public void DecreaseGoldAndCheckNewAmount()
  {
    BalanceGold balance = new BalanceGold("test user id");

    decimal increasegold = 9999;
    decimal decreaseGold = 1000;
    decimal diff = increasegold - decreaseGold;

    balance.Increase(balance, increasegold);

    balance.Decrease(balance, decreaseGold);

    Assert.Equal(diff, balance.Gold);
  }
}

