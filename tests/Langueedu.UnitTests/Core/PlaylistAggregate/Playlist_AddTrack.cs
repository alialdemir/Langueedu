using Langueedu.Core.Entities.PlaylistAggregate;
using Xunit;

namespace Langueedu.UnitTests.Core.PlaylistAggregate;

public class Playlist_AddTrack
{
  private PlayList _testPlaylist = new PlayList("some name");

  [Fact]
  public void AddsTrackToTracks()
  {
    var _testTrack = new Track("test title", "test video id", 12);

    _testPlaylist.AddTrack(_testTrack);

    Assert.Contains(_testTrack, _testPlaylist.Tracks);
  }

  [Fact]
  public void ThrowsExceptionGivenNullTrack()
  {
#nullable disable
    Action action = () => _testPlaylist.AddTrack(null);
#nullable enable

    var ex = Assert.Throws<ArgumentNullException>(action);
    Assert.Equal("track", ex.ParamName);
  }
}

