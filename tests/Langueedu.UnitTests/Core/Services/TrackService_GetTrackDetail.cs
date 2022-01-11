using System;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Services;
using Langueedu.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Services
{
    public class TrackService_GetTrackDetail
    {
        private Mock<IReadRepository<Playlist>> _mockRepo = new Mock<IReadRepository<Playlist>>();
        private PlaylistService _searchService;

        public TrackService_GetTrackDetail()
        {
            _searchService = new PlaylistService(_mockRepo.Object);
        }

        [Fact]
        public async Task ReturnsPlaylistDetailGivenByPlaylistId()
        {
            var result = await _searchService.GetPlaylistDetailByIdAsync(1);

            Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
        }
        [Fact]
        public void ThrowsExceptionGivenNegativeOrZeroTrack()
        {
            Action action = () => _searchService.GetPlaylistDetailByIdAsync(0);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("playlistId", ex.ParamName);
        }
    }
}

