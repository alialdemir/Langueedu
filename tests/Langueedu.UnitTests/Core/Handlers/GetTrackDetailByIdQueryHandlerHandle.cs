using Langueedu.Core.Features.Queries.Track.GetTrackDetailById;
using Langueedu.Core.Interfaces;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Handlers;

public class GetTrackDetailByIdQueryHandlerHandle
{
  private GetTrackDetailByIdQueryHandler _handler;
  private Mock<ITrackService> _trackServiceMock;

  public GetTrackDetailByIdQueryHandlerHandle()
  {
    _trackServiceMock = new Mock<ITrackService>();
    _handler = new GetTrackDetailByIdQueryHandler(_trackServiceMock.Object);
  }

  [Fact]
  public async Task ThrowsExceptionGivenNullEventArgument()
  {
#nullable disable
    Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
#nullable enable
  }

  [Fact]
  public async Task SendsGetTrackDetailGivenEventInstance()
  {
    await _handler.Handle(new GetTrackDetailByIdQuery(1, Constants.UserId), CancellationToken.None);

    _trackServiceMock.Verify(sender => sender.GetTrackDetailByIdAsync(1, Constants.UserId));
  }

  [Fact]
  public async Task ReturnsErrorIfGivenIdIsZero()
  {
    var result = await _handler.Handle(new GetTrackDetailByIdQuery(0, Constants.UserId), CancellationToken.None);

    Assert.Equal(Ardalis.Result.ResultStatus.Invalid, result.Status);
    Assert.True(result.ValidationErrors.Any());
  }
}

