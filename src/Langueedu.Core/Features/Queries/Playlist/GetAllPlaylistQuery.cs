using Ardalis.Result;
using Langueedu.SharedKernel.ViewModels;
using MediatR;

namespace Langueedu.Core.Features.Queries.Playlist;

public class GetAllPlaylistQuery : IRequest<Result<IEnumerable<PlaylistViewModel>>>
{
}