﻿using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Langueedu.Core.Specifications;
using Xunit;

namespace Langueedu.UnitTests.Core.Specifications;

public class GetTrackDetailByTrackIdSpecConstructor
{
  [Fact]
  public void FilterCollectionToOnlyReturnItemsWithIsDoneFalse()
  {
    var item1 = new Track("Track 1", "test id", 1).ChangeContentStatus(ContentStatus.Active);
    var item2 = new Track("Track 1", "test id", 1).ChangeContentStatus(ContentStatus.Active);
    var item3 = new Track("Track 1", "test id", 1).ChangeContentStatus(ContentStatus.Active);

    var album = new Album("test");
    item1.Album = album.ChangeContentStatus(ContentStatus.Active);

    item1.AddPerformsOnSongs(new List<Artist>
    {
      new Artist("test").ChangeContentStatus(ContentStatus.Active)
    });

    item1.Id = 1;

    var items = new List<Track>() { item1, item2, item3 };

    var spec = new GetTrackDetailByTrackIdSpec(1, Constants.UserId);

    var filteredList = items.FirstOrDefault(spec.WhereExpressions.First().FilterFunc);

    Assert.Equal(item1, filteredList);
  }
}
