using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Specifications.Lyrics;
using Xunit;

namespace Langueedu.UnitTests.Core.Specifications;

public class GetLyricsByTrackIdSpecConstructor
{
  [Fact]
  public void FilterCollectionToOnlyReturnItemsWithIsDoneFalse()
  {
    short trackId = 1;

    var item1 = new Lyrics("text", "answer", 0, 2);
    var item2 = new Lyrics("text", "answer", 1, 1);
    var item3 = new Lyrics("text", "answer", 2, 3);

    item1.Id = 1;
    item2.Id = 2;
    item3.Id = 3;

    var items = new List<Lyrics>() { item1, item2, item3 };

    var spec = new GetLyricsByTrackIdSpec(trackId);

    var filteredList = spec.Evaluate(items);

    //var filteredList = items
    //    .Where(spec.WhereExpressions.FirstOrDefault().FilterFunc);

    //Assert.Contains(filteredList, x => x.LyricsId == item1.Id);
  }
}

