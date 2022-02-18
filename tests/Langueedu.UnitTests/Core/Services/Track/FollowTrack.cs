using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Services;
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

  public FollowTrack()
  {

    _followTrackService = new(_mockFollowerTrackRepo.Object, _mockTrackRepo.Object, _mockFollowerTrackReadRepo.Object);
  }

  [Fact]
  public async Task ReturnSuccessAddFollowTrack()
  {
    var result = await _followTrackService.AddAsync(new FollowerTrack(1, Constants.UserId));

    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
  }

  [Fact]
  public async Task ReturnsErrorAlreadyTrackId()
  {
    //_mockFollowerTrackReadRepo.Setup(r => r.AnyAsync(It.IsAny<ISpecification<FollowerTrack>>()))
    //    .ReturnsAsync(true);


    var result = await _followTrackService.AddAsync(new FollowerTrack(1, Constants.UserId));

    Assert.Equal(Ardalis.Result.ResultStatus.Error, result.Status);
    Assert.Contains(result.Errors, x => x == "The track is already being followed!");
  }
}
