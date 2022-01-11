using Langueedu.Core.Features.Queries.Playlist.GetAllPlaylist;
using Langueedu.Core.Interfaces;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Handlers;

public class GetAllPlaylistsQueryHandlerHandle
{
    private GetAllPlaylistsQueryHandler _handler;
    private Mock<IPlaylistService> _playlistServiceMock;

    public GetAllPlaylistsQueryHandlerHandle()
    {
        _playlistServiceMock = new Mock<IPlaylistService>();
        _handler = new GetAllPlaylistsQueryHandler(_playlistServiceMock.Object);
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
        await _handler.Handle(new GetAllPlaylistsQuery(), CancellationToken.None);

        _playlistServiceMock.Verify(sender => sender.GetAllPlaylistsAsync());
    }
}

