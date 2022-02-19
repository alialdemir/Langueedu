using Langueedu.Core.Entities.BalanceAggregate;
using Xunit;

namespace Langueedu.UnitTests.Core.PlaylistAggregate;

public class Balance_IncreaseGold
{
  [Fact]
  public void IncreaseGoldAndCheckGoldBalance()
  {
    Balance balance = new Balance("test user id");

    decimal gold = 9999;

    balance.IncreaseGold(gold);

    Assert.Equal(gold, balance.Gold);
  }

  [Fact]
  public void DecreaseGoldAndCheckNewAmount()
  {
    Balance balance = new Balance("test user id");

    decimal increasegold = 9999;
    decimal decreaseGold = 1000;
    decimal diff = increasegold - decreaseGold;

    balance.DecreaseGold(decreaseGold);

    balance.IncreaseGold(increasegold);

    Assert.Equal(diff, balance.Gold);
  }
}

