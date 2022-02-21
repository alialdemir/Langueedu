using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Services;
using Langueedu.Core.Specifications.Track;
using Langueedu.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Services;

public class FollowTrack
{
  private readonly Mock<IRepository<FollowerTrack>> _mockFollowerTrackRepo = new();
  private readonly Mock<IReadRepository<FollowerTrack>> _mockFollowerTrackReadRepo = new();
  private readonly Mock<IRepository<Track>> _mockTrackRepo = new();
  private readonly FollowerTrackService _followTrackService;
  private readonly FollowerTrack _followerTrack = new(1, Constants.UserId);
  private Track _track = new("test", "test", 1);
  public FollowTrack()
  {
    _track.Id = 1;
    _followerTrack.Id = 1;

    _mockFollowerTrackRepo.Setup(foo => foo.GetBySpecAsync(It.IsAny<GetFollowerTrackByUserIdAndIdSpec>(), default).Result).Returns(_followerTrack);
    _mockTrackRepo.Setup(foo => foo.GetByIdAsync(It.IsAny<short>(), default).Result).Returns(_track);
    _mockFollowerTrackReadRepo.Setup(foo => foo.AnyAsync(It.IsAny<GetFollowerTrackByUserIdAndIdSpec>(), default).Result).Returns(true);

    _followTrackService = new(_mockFollowerTrackRepo.Object, _mockTrackRepo.Object, _mockFollowerTrackReadRepo.Object);
  }

  [Fact]
  public async Task ReturnSuccessAddFollowTrack()
  {
    _mockFollowerTrackReadRepo.Setup(foo => foo.AnyAsync(It.IsAny<GetFollowerTrackByUserIdAndIdSpec>(), default).Result).Returns(false);

    var result = await _followTrackService.AddAsync(new FollowerTrack(1, Constants.UserId));


    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
  }

  [Fact]
  public async Task ReturnSuccessDeleteFollowTrack()
  {

    var result = await _followTrackService.DeleteAsync(new FollowerTrack(1, Constants.UserId));



    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
  }

  [Fact]
  public async Task ReturnsErrorAlreadyTrackId()
  {
    var result = await _followTrackService.AddAsync(new FollowerTrack(1, Constants.UserId));

    Assert.Equal(Ardalis.Result.ResultStatus.Error, result.Status);
    Assert.Contains(result.Errors, x => x == "The track is already being followed!");
  }
}
