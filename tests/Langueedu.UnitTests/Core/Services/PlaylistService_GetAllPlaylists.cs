using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Services;
using Langueedu.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Services
{
    public class PlaylistService_GetAllPlaylists
    {
        private Mock<IReadRepository<Playlist>> _mockRepo = new Mock<IReadRepository<Playlist>>();
        private PlaylistService _playlistService;

        public PlaylistService_GetAllPlaylists()
        {
            _playlistService = new PlaylistService(_mockRepo.Object);
        }

        [Fact]
        public async Task ReturnsListsGivenActivePlaylist()
        {
            var result = await _playlistService.GetAllPlaylistsAsync();

            Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
        }
    }
}
