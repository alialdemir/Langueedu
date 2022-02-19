using Langueedu.Core.Entities.BalanceAggregate;
using Xunit;

namespace Langueedu.UnitTests.Core.PlaylistAggregate;

public class Balance_DecreaseGold
{
  [Fact]
  public void DecreaseGoldAndCheckGoldBalance()
  {
    Balance balance = new Balance("some name");

    decimal gold = 9999;

    balance.DecreaseGold(gold);

    Assert.Equal(gold, -balance.Gold);
  }

  [Fact]
  public void IncreaseGoldAndCheckNewAmount()
  {
    Balance balance = new Balance("some name");

    decimal increasegold = 9999;
    decimal decreaseGold = 1000;
    decimal diff = increasegold - decreaseGold;

    balance.IncreaseGold(increasegold);

    balance.DecreaseGold(decreaseGold);

    Assert.Equal(diff, balance.Gold);
  }
}

