using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Specifications.Balance;
using Xunit;

namespace Langueedu.UnitTests.Core.Specifications;

public class GetBalanceByUserIdSpecConstructor
{

  [Fact]
  public void FilterCollectionToOnlyReturnItemsWithIsDoneFalse()
  {
    var item1 = new Balance(Constants.UserId);
    var item2 = new Balance("User 2");
    var item3 = new Balance("User 3");

    item1.Increase(10);
    item2.Increase(10);
    item3.Increase(10);

    var items = new List<Balance>() { item2, item1, item3 };

    var spec = new GetBalanceByUserIdSpec(Constants.UserId);
    var balance = items
      .FirstOrDefault(spec.WhereExpressions.First().FilterFunc);

    Assert.NotNull(balance);
    Assert.Equal(item1.UserId, balance.UserId);
    Assert.Equal(item1.Gold, balance.Gold);
  }
}
