using Langueedu.Core.Enums;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Specifications;
using Langueedu.SharedKernel.ViewModels;
using Xunit;

namespace Langueedu.UnitTests.Core.Specifications;

public class GetAllPlaylistSpecificationConstructor
{
    [Fact]
    public void FilterCollectionToOnlyReturnItemsWithIsDoneFalse()
    {
        var item1 = new Playlist("Playlist 1").ChangeContentStatus(ContentStatus.Active);
        var item2 = new Playlist("Playlist 2").ChangeContentStatus(ContentStatus.Active);
        var item3 = new Playlist("Playlist 3").ChangeContentStatus(ContentStatus.Passive);

        var items = new List<Playlist>() { item1, item2, item3 };

        var spec = new GetAllPlaylistSpec();
        List<Playlist> filteredList = items
            .Where(spec.WhereExpressions.First().Compile())
            .ToList();

        Assert.Contains(item1, filteredList);
        Assert.Contains(item2, filteredList);
        Assert.DoesNotContain(item3, filteredList);
    }
}

