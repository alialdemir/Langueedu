using Langueedu.Core.Features.Queries.Playlist;
using Langueedu.Core.Interfaces;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Handlers;

public class GetAllPlaylistQueryHandlerHandle
{
    private GetAllPlaylistQueryHandler _handler;
    private Mock<IPlaylistService> _playlistServiceMock;

    public GetAllPlaylistQueryHandlerHandle()
    {
        _playlistServiceMock = new Mock<IPlaylistService>();
        _handler = new GetAllPlaylistQueryHandler(_playlistServiceMock.Object);
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
        await _handler.Handle(new GetAllPlaylistQuery(), CancellationToken.None);

        _playlistServiceMock.Verify(sender => sender.GetAllPlaylistAsync());
    }
}

