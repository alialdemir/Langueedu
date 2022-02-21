using Langueedu.Core.Entities.PlaylistAggregate;
using Xunit;

namespace Langueedu.UnitTests.Core.PlaylistAggregate;

public class Track_AddFollowerTrack
{
  private Track _track = new("some name", "youtube video id", 12);

  [Fact]
  public void AddFollowedTrackCheckFollowerCount()
  {
    _track.AddFollowerTrack(new FollowerTrack(1, Constants.UserId));

    Assert.Equal(1, _track.FollowerCount);
  }
}

