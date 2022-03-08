using Langueedu.Core.Entities.BalanceAggregate;
using Xunit;

namespace Langueedu.UnitTests.Core.PlaylistAggregate;

public class Balance_DecreaseGold
{
  [Fact]
  public void DecreaseGoldAndCheckGoldBalance()
  {
    BalanceGold balance = new BalanceGold("test user id");

    decimal gold = 9999;

    balance.Increase(gold);

    Assert.Equal(gold, balance.Gold);
  }

  [Fact]
  public void IncreaseGoldAndCheckNewAmount()
  {
    BalanceGold balance = new BalanceGold("test user id");

    decimal increasegold = 9999;
    decimal decreaseGold = 1000;
    decimal totalGold = increasegold + decreaseGold;

    balance.Increase(increasegold);

    balance.Increase(decreaseGold);

    Assert.Equal(totalGold, balance.Gold);
  }
}

