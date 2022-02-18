using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Xunit;

namespace Langueedu.UnitTests.Core.PlaylistAggregate;

public class Playlist_ChangeContentStatus
{
  private Playlist _testPlaylist = new Playlist("some name");

  [Fact]
  public void ChangeContentStatusToContentStatus()
  {
    _testPlaylist.ChangeContentStatus(ContentStatus.Active);

    Assert.Equal(ContentStatus.Active, _testPlaylist.ContentStatus);
  }
}

