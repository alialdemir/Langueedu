using System;
using Langueedu.Core.Entities.PlaylistAggregate;
using Xunit;

namespace Langueedu.UnitTests.Core.PlaylistAggregate;

public class Track_RemoveFollowerTrack
{
  private Track _track = new("some name", "youtube video id", "lang", 12);

  [Fact]
  public void RemoveFollowedTrackCheckNenativeFollowerCount()
  {
    _track.RemoveFollowerTrack(new FollowerTrack(1, Constants.UserId));

    Assert.Equal(0, _track.FollowerCount);
  }

  [Fact]
  public void RemoveFollowedTrackCheckFollowerCount()
  {
    _track.AddFollowerTrack(new FollowerTrack(1, Constants.UserId));
    _track.AddFollowerTrack(new FollowerTrack(2, Constants.UserId));
    _track.AddFollowerTrack(new FollowerTrack(3, Constants.UserId));

    _track.RemoveFollowerTrack(new FollowerTrack(1, Constants.UserId));

    Assert.Equal(2, _track.FollowerCount);
  }
}

