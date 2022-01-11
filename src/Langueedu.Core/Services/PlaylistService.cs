using Ardalis.Result;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Specifications;
using Langueedu.SharedKernel.Interfaces;
using Langueedu.SharedKernel.ViewModels;

namespace Langueedu.Core.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IReadRepository<Langueedu.Core.Entities.PlaylistAggregate.Playlist> _playlistRepository;

        public PlaylistService(IReadRepository<Langueedu.Core.Entities.PlaylistAggregate.Playlist> playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<Result<IEnumerable<PlaylistViewModel>>> GetAllPlaylistsAsync()
        {
            var spec = new GetAllPlaylistSpec();
            var response = await _playlistRepository
                .ListAsync(spec);

            return Result<IEnumerable<PlaylistViewModel>>.Success(response);
        }

    }
}

