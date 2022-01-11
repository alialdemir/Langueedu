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
        private PlaylistService _searchService;

        public PlaylistService_GetAllPlaylists()
        {
            _searchService = new PlaylistService(_mockRepo.Object);
        }

        [Fact]
        public async Task ReturnsListsGivenActivePlaylist()
        {
            var result = await _searchService.GetAllPlaylistsAsync();

            Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
        }
    }
}
