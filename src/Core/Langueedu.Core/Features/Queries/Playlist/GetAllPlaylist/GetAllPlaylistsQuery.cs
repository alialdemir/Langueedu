using Ardalis.Result;
using Langueedu.SharedKernel.ViewModels;
using MediatR;

namespace Langueedu.Core.Features.Queries.Playlist.GetAllPlaylist;

public class GetAllPlaylistsQuery : IRequest<Result<IEnumerable<PlaylistViewModel>>>
{
}