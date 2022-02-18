using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Specifications.Track;
using Xunit;

namespace Langueedu.UnitTests.Core.Specifications;

public class GetFollowerTrackByUserIdAndIdSpecConstructor
{
  [Fact]
  public void FilterCollectionToOnlyReturnItemsWithIsDoneFalse()
  {
    var item1 = new FollowerTrack(1, Constants.UserId);
    var item2 = new FollowerTrack(2, Constants.UserId);
    var item3 = new FollowerTrack(3, Constants.UserIdDemo);

    item1.Id = 1;

    var items = new List<FollowerTrack>() { item1, item2, item3 };

    var spec = new GetFollowerTrackByUserIdAndIdSpec(Constants.UserId, 1);

    var filteredList = items.FirstOrDefault(spec.WhereExpressions.First().FilterFunc);

    Assert.Equal(item1, filteredList);
  }
}

