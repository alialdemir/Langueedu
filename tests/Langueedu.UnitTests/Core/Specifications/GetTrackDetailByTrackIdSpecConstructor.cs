using Langueedu.Core.Enums;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Specifications;
using Langueedu.SharedKernel.ViewModels;
using Xunit;

namespace Langueedu.UnitTests.Core.Specifications;

public class GetTrackDetailByTrackIdSpecConstructor
{
    [Fact]
    public void FilterCollectionToOnlyReturnItemsWithIsDoneFalse()
    {
        var item1 = new Track("Track 1", "test id", "test lang", 1).ChangeContentStatus(ContentStatus.Active);
        var item2 = new Track("Track 1", "test id", "test lang", 1).ChangeContentStatus(ContentStatus.Active);
        var item3 = new Track("Track 1", "test id", "test lang", 1).ChangeContentStatus(ContentStatus.Active);

        item1.Id = 1;

        var items = new List<Track>() { item1, item2, item3 };

        var spec = new GetTrackDetailByTrackIdSpec(1);
        Track filteredList = items.FirstOrDefault(spec.WhereExpressions.First().FilterFunc);

        Assert.Equal(item1, filteredList);
    }
}