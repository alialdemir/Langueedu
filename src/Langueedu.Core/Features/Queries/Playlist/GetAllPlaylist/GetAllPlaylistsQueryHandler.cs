using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.ViewModels;
using MediatR;

namespace Langueedu.Core.Features.Queries.Playlist.GetAllPlaylist;

public class GetAllPlaylistsQueryHandler : IRequestHandler<GetAllPlaylistsQuery, Result<IEnumerable<PlaylistViewModel>>>
{
    private readonly IPlaylistService _playlistService;

    public GetAllPlaylistsQueryHandler(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    public async Task<Result<IEnumerable<PlaylistViewModel>>> Handle(GetAllPlaylistsQuery domainEvent, CancellationToken cancellationToken)
    {
        var validator = new GetAllPlaylistQueryValidator();
        var validate = validator.Validate(domainEvent);
        if (!validate.IsValid)
        {
            return Result<IEnumerable<PlaylistViewModel>>.Invalid(validate.AsErrors());
        }

        var response = await _playlistService
            .GetAllPlaylistsAsync();

        return response;
    }
}
