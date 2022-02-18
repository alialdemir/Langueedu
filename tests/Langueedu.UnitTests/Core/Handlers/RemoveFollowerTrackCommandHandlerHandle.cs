using AutoMapper;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Features.Commands.Playlist.RemoveFollowerTrack;
using Langueedu.Core.Interfaces;
using Moq;
using Xunit;
namespace Langueedu.UnitTests.Core.Handlers;

public class RemoveFollowerTrackCommandHandlerHandle
{
  private readonly RemoveFollowerTrackCommandHandler _handler;
  private readonly Mock<IFollowerTrackService> _folowerTrackService = new();
  private readonly RemoveFollowerTrackCommand _removeFollowerTrackCommand = new(Constants.UserId, 1);
  private readonly FollowerTrack _followerTrack = new(1, Constants.UserId);

  public RemoveFollowerTrackCommandHandlerHandle()
  {
    IMapper mapper = Mock.Of<IMapper>(l =>
   l.Map<FollowerTrack>(_removeFollowerTrackCommand) == _followerTrack);

    _handler = new RemoveFollowerTrackCommandHandler(_folowerTrackService.Object, mapper);
  }

  [Fact]
  public async Task ThrowsExceptionGivenNullEventArgument()
  {
#nullable disable
    Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
#nullable enable
  }

  [Fact]
  public async Task SendsDeleteGivenVerify()
  {
    await _handler.Handle(_removeFollowerTrackCommand, CancellationToken.None);

    _folowerTrackService.Verify(sender => sender.DeleteAsync(_followerTrack));
  }
}

