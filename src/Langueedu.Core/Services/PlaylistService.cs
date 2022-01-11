using Ardalis.GuardClauses;
using Ardalis.Result;
using Langueedu.Core.Specifications;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.Interfaces;
using Langueedu.SharedKernel.ViewModels;
using MediatR;
using Ardalis.Result.FluentValidation;
using Langueedu.Core.Features.Queries.Playlist;
using Langueedu.Core.Interfaces;

namespace Langueedu.Core.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IReadRepository<Langueedu.Core.Entities.PlaylistAggregate.Playlist> _playlistRepository;

        public PlaylistService(IReadRepository<Langueedu.Core.Entities.PlaylistAggregate.Playlist> playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }
        public async Task<Result<IEnumerable<PlaylistViewModel>>> GetAllPlaylistAsync()
        {
            var spec = new GetAllPlaylistSpec();
            var response = await _playlistRepository
                .ListAsync(spec);

            return Result<IEnumerable<PlaylistViewModel>>.Success(response);
        }

    }
}

