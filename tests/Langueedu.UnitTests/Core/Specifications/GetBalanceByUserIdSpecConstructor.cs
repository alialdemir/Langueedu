using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Specifications.Balance;
using Xunit;

namespace Langueedu.UnitTests.Core.Specifications;

public class GetBalanceByUserIdSpecConstructor
{

  [Fact]
  public void FilterCollectionToOnlyReturnItemsWithIsDoneFalse()
  {
    var item1 = new Balance(Constants.UserId).IncreaseGold(10);
    var item2 = new Balance("User 2").IncreaseGold(20);
    var item3 = new Balance("User 3").IncreaseGold(30);

    var items = new List<Balance>() { item1, item2, item3 };

    var spec = new GetBalanceByUserIdSpec(Constants.UserId);
    var balance = items
      .FirstOrDefault(spec.WhereExpressions.First().FilterFunc);

    Assert.NotNull(balance);
    Assert.Equal(item1.UserId, balance.UserId);
    Assert.Equal(item1.Gold, balance.Gold);
  }
}
