using System;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Services;
using Langueedu.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Services;

public class TrackService_GetTrackDetail
{
  private Mock<IReadRepository<Track>> _mockRepo = new Mock<IReadRepository<Track>>();
  private TrackService _trackService;
  private readonly string _userId = "1111-1111-1111-1111";

  public TrackService_GetTrackDetail()
  {
    _trackService = new TrackService(_mockRepo.Object);
  }

  [Fact]
  public async Task ReturnsTrackDetailGivenByTrackId()
  {
    var result = await _trackService.GetTrackDetailByIdAsync(1, _userId);

    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
  }

  [Fact]
  public async Task ReturnsErrorIfGivenIdIsZero()
  {
    var result = await _trackService.GetTrackDetailByIdAsync(0, _userId);

    Assert.Equal(Ardalis.Result.ResultStatus.Invalid, result.Status);
    Assert.True(result.ValidationErrors.Any());
  }
}

