using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Xunit;

namespace Langueedu.UnitTests.Core.PlaylistAggregate;

public class PlaylistConstructor
{
  private string _testName = "test name";
  private Playlist? _testPlaylist;

  private Playlist CreatePlaylist()
  {
    return new Playlist(_testName);
  }

  [Fact]
  public void InitializesName()
  {
    _testPlaylist = CreatePlaylist();

    Assert.Equal(_testName, _testPlaylist.PlaylistName);
  }

  [Fact]
  public void InitializesTaskListToEmptyList()
  {
    _testPlaylist = CreatePlaylist();

    Assert.NotNull(_testPlaylist.Tracks);
  }

  [Fact]
  public void InitializesStatusToInProgress()
  {
    _testPlaylist = CreatePlaylist();

    Assert.Equal(ContentStatus.Passive, _testPlaylist.ContentStatus);
  }
}

