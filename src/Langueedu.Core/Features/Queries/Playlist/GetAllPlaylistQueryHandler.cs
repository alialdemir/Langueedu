using Ardalis.GuardClauses;
using Ardalis.Result;
using Langueedu.Core.Specifications;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.Interfaces;
using Langueedu.SharedKernel.ViewModels;
using MediatR;
using Ardalis.Result.FluentValidation;
using Langueedu.Core.Interfaces;

namespace Langueedu.Core.Features.Queries.Playlist;

public class GetAllPlaylistQueryHandler : IRequestHandler<GetAllPlaylistQuery, Result<IEnumerable<PlaylistViewModel>>>
{
    private readonly IPlaylistService _playlistService;

    public GetAllPlaylistQueryHandler(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    public async Task<Result<IEnumerable<PlaylistViewModel>>> Handle(GetAllPlaylistQuery domainEvent, CancellationToken cancellationToken)
    {
        var validator = new GetAllPlaylistCommandValidator();
        var validate = validator.Validate(domainEvent);
        if (!validate.IsValid)
        {
            return Result<IEnumerable<PlaylistViewModel>>.Invalid(validate.AsErrors());
        }

        var spec = new GetAllPlaylistSpec();
        var response = await _playlistService
            .GetAllPlaylistAsync();

        return response;// Result<IEnumerable<PlaylistViewModel>>.Success(response);
    }
}
