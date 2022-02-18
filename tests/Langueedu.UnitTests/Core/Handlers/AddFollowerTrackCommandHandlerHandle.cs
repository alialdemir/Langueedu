using AutoMapper;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Features.Commands.Playlist.FollowerTrack;
using Langueedu.Core.Interfaces;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Handlers;

public class AddFollowerTrackCommandHandlerHandle
{
  private readonly AddFollowerTrackCommandHandler _handler;
  private readonly Mock<IFollowerTrackService> _folowerTrackService = new();
  private readonly AddFollowerTrackCommand _addFollowerTrackCommand = new(Constants.UserId, 1);
  private readonly FollowerTrack _followerTrack = new(1, Constants.UserId);

  public AddFollowerTrackCommandHandlerHandle()
  {
    IMapper mapper = Mock.Of<IMapper>(l =>
   l.Map<FollowerTrack>(_addFollowerTrackCommand) == _followerTrack);

    _handler = new AddFollowerTrackCommandHandler(_folowerTrackService.Object, mapper);
  }

  [Fact]
  public async Task ThrowsExceptionGivenNullEventArgument()
  {
#nullable disable
    Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
#nullable enable
  }

  [Fact]
  public async Task SendsGetAllPlaylistGivenEventInstance()
  {
    await _handler.Handle(_addFollowerTrackCommand, CancellationToken.None);

    _folowerTrackService.Verify(sender => sender.AddAsync(_followerTrack));
  }
}

